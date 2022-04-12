using UnityEngine;

namespace CrazyBox.UGUIExtend
{
    [ExecuteAlways]
    public class HorizontalLayoutSizeFitter : HorizontalOrVerticalSizeFitterLayoutGroup
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
    }
}