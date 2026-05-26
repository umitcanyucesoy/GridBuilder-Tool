using Plugin.Code.Runtime.Core;
using Plugin.Code.Runtime.Enums;
using Plugin.Code.Runtime.Layout;
using UnityEngine;

namespace Plugin.Code.Runtime.Data
{
    public abstract class GridDefinition : ScriptableObject
    {
        [Header("Grid Settings")]
        [Min(1)] public int width;
        [Min(1)] public int height;
        [Min(0.01f)] public float cellSize;
        public GridPlane plane;
        public Tile tilePrefab;

        public abstract IGridLayout CreateLayout();
    }
}