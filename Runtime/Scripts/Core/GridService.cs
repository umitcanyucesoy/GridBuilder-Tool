using System.Collections.Generic;
using Plugin.Code.Runtime.Layout;
using UnityEngine;

namespace Plugin.Code.Runtime.Core
{
    public sealed class GridService<TContent>
    {
        private readonly IGridLayout _layout;
        private readonly int _width;
        private readonly int _height;

        private Cell<TContent>[,] _cells;

        public int Width => _width;
        public int Height => _height;
        public IGridLayout Layout => _layout;

        public GridService(IGridLayout layout, int width, int height)
        {
            _layout = layout;
            _width = width;
            _height = height;
        }

        public void Build()
        {
            _cells = new Cell<TContent>[_width, _height];

            for (int x = 0; x < _width; x++)
            {
                for (int y = 0; y < _height; y++)
                {
                    var coord = new Vector2Int(x, y);
                    var worldPos = _layout.CellToWorld(coord);
                    _cells[x,y] = new Cell<TContent>(coord, worldPos);
                }
            }
        }

        public bool TryGetCell(Vector2Int coord, out Cell<TContent> cell)
        {
            if (!InBounds(coord))
            {
                cell = null;
                return false;
            }
            
            cell = _cells[coord.x, coord.y];
            return true;
        }

        public bool TryGetCell(Vector3 worldPosition, out Cell<TContent> cell)
        {
            var coord = _layout.WorldToCell(worldPosition);
            return TryGetCell(coord, out cell);
        }

        public void GetNeighbors(Vector2Int coord, List<Cell<TContent>> buffer)
        {
            buffer.Clear();
            
            var rawNeighbors = _layout.GetNeighbors(coord);
            for (int i = 0; i < rawNeighbors.Count; i++)
            {
                if (TryGetCell(rawNeighbors[i], out var neighbor))
                    buffer.Add(neighbor);
            }
        }

        public bool InBounds(Vector2Int coord)
        {
            return coord.x >= 0 && coord.x < _width && coord.y >= 0 && coord.y < _height;
        }
    }
}