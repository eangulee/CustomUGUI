using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SpringGUIDefaultControls
{
    public static GameObject CreateDoubleClickButton(DefaultControls.Resources resources)
    {
        GameObject dcButton = DefaultControls.CreateButton(convertToDefaultResources(resources));
        dcButton.name = "DoubleClickButton";
        dcButton.transform.Find("Text").GetComponent<Text>().text = "双击按钮";
        Object.DestroyImmediate(dcButton.GetComponent<Button>());
        dcButton.AddComponent<DoubleClickButton>();
        return dcButton;
    }

    public static GameObject CreateLongClickButton(DefaultControls.Resources resources)
    {
        GameObject lcButton = DefaultControls.CreateButton(convertToDefaultResources(resources));
        lcButton.name = "LongClickButton";
        lcButton.transform.Find("Text").GetComponent<Text>().text = "长按按钮";
        Object.DestroyImmediate(lcButton.GetComponent<Button>());
        lcButton.AddComponent<LongClickButton>();
        return lcButton;
    }

    private static DefaultControls.Resources convertToDefaultResources(DefaultControls.Resources resources)
    {
        return resources;
    }
}