using System.Collections.Generic;
using Plugin.Code.Runtime.Enums;
using UnityEngine;

namespace Plugin.Code.Runtime.Layout
{
    public class RectLayout : IGridLayout
    {
        private readonly float _cellSize;
        private readonly GridPlane _plane;

        private static readonly Vector2Int[] NeighborOffsets =
        {
            new(0, 1),
            new(1, 0),
            new(0, -1),
            new(-1, 0)
        };

        public RectLayout(float cellSize, GridPlane plane)
        {
            _cellSize = cellSize;
            _plane = plane;
        }
        
        public Vector3 CellToWorld(Vector2Int coord)
        {
           var x = coord.x * _cellSize;
           var y = coord.y * _cellSize;
           return ToWorld(x, y);
        }

        public Vector2Int WorldToCell(Vector3 worldPosition)
        {
            var (x, y) = FromWorld(worldPosition);
            var cellX = Mathf.RoundToInt(x / _cellSize);
            var cellY = Mathf.RoundToInt(y / _cellSize);
            return new Vector2Int(cellX, cellY);
        }

        public IReadOnlyList<Vector2Int> GetNeighbors(Vector2Int coord)
        {
            var result = new Vector2Int[NeighborOffsets.Length];
            
            for (int i = 0; i < NeighborOffsets.Length; i++)
                result[i] = coord + NeighborOffsets[i];

            return result;
        }

        private Vector3 ToWorld(float x, float y)
        {
            return _plane == GridPlane.XZ ? new Vector3(x, 0, y) : new Vector3(x, y, 0);
        }
        
        private (float x, float y) FromWorld(Vector3 worldPosition)
        {
            return _plane == GridPlane.XZ ? (worldPosition.x, worldPosition.z) : (worldPosition.x, worldPosition.y);
        }
        
        public Quaternion TileRotation =>
            _plane == GridPlane.XY
                ? Quaternion.Euler(-90f, 0f, 0f)
                : Quaternion.identity;    
    }
}