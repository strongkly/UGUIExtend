using UnityEngine;

namespace CrazyBox.UGUIExtend
{
    [ExecuteAlways]
    public class VerticalLayoutSizeFitter : HorizontalOrVerticalSizeFitterLayoutGroup
    {
        public override void CalculateLayoutInputHorizontal()
        {
            base.CalculateLayoutInputHorizontal();
            CalcAlongAxis(0, true);
        }

        public override void CalculateLayoutInputVertical()
        {
            CalcAlongAxis(1, true);
        }

        public override void SetLayoutHorizontal()
        {
            base.SetLayoutHorizontal();
            SetChildrenAlongAxis(0, true);
        }

        public override void SetLayoutVertical()
        {
            base.SetLayoutVertical();
            SetChildrenAlongAxis(1, true);
        }
    }
}