using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.Serialization;
using UnityEngine.UI;

//长按按钮
[AddComponentMenu("UI/LongClickButton")]
public class LongClickButton : Button
{
    /// <summary>
    ///   <para>Function definition for a single button click event.</para>
    /// </summary>
    [Serializable]
    public class SingleClickedEvent : UnityEvent { }
    [Serializable]
    public class LongClickEvent : UnityEvent { }

    //这里使用新定义的SingleClickedEvent，避免跟Button冲突
    [FormerlySerializedAs("onSingleClick"), SerializeField]
    private SingleClickedEvent m_onSingleClick = new SingleClickedEvent();
    public SingleClickedEvent onSingleClick
    {
        get { return m_onSingleClick; }
        set { m_onSingleClick = value; }
    }


    [FormerlySerializedAs("onLongClick"), SerializeField]
    private LongClickEvent m_onLongClick = new LongClickEvent();
    public LongClickEvent onLongClick
    {
        get { return m_onLongClick; }
        set { m_onLongClick = value; }
    }

    private DateTime m_firstTime = default(DateTime);
    private DateTime m_secondTime = default(DateTime);

    [FormerlySerializedAs("m_singleClickable"), SerializeField]
    public bool m_singleClickable = false;

    private void Press()
    {
        if (null != onLongClick)
            onLongClick.Invoke();
        resetTime();
    }

    private void SingleClick()
    {
        if (null != m_onSingleClick)
            m_onSingleClick.Invoke();
        resetTime();
    }

    public override void OnPointerDown(PointerEventData eventData)
    {
        base.OnPointerDown(eventData);
        if (m_firstTime.Equals(default(DateTime)))
            m_firstTime = DateTime.Now;
        //Debug.Log("OnPointerDown");
    }

    public override void OnPointerUp(PointerEventData eventData)
    {
        base.OnPointerUp(eventData);
        // 在鼠标抬起的时候进行事件触发，时差大于600ms触发
        if (!m_firstTime.Equals(default(DateTime)))
            m_secondTime = DateTime.Now;
        if (!m_firstTime.Equals(default(DateTime)) && !m_secondTime.Equals(default(DateTime)))
        {
            var intervalTime = m_secondTime - m_firstTime;
            int milliSeconds = intervalTime.Seconds * 1000 + intervalTime.Milliseconds;
            if (milliSeconds > 600)
                Press();
            else
            {
                if (m_singleClickable)
                {
                    SingleClick();
                }
                else
                    resetTime();
            }
        }
    }

    public override void OnPointerExit(PointerEventData eventData)
    {
        base.OnPointerExit(eventData);
        resetTime();
    }

    private void resetTime()
    {
        m_firstTime = default(DateTime);
        m_secondTime = default(DateTime);
    }
}