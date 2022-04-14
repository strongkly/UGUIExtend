using UnityEngine;
using UnityEngine.UI;
#if UNITY_EDITOR
using UnityEditor;
#endif

namespace CrazyBox.UGUIExtend
{
    [ExecuteAlways]
    public abstract class AnimationItemListLayout : LayoutGroup, ILayoutSelfController
    {
#if UNITY_EDITOR
        [CustomEditor(typeof(AnimationItemListLayout), true)]
        [CanEditMultipleObjects]
        class AnimationItemListLayoutEditor : Editor 
        {
            SerializedProperty m_ChildrenFitMode;
            SerializedProperty m_SelfHorizontalFitMode;
            SerializedProperty m_SelfVerticalFitMode;
            SerializedProperty m_Padding;
            SerializedProperty m_Spacing;
            SerializedProperty m_ChildAlignment;
            SerializedProperty m_ChildControlWidth;
            SerializedProperty m_ChildControlHeight;
            SerializedProperty m_ChildScaleWidth;
            SerializedProperty m_ChildScaleHeight;
            SerializedProperty m_ChildForceExpandWidth;
            SerializedProperty m_ChildForceExpandHeight;

            protected virtual void OnEnable()
            {
                m_ChildrenFitMode = serializedObject.FindProperty("m_ChildrenFitMode");
                m_SelfHorizontalFitMode = serializedObject.FindProperty("m_SelfHorizontalFitMode");
                m_SelfVerticalFitMode = serializedObject.FindProperty("m_SelfVerticalFitMode");
                m_Padding = serializedObject.FindProperty("m_Padding");
                m_Spacing = serializedObject.FindProperty("m_Spacing");
                m_ChildAlignment = serializedObject.FindProperty("m_ChildAlignment");
                m_ChildControlWidth = serializedObject.FindProperty("m_ChildControlWidth");
                m_ChildControlHeight = serializedObject.FindProperty("m_ChildControlHeight");
                m_ChildScaleWidth = serializedObject.FindProperty("m_ChildScaleWidth");
                m_ChildScaleHeight = serializedObject.FindProperty("m_ChildScaleHeight");
                m_ChildForceExpandWidth = serializedObject.FindProperty("m_ChildForceExpandWidth");
                m_ChildForceExpandHeight = serializedObject.FindProperty("m_ChildForceExpandHeight");
            }

            public override void OnInspectorGUI()
            {
                serializedObject.Update();
                EditorGUILayout.PropertyField(m_ChildrenFitMode, true);
                EditorGUILayout.PropertyField(m_SelfHorizontalFitMode, true);
                EditorGUILayout.PropertyField(m_SelfVerticalFitMode, true);
                EditorGUILayout.PropertyField(m_Padding, true);
                EditorGUILayout.PropertyField(m_Spacing, true);
                EditorGUILayout.PropertyField(m_ChildAlignment, true);

                Rect rect = EditorGUILayout.GetControlRect();
                rect = EditorGUI.PrefixLabel(rect, -1, EditorGUIUtility.TrTextContent("Control Child Size"));
                rect.width = Mathf.Max(50, (rect.width - 4) / 3);
                EditorGUIUtility.labelWidth = 50;
                ToggleLeft(rect, m_ChildControlWidth, EditorGUIUtility.TrTextContent("Width"));
                rect.x += rect.width + 2;
                ToggleLeft(rect, m_ChildControlHeight, EditorGUIUtility.TrTextContent("Height"));
                EditorGUIUtility.labelWidth = 0;

                rect = EditorGUILayout.GetControlRect();
                rect = EditorGUI.PrefixLabel(rect, -1, EditorGUIUtility.TrTextContent("Use Child Scale"));
                rect.width = Mathf.Max(50, (rect.width - 4) / 3);
                EditorGUIUtility.labelWidth = 50;
                ToggleLeft(rect, m_ChildScaleWidth, EditorGUIUtility.TrTextContent("Width"));
                rect.x += rect.width + 2;
                ToggleLeft(rect, m_ChildScaleHeight, EditorGUIUtility.TrTextContent("Height"));
                EditorGUIUtility.labelWidth = 0;

                rect = EditorGUILayout.GetControlRect();
                rect = EditorGUI.PrefixLabel(rect, -1, EditorGUIUtility.TrTextContent("Child Force Expand"));
                rect.width = Mathf.Max(50, (rect.width - 4) / 3);
                EditorGUIUtility.labelWidth = 50;
                ToggleLeft(rect, m_ChildForceExpandWidth, EditorGUIUtility.TrTextContent("Width"));
                rect.x += rect.width + 2;
                ToggleLeft(rect, m_ChildForceExpandHeight, EditorGUIUtility.TrTextContent("Height"));
                EditorGUIUtility.labelWidth = 0;



                serializedObject.ApplyModifiedProperties();
            }

            void ToggleLeft(Rect position, SerializedProperty property, GUIContent label)
            {
                bool toggle = property.boolValue;
                EditorGUI.showMixedValue = property.hasMultipleDifferentValues;
                EditorGUI.BeginChangeCheck();
                int oldIndent = EditorGUI.indentLevel;
                EditorGUI.indentLevel = 0;
                toggle = EditorGUI.ToggleLeft(position, label, toggle);
                EditorGUI.indentLevel = oldIndent;
                if (EditorGUI.EndChangeCheck())
                {
                    property.boolValue = property.hasMultipleDifferentValues ? true : !property.boolValue;
                }
                EditorGUI.showMixedValue = false;
            }
        }
#endif

        [SerializeField] protected FitMode m_ChildrenFitMode = FitMode.MinSize;
        
        public FitMode ChildrenFit { get { return m_ChildrenFitMode; } set { if (SetPropertyUtility.SetStruct(ref m_ChildrenFitMode, value)) SetDirty(); } }

        [SerializeField] protected FitMode m_SelfHorizontalFitMode = FitMode.Unconstrained;

        public FitMode SelfHorizontalFit { get { return m_SelfHorizontalFitMode; } set { if (SetPropertyUtility.SetStruct(ref m_SelfHorizontalFitMode, value)) SetDirty(); } }

        [SerializeField] protected FitMode m_SelfVerticalFitMode = FitMode.Unconstrained;

        public FitMode SelfVerticalFit { get { return m_SelfVerticalFitMode; } set { if (SetPropertyUtility.SetStruct(ref m_SelfVerticalFitMode, value)) SetDirty(); } }

        [SerializeField] protected float m_Spacing = 0;

        /// <summary>
        /// The spacing to use between layout elements in the layout group.
        /// </summary>
        public float spacing { get { return m_Spacing; } set { SetProperty(ref m_Spacing, value); } }

        [SerializeField] protected bool m_ChildForceExpandWidth = true;

        /// <summary>
        /// Whether to force the children to expand to fill additional available horizontal space.
        /// </summary>
        public bool childForceExpandWidth { get { return m_ChildForceExpandWidth; } set { SetProperty(ref m_ChildForceExpandWidth, value); } }

        [SerializeField] protected bool m_ChildForceExpandHeight = true;

        /// <summary>
        /// Whether to force the children to expand to fill additional available vertical space.
        /// </summary>
        public bool childForceExpandHeight { get { return m_ChildForceExpandHeight; } set { SetProperty(ref m_ChildForceExpandHeight, value); } }

        [SerializeField] protected bool m_ChildControlWidth = true;

        /// <summary>
        /// Returns true if the Layout Group controls the widths of its children. Returns false if children control their own widths.
        /// </summary>
        /// <remarks>
        /// If set to false, the layout group will only affect the positions of the children while leaving the widths untouched. The widths of the children can be set via the respective RectTransforms in this case.
        ///
        /// If set to true, the widths of the children are automatically driven by the layout group according to their respective minimum, preferred, and flexible widths. This is useful if the widths of the children should change depending on how much space is available.In this case the width of each child cannot be set manually in the RectTransform, but the minimum, preferred and flexible width for each child can be controlled by adding a LayoutElement component to it.
        /// </remarks>
        public bool childControlWidth { get { return m_ChildControlWidth; } set { SetProperty(ref m_ChildControlWidth, value); } }

        [SerializeField] protected bool m_ChildControlHeight = true;

        /// <summary>
        /// Returns true if the Layout Group controls the heights of its children. Returns false if children control their own heights.
        /// </summary>
        /// <remarks>
        /// If set to false, the layout group will only affect the positions of the children while leaving the heights untouched. The heights of the children can be set via the respective RectTransforms in this case.
        ///
        /// If set to true, the heights of the children are automatically driven by the layout group according to their respective minimum, preferred, and flexible heights. This is useful if the heights of the children should change depending on how much space is available.In this case the height of each child cannot be set manually in the RectTransform, but the minimum, preferred and flexible height for each child can be controlled by adding a LayoutElement component to it.
        /// </remarks>
        public bool childControlHeight { get { return m_ChildControlHeight; } set { SetProperty(ref m_ChildControlHeight, value); } }

        [SerializeField] protected bool m_ChildScaleWidth = false;

        /// <summary>
        /// Whether children widths are scaled by their x scale.
        /// </summary>
        public bool childScaleWidth { get { return m_ChildScaleWidth; } set { SetProperty(ref m_ChildScaleWidth, value); } }

        [SerializeField] protected bool m_ChildScaleHeight = false;

        /// <summary>
        /// Whether children heights are scaled by their y scale.
        /// </summary>
        public bool childScaleHeight { get { return m_ChildScaleHeight; } set { SetProperty(ref m_ChildScaleHeight, value); } }


        /// <summary>
        /// Calculate and apply the horizontal component of the size to the RectTransform
        /// </summary>
        public override void SetLayoutHorizontal()
        {
            m_Tracker.Clear();
            HandleSelfFittingAlongAxis(0);
        }

        /// <summary>
        /// Calculate and apply the vertical component of the size to the RectTransform
        /// </summary>
        public override void SetLayoutVertical()
        {
            HandleSelfFittingAlongAxis(1);
        }

        private void HandleSelfFittingAlongAxis(int axis)
        {
            FitMode fitting = (axis == 0 ? SelfHorizontalFit : SelfVerticalFit);
            if (fitting == FitMode.Unconstrained)
            {
                // Keep a reference to the tracked transform, but don't control its properties:
                m_Tracker.Add(this, rectTransform, DrivenTransformProperties.None);
                return;
            }

            m_Tracker.Add(this, rectTransform, (axis == 0 ? DrivenTransformProperties.SizeDeltaX : DrivenTransformProperties.SizeDeltaY));

            // Set size to min or preferred size
            if (fitting == FitMode.MinSize)
                rectTransform.SetSizeWithCurrentAnchors((RectTransform.Axis)axis, LayoutUtility.GetMinSize(rectTransform, axis));
            else
                rectTransform.SetSizeWithCurrentAnchors((RectTransform.Axis)axis, LayoutUtility.GetPreferredSize(rectTransform, axis));
        }

        /// <summary>
        /// Calculate the layout element properties for this layout element along the given axis.
        /// </summary>
        /// <param name="axis">The axis to calculate for. 0 is horizontal and 1 is vertical.</param>
        /// <param name="isVertical">Is this group a vertical group?</param>
        protected void CalcAlongAxis(int axis, bool isVertical)
        {
            float combinedPadding = (axis == 0 ? padding.horizontal : padding.vertical);
            bool controlSize = (axis == 0 ? m_ChildControlWidth : m_ChildControlHeight);
            bool useScale = (axis == 0 ? m_ChildScaleWidth : m_ChildScaleHeight);
            bool childForceExpandSize = (axis == 0 ? m_ChildForceExpandWidth : m_ChildForceExpandHeight);

            float totalMin = combinedPadding;
            float totalPreferred = combinedPadding;
            float totalFlexible = 0;

            bool alongOtherAxis = (isVertical ^ (axis == 1));
            for (int i = 0; i < rectChildren.Count; i++)
            {
                RectTransform child = rectChildren[i];
                float min, preferred, flexible;
                GetChildSizes(child, axis, controlSize, childForceExpandSize, out min, out preferred, out flexible);
                
                if (useScale)
                {
                    float scaleFactor = child.localScale[axis];
                    min *= scaleFactor;
                    preferred *= scaleFactor;
                    flexible *= scaleFactor;
                }

                if (alongOtherAxis)
                {
                    totalMin = Mathf.Max(min + combinedPadding, totalMin);
                    totalPreferred = Mathf.Max(preferred + combinedPadding, totalPreferred);
                    totalFlexible = Mathf.Max(flexible, totalFlexible);
                }
                else
                {
                    totalMin += min + spacing;
                    totalPreferred += preferred + spacing;

                    // Increment flexible size with element's flexible size.
                    totalFlexible += flexible;
                }
            }

            if (!alongOtherAxis && rectChildren.Count > 0)
            {
                totalMin -= spacing;
                totalPreferred -= spacing;
            }
            totalPreferred = Mathf.Max(totalMin, totalPreferred);
            SetLayoutInputForAxis(totalMin, totalPreferred, totalFlexible, axis);
        }

        /// <summary>
        /// Set the positions and sizes of the child layout elements for the given axis.
        /// </summary>
        /// <param name="axis">The axis to handle. 0 is horizontal and 1 is vertical.</param>
        /// <param name="isVertical">Is this group a vertical group?</param>
        protected void SetChildrenAlongAxis(int axis, bool isVertical)
        {
            float size = rectTransform.rect.size[axis];
            bool controlSize = (axis == 0 ? m_ChildControlWidth : m_ChildControlHeight);
            bool useScale = (axis == 0 ? m_ChildScaleWidth : m_ChildScaleHeight);
            bool childForceExpandSize = (axis == 0 ? m_ChildForceExpandWidth : m_ChildForceExpandHeight);
            float alignmentOnAxis = GetAlignmentOnAxis(axis);

            bool alongOtherAxis = (isVertical ^ (axis == 1));
            if (alongOtherAxis)
            {
                float innerSize = size - (axis == 0 ? padding.horizontal : padding.vertical);
                for (int i = 0; i < rectChildren.Count; i++)
                {
                    RectTransform child = rectChildren[i];
                    float min, preferred, flexible;
                    GetChildSizes(child, axis, controlSize, childForceExpandSize, out min, out preferred, out flexible);

                    //Debug.LogErrorFormat("min:{0} preferred:{1} flexible:{2}", min, preferred, flexible);

                    float scaleFactor = useScale ? child.localScale[axis] : 1f;
                    float requiredSpace = Mathf.Clamp(innerSize, min, flexible > 0 ? size : preferred);
                    float startOffset = GetStartOffset(axis, requiredSpace * scaleFactor);
                    if (controlSize)
                    {
                        SetChildAlongAxisWithScale(child, axis, startOffset, requiredSpace, scaleFactor);
                    }
                    else
                    {
                        float offsetInCell = (requiredSpace - child.sizeDelta[axis]) * alignmentOnAxis;
                        SetChildAlongAxisWithScale(child, axis, startOffset + offsetInCell, scaleFactor);
                    }
                }
            }
           
        }

        private void GetChildSizes(RectTransform child, int axis, bool controlSize, bool childForceExpand,
           out float min, out float preferred, out float flexible)
        {
            if (!controlSize)
            {
                min = child.sizeDelta[axis];
                preferred = min;
                flexible = 0;
            }
            else
            {
                min = LayoutUtility.GetMinSize(child, axis);
                preferred = LayoutUtility.GetPreferredSize(child, axis);
                flexible = LayoutUtility.GetFlexibleSize(child, axis);
            }

            if (childForceExpand)
                flexible = Mathf.Max(flexible, 1);
        }


        protected Vector2 GetChildrenAnchoredPositionAlongAxis(int axis, int childIndex)
        {
            Vector2 result = default;

            float size = rectTransform.rect.size[axis];

            bool controlSize = (axis == 0 ? m_ChildControlWidth : m_ChildControlHeight);
            bool useScale = (axis == 0 ? m_ChildScaleWidth : m_ChildScaleHeight);
            bool childForceExpandSize = (axis == 0 ? m_ChildForceExpandWidth : m_ChildForceExpandHeight);
            float alignmentOnAxis = GetAlignmentOnAxis(axis);

            float pos = (axis == 0 ? padding.left : padding.top);
            float itemFlexibleMultiplier = 0;
            float surplusSpace = size - GetTotalPreferredSize(axis);

            //Debug.LogError(GetTotalPreferredSize(axis));

            if (surplusSpace > 0 && ChildrenFit == FitMode.Unconstrained)
            {
                if (GetTotalFlexibleSize(axis) == 0)
                    pos = GetStartOffset(axis, GetTotalPreferredSize(axis) - (axis == 0 ? padding.horizontal : padding.vertical));
                else if (GetTotalFlexibleSize(axis) > 0)
                    itemFlexibleMultiplier = surplusSpace / GetTotalFlexibleSize(axis);
            }

            float minMaxLerp = 0;
            if (GetTotalMinSize(axis) != GetTotalPreferredSize(axis) && (ChildrenFit == FitMode.Unconstrained | ChildrenFit == FitMode.PreferredSize))
                minMaxLerp = Mathf.Clamp01((size - GetTotalMinSize(axis)) / (GetTotalPreferredSize(axis) - GetTotalMinSize(axis)));

            for (int i = 0; i < rectChildren.Count; i++)
            {
                RectTransform child = rectChildren[i];
                float min, preferred, flexible;
                //Debug.LogError(child.GetComponent<HorizontalLayoutSizeFitter>().minWidth);
                GetChildSizes(child, axis, controlSize, childForceExpandSize, out min, out preferred, out flexible);
                float scaleFactor = useScale ? child.localScale[axis] : 1f;

                float childSize = Mathf.Lerp(min, preferred, minMaxLerp);
                childSize += flexible * itemFlexibleMultiplier;

                //if(min == 60)
                //Debug.LogErrorFormat("min:{0} preferred:{1} flexible:{2} childSize:{3}", min, preferred, flexible, childSize);

                if (i == childIndex)
                {
                    if (controlSize)
                    {
                        result = GetChildPosAlongAxis(child, axis, pos, childSize, scaleFactor);
                        break;
                    }
                    else
                    {
                        float offsetInCell = (childSize - child.sizeDelta[axis]) * alignmentOnAxis;
                        result = GetChildPosAlongAxis(child, axis, pos + offsetInCell, scaleFactor);
                        break;
                    }
                }

                pos += childSize * scaleFactor + spacing;
            }

            //Debug.LogErrorFormat("_____________________{0}", result);

            return result;
        }

        /// <summary>
        /// Get the position and size of a child layout element along the given axis.
        /// </summary>
        /// <param name="rect">The RectTransform of the child layout element.</param>
        /// <param name="axis">The axis to set the position and size along. 0 is horizontal and 1 is vertical.</param>
        /// <param name="pos">The position from the left side or top.</param>
        protected Vector2 GetChildPosAlongAxis(RectTransform rect, int axis, float pos, float scaleFactor)
        {
            Vector2 anchoredPosition = rect.anchoredPosition;
            anchoredPosition[axis] = (axis == 0) ? (pos + rect.sizeDelta[axis] * rect.pivot[axis] * scaleFactor) : (-pos - rect.sizeDelta[axis] * (1f - rect.pivot[axis]) * scaleFactor);
            return anchoredPosition;
        }

        /// <summary>
        /// Get the position and size of a child layout element along the given axis.
        /// </summary>
        /// <param name="rect">The RectTransform of the child layout element.</param>
        /// <param name="axis">The axis to set the position and size along. 0 is horizontal and 1 is vertical.</param>
        /// <param name="pos">The position from the left side or top.</param>
        /// <param name="size">The size.</param>
        protected Vector2 GetChildPosAlongAxis(RectTransform rect, int axis, float pos, float size, float scaleFactor)
        {
            Vector2 sizeDelta = rect.sizeDelta;
            sizeDelta[axis] = size;
            rect.sizeDelta = sizeDelta;

            Vector2 anchoredPosition = rect.anchoredPosition;
            anchoredPosition[axis] = (axis == 0) ? (pos + size * rect.pivot[axis] * scaleFactor) : (-pos - size * (1f - rect.pivot[axis]) * scaleFactor);
            return anchoredPosition;
        }


        public virtual Vector2 GetAnchoredPosFor(int childIndx)
        {
            return default;
        }

        public void SetItDirty()
        {
            SetDirty();
        }

#if UNITY_EDITOR
        protected override void Reset()
        {
            base.Reset();

            // For new added components we want these to be set to false,
            // so that the user's sizes won't be overwritten before they
            // have a chance to turn these settings off.
            // However, for existing components that were added before this
            // feature was introduced, we want it to be on be default for
            // backwardds compatibility.
            // Hence their default value is on, but we set to off in reset.
            m_ChildControlWidth = false;
            m_ChildControlHeight = false;
        }

        private int m_Capacity = 10;
        private Vector2[] m_Sizes = new Vector2[10];

        protected virtual void Update()
        {
            if (Application.isPlaying)
                return;

            int count = transform.childCount;

            if (count > m_Capacity)
            {
                if (count > m_Capacity * 2)
                    m_Capacity = count;
                else
                    m_Capacity *= 2;

                m_Sizes = new Vector2[m_Capacity];
            }

            // If children size change in editor, update layout (case 945680 - Child GameObjects in a Horizontal/Vertical Layout Group don't display their correct position in the Editor)
            bool dirty = false;
            for (int i = 0; i < count; i++)
            {
                RectTransform t = transform.GetChild(i) as RectTransform;
                if (t != null && t.sizeDelta != m_Sizes[i])
                {
                    dirty = true;
                    m_Sizes[i] = t.sizeDelta;
                }
            }

            if (dirty)
                LayoutRebuilder.MarkLayoutForRebuild(transform as RectTransform);
        }

#endif
    }
}
