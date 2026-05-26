using UnityEngine;

namespace Plugin.Code.Runtime.Core
{
    public class Cell<TContent>
    {
        public Vector2Int Coord { get; }
        public Vector3 WorldPosition { get; }
        public TContent Content { get; set; }

        public Cell(Vector2Int coord, Vector3 worldPosition)
        {
            Coord = coord;
            WorldPosition = worldPosition;
        }
    }
}