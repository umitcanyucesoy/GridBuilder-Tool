using Plugin.Code.Runtime.Layout;
using UnityEngine;

namespace Plugin.Code.Runtime.Data
{
    [CreateAssetMenu(fileName = "RectangleGridDefinition", menuName = "Grid System/Rectangle Grid Definition")]
    public class RectangleDefinition : GridDefinition
    {
        public override IGridLayout CreateLayout()
        {
            return new RectLayout(cellSize, plane);
        }
    }
}