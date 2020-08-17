using System;
using UnityEngine;
using UnityEngine.UI;

namespace UnityEditor.UI
{
    /// <summary>
    ///   <para>Custom Editor for the Button Component.</para>
    /// </summary>
    [CanEditMultipleObjects, CustomEditor(typeof(DoubleClickButton), true)]
    public class DoubleClickButtonEditor : SelectableEditor
    {
        private SerializedProperty m_OnClickProperty;

        protected override void OnEnable()
        {
            base.OnEnable();
            this.m_OnClickProperty = base.serializedObject.FindProperty("m_onDoubleClick");
        }

        /// <summary>
        ///   <para>See Editor.OnInspectorGUI.</para>
        /// </summary>
        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();
            EditorGUILayout.Space();
            base.serializedObject.Update();
            EditorGUILayout.PropertyField(this.m_OnClickProperty, new GUILayoutOption[0]);
            base.serializedObject.ApplyModifiedProperties();
        }
    }
}