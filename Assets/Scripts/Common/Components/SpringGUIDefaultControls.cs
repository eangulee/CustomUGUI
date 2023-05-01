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

    public static GameObject CreateRollText(DefaultControls.Resources resources)
    {
        GameObject rollTxt = DefaultControls.CreateButton(convertToDefaultResources(resources));
        rollTxt.name = "auto_roll_txt";
        rollTxt.transform.Find("Text").GetComponent<Text>().text = "滚动文字";
        Object.DestroyImmediate(rollTxt.GetComponent<Button>());
        Mask mask = rollTxt.AddComponent<Mask>();
        mask.showMaskGraphic = false;
        AutoRollText autoRollText = rollTxt.AddComponent<AutoRollText>();
        autoRollText.m_text = autoRollText.GetComponentInChildren<Text>();
        autoRollText.m_text.rectTransform.anchorMax = Vector2.one * 0.5f;
        autoRollText.m_text.rectTransform.anchorMin = Vector2.one * 0.5f;
        autoRollText.m_text.rectTransform.sizeDelta = (autoRollText.transform as RectTransform).sizeDelta;
        Image image = autoRollText.GetComponent<Image>();
        image.type = Image.Type.Simple;
        image.sprite = null;
        return rollTxt;
    }

    private static DefaultControls.Resources convertToDefaultResources(DefaultControls.Resources resources)
    {
        return resources;
    }
}