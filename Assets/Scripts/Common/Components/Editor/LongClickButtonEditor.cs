using System;
using UnityEngine;
using UnityEngine.UI;

namespace UnityEditor.UI
{
    /// <summary>
    ///   <para>Custom Editor for the Button Component.</para>
    /// </summary>
    [CanEditMultipleObjects, CustomEditor(typeof(LongClickButton), true)]
    public class LongClickButtonEditor : SelectableEditor
    {
        private SerializedProperty m_OnClickProperty;
        private SerializedProperty m_OnLongClickProperty;
        private SerializedProperty m_SingleClickableProperty;
        protected override void OnEnable()
        {
            base.OnEnable();
            this.m_OnClickProperty = base.serializedObject.FindProperty("m_onSingleClick");
            this.m_OnLongClickProperty = base.serializedObject.FindProperty("m_onLongClick");
            this.m_SingleClickableProperty = base.serializedObject.FindProperty("m_singleClickable");
        }

        /// <summary>
        ///   <para>See Editor.OnInspectorGUI.</para>
        /// </summary>
        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();
            EditorGUILayout.Space();
            base.serializedObject.Update();
            EditorGUILayout.PropertyField(this.m_SingleClickableProperty, new GUILayoutOption[0]);
            if (m_SingleClickableProperty.boolValue)
                EditorGUILayout.PropertyField(this.m_OnClickProperty, new GUILayoutOption[0]);
            EditorGUILayout.PropertyField(this.m_OnLongClickProperty, new GUILayoutOption[0]);
            base.serializedObject.ApplyModifiedProperties();
        }
    }
}