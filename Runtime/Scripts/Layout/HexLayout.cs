using System.Collections.Generic;
using Plugin.Code.Runtime.Enums;
using UnityEngine;

namespace Plugin.Code.Runtime.Layout
{
    public class HexLayout : IGridLayout
    {
        private readonly float _cellSize;
        private readonly GridPlane _plane;
        private readonly HexOrientation _orientation;

        private static readonly Vector2Int[] NeighborOffsets =
        {
            new(1, 0),
            new(1, -1),
            new(0, -1),
            new(-1, 0),
            new(-1, 1),
            new(0, 1)
        };

        public HexLayout(float cellSize, GridPlane plane, HexOrientation orientation)
        {
            _cellSize = cellSize;
            _plane = plane;
            _orientation = orientation;
        }

        public Vector3 CellToWorld(Vector2Int coord)
        {
            var col = coord.x;
            var row = coord.y;
            float u, v;

            if (_orientation == HexOrientation.FlatTop)
            {
                u = _cellSize * 1.5f * col;
                var offset = (col % 2 != 0) ? _cellSize * Mathf.Sqrt(3f) * 0.5f : 0f;
                v = _cellSize * Mathf.Sqrt(3f) * row + offset;
            }
            else
            {
                v = _cellSize * 1.5f * row;
                var offset = (row % 2 != 0) ? _cellSize * Mathf.Sqrt(3f) * 0.5f : 0f;
                u = _cellSize * Mathf.Sqrt(3f) * col + offset;
            }

            return ToWorld(u, v);
        }

        public Vector2Int WorldToCell(Vector3 worldPosition)
        {
            return Vector2Int.zero;
        }

        public IReadOnlyList<Vector2Int> GetNeighbors(Vector2Int coord)
        {
            var result = new Vector2Int[NeighborOffsets.Length];
            for (int i = 0; i < NeighborOffsets.Length; i++)
                result[i] = coord + NeighborOffsets[i];
            
            return result;
        }

        private Vector3 ToWorld(float u, float v)
        {
            return _plane == GridPlane.XZ ? new Vector3(u, 0f, v) : new Vector3(u, v, 0f);
        }
        
        public Quaternion TileRotation =>
            _plane == GridPlane.XY
                ? Quaternion.Euler(-90f, 0f, 0f)
                : Quaternion.identity;
    }
}