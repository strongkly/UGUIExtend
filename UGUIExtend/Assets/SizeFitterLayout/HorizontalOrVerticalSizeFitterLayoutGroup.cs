using UnityEngine;
using UnityEngine.UI;
#if UNITY_EDITOR
using UnityEditor;
using UnityEditor.UI;
#endif

namespace CrazyBox.UGUIExtend
{
    public class HorizontalOrVerticalSizeFitterLayoutGroup : HorizontalOrVerticalLayoutGroup, ILayoutSelfController
    {
#if UNITY_EDITOR
        [CustomEditor(typeof(HorizontalOrVerticalSizeFitterLayoutGroup), true)]
        [CanEditMultipleObjects]
        /// <summary>
        ///   Custom Editor for the HorizontalOrVerticalLayoutGroupEditor Component.
        ///   Extend this class to write a custom editor for an HorizontalOrVerticalLayoutGroupEditor-derived component.
        /// </summary>
        public class HorizontalOrVerticalSizeFitterLayoutGroupEditor : HorizontalOrVerticalLayoutGroupEditor
        {
            SerializedProperty m_HorizontalFit;
            SerializedProperty m_VerticalFit;

            protected override void OnEnable()
            {
                base.OnEnable();
                m_HorizontalFit = serializedObject.FindProperty("m_HorizontalFit");
                m_VerticalFit = serializedObject.FindProperty("m_VerticalFit");
            }

            public override void OnInspectorGUI()
            {
                serializedObject.Update();
                EditorGUILayout.PropertyField(m_HorizontalFit, true);
                EditorGUILayout.PropertyField(m_VerticalFit, true);
                serializedObject.ApplyModifiedProperties();

                base.OnInspectorGUI();

            }
        }
#endif

        [SerializeField] protected FitMode m_HorizontalFit = FitMode.Unconstrained;

        public FitMode horizontalFit { get { return m_HorizontalFit; } set { if (SetPropertyUtility.SetStruct(ref m_HorizontalFit, value)) SetDirty(); } }


        [SerializeField] protected FitMode m_VerticalFit = FitMode.Unconstrained;

        public FitMode verticalFit { get { return m_VerticalFit; } set { if (SetPropertyUtility.SetStruct(ref m_VerticalFit, value)) SetDirty(); } }

        public override void CalculateLayoutInputVertical()
        {
            base.CalculateLayoutInputHorizontal();
        }

        public override void SetLayoutHorizontal()
        {
            HandleSelfFittingAlongAxis(0);
        }

        public override void SetLayoutVertical()
        {
            HandleSelfFittingAlongAxis(1);
        }

        protected void HandleSelfFittingAlongAxis(int axis)
        {
            FitMode fitting = (axis == 0 ? horizontalFit : verticalFit);
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
    }
}