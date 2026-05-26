using System.Collections.Generic;
using UnityEngine;

namespace Plugin.Code.Runtime.Core
{
    public class Tile : MonoBehaviour
    {
        [SerializeField] private Vector2Int coord;
        [SerializeField] private List<Vector2Int> neighbors = new();

        public Vector2Int Coord => coord;
        public IReadOnlyList<Vector2Int> Neighbors => neighbors;

        public void Setup(Vector2Int coord, Vector3 worldPosition, Quaternion rotation, List<Vector2Int> neighbors)
        {
            this.coord = coord;
            this.neighbors = neighbors;
            transform.SetPositionAndRotation(worldPosition, rotation);
        }
    }
}