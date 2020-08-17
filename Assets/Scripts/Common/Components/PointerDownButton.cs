using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.Serialization;
using UnityEngine.UI;

//双击按钮
[AddComponentMenu("UI/DubleClickButton")]
public class PointerDownButton : Button
{
    [Serializable]
    public class PointerDownEvent : UnityEvent { }
}
