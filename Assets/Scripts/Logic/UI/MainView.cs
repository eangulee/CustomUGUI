using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainView : MonoBehaviour
{
    #region Event Handler
    public void OnDoubleClickBtnClickHandler()
    {
        Debug.Log("OnDoubleClickBtnClickHandler");
    }

    public void OnLongClickBtnClickHandler()
    {
        Debug.Log("OnLongClickBtnClickHandler");
    }

    public void OnTransparentBtnClickHandler()
    {
        Debug.Log("OnTransparentBtnClickHandler");
    }
    #endregion
}
