using System.Collections.Generic;
using Plugin.Code.Runtime.Core;
using Plugin.Code.Runtime.Data;
using Plugin.Code.Runtime.Layout;
using UnityEditor;
using UnityEngine;

namespace Plugin.Code.Editor
{
    public static class GridBuilder
    {
        private const string RootName = "Grid";

        public static void Build(GridDefinition definition)
        {
            if (!definition)
            {
                Debug.LogError("Grid definition is null. Cannot build grid.");
                return;
            }

            if (!definition.tilePrefab)
            {
                Debug.LogError("Tile prefab is not assigned in the grid definition. Cannot build grid.");
                return;
            }

            var layout = definition.CreateLayout();
            var rotation = layout.TileRotation;
            var root = GetOrCreateRoot();

            ClearChildren(root);

            var neighborBuffer = new List<Vector2Int>();

            for (int x = 0; x < definition.width; x++)
            {
                for (int y = 0; y < definition.height; y++)
                {
                    var coord = new Vector2Int(x, y);
                    var worldPos = layout.CellToWorld(coord);
                    
                    GetInBoundsNeighbors(layout, coord, definition.width, definition.height, neighborBuffer);
                    SpawnTile(definition.tilePrefab, root, coord, worldPos, rotation, neighborBuffer);
                }
            }
        }

        private static void GetInBoundsNeighbors(IGridLayout layout, Vector2Int coord,
            int width, int height, List<Vector2Int> buffer)
        {
            buffer.Clear();
            var raw = layout.GetNeighbors(coord);
            for (int i = 0; i < raw.Count; i++)
            {
                var n = raw[i];
                if (n.x >= 0 && n.x < width && n.y >= 0 && n.y < height)
                    buffer.Add(n);
            }
        }

        private static void SpawnTile(Tile prefab, Transform parent, Vector2Int coord,
            Vector3 worldPos, Quaternion rotation, List<Vector2Int> neighbors)
        {
            var instance = (Tile)PrefabUtility.InstantiatePrefab(prefab, parent);
            Undo.RegisterCreatedObjectUndo(instance, "Create Grid");
            instance.name = $"Tile_{coord.x},{coord.y}";

            if (instance.TryGetComponent(out Tile tile))
                tile.Setup(coord, worldPos, rotation, new List<Vector2Int>(neighbors));
            else
                Debug.LogError($"{prefab.name} does not have a tile component.");
        }

        private static Transform GetOrCreateRoot()
        {
            var existing = GameObject.Find(RootName);
            if (existing)
            {
                return existing.transform;
            }

            var root = new GameObject(RootName);
            Undo.RegisterCreatedObjectUndo(root, "Create Grid");
            return root.transform;
        }

        private static void ClearChildren(Transform root)
        {
            for (int i = root.childCount - 1; i >= 0; i--)
                Undo.DestroyObjectImmediate(root.GetChild(i).gameObject);
        }
    }
}