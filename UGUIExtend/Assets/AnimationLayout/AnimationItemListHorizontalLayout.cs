using UnityEngine;
using UnityEngine.UI;

namespace CrazyBox.UGUIExtend
{
    public class AnimationItemListHorizontalLayout : AnimationItemListLayout
    {
        public override void CalculateLayoutInputHorizontal()
        {
            base.CalculateLayoutInputHorizontal();
            CalcAlongAxis(0, false);
        }

        public override void CalculateLayoutInputVertical()
        {
            CalcAlongAxis(1, false);
        }

        public override void SetLayoutHorizontal()
        {
            base.SetLayoutHorizontal();
            SetChildrenAlongAxis(0, false);
        }

        public override void SetLayoutVertical()
        {
            base.SetLayoutVertical();
            SetChildrenAlongAxis(1, false);
        }

        public override Vector2 GetAnchoredPosFor(int childIndx)
        {
            Vector2 result = GetChildrenAnchoredPositionAlongAxis(0, childIndx);
            //Debug.LogErrorFormat("childIndx({0}):{1}", childIndx, result);
            return result;
        }
    }
}