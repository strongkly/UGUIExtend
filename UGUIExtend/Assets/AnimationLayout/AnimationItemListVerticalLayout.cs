using CrazyBox.UGUIExtend;
using UnityEngine;

public class AnimationItemListVerticalLayout : AnimationItemListLayout
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

    public override Vector2 GetAnchoredPosFor(int childIdx)
    {
        return GetChildrenAnchoredPositionAlongAxis(1, childIdx);
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
