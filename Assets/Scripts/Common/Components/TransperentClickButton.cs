using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Serialization;
using UnityEngine.UI;

//透明按钮
[AddComponentMenu("UI/TransperentClickButton")]
public class TransperentClickButton : Button
{
    /// <summary>
    ///   <para>Function definition for a single button click event.</para>
    /// </summary>
    [Serializable]
    public class SingleClickedEvent : UnityEvent { }
    [FormerlySerializedAs("onSingleClick"), SerializeField]
    private SingleClickedEvent m_onSingleClick = new SingleClickedEvent();

    //protected TransperentClickButton() {
    //    useLegacyMeshGeneration = false;
    //}

    //protected override void OnPopulateMesh(VertexHelper toFill)
    //{
    //    toFill.Clear();
    //}

public SingleClickedEvent onSingleClick
    {
        get { return m_onSingleClick; }
        set { m_onSingleClick = value; }
    }

    private void Press()
    {
        if (null != m_onSingleClick)
            m_onSingleClick.Invoke();
    }
}
