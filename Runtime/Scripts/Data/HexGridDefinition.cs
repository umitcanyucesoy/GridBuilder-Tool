using Plugin.Code.Runtime.Enums;
using Plugin.Code.Runtime.Layout;
using UnityEngine;

namespace Plugin.Code.Runtime.Data
{
    [CreateAssetMenu(fileName = "HexGridDefinition", menuName = "Grid System/Hexagon Grid Definition")]
    public sealed class HexGridDefinition : GridDefinition
    {
        public HexOrientation orientation = HexOrientation.PointyTop;

        public override IGridLayout CreateLayout()
        {
            return new HexLayout(cellSize, plane, orientation);
        }
    }
}