using System.Collections.Generic;
using UnityEngine;

namespace Plugin.Code.Runtime.Layout
{
    public interface IGridLayout
    {
        Vector3 CellToWorld(Vector2Int coord);
        Vector2Int WorldToCell(Vector3 worldPosition);
        IReadOnlyList<Vector2Int> GetNeighbors(Vector2Int coord);
        Quaternion TileRotation { get; }
    }
}