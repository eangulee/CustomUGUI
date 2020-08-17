using UnityEditor;
using UnityEditor.Experimental.SceneManagement;
using UnityEditor.SceneManagement;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SpringGUIMenuOptions
{
    [MenuItem("GameObject/UI/Buttons/DoubleClickButton", false, 2065)]
    public static void AddDoubleClickButton(MenuCommand menuCommand)
    {
        GameObject dcButton = SpringGUIDefaultControls.CreateDoubleClickButton(GetStandardResources());
        //这个方法对创建的UI进行配置
        //比如创建唯一的名字
        //根据是否选中画布创建父级
        //是UGUI源码的一部分，我们只需要复制到我们脚本中来即可，稍后我会附上源码
        PlaceUIElementRoot(dcButton, menuCommand);
    }

    [MenuItem("GameObject/UI/Buttons/LongClickButton", false, 2066)]
    public static void AddLongClickButton(MenuCommand menuCommand)
    {
        GameObject lcButton = SpringGUIDefaultControls.CreateLongClickButton(GetStandardResources());
        PlaceUIElementRoot(lcButton, menuCommand);
    }

    private static DefaultControls.Resources s_StandardResources;
    // UnityEditor.UI.MenuOptions
    private static DefaultControls.Resources GetStandardResources()
    {
        if (SpringGUIMenuOptions.s_StandardResources.standard == null)
        {
            SpringGUIMenuOptions.s_StandardResources.standard = AssetDatabase.GetBuiltinExtraResource<Sprite>("UI/Skin/UISprite.psd");
            SpringGUIMenuOptions.s_StandardResources.background = AssetDatabase.GetBuiltinExtraResource<Sprite>("UI/Skin/Background.psd");
            SpringGUIMenuOptions.s_StandardResources.inputField = AssetDatabase.GetBuiltinExtraResource<Sprite>("UI/Skin/InputFieldBackground.psd");
            SpringGUIMenuOptions.s_StandardResources.knob = AssetDatabase.GetBuiltinExtraResource<Sprite>("UI/Skin/Knob.psd");
            SpringGUIMenuOptions.s_StandardResources.checkmark = AssetDatabase.GetBuiltinExtraResource<Sprite>("UI/Skin/Checkmark.psd");
            SpringGUIMenuOptions.s_StandardResources.dropdown = AssetDatabase.GetBuiltinExtraResource<Sprite>("UI/Skin/DropdownArrow.psd");
            SpringGUIMenuOptions.s_StandardResources.mask = AssetDatabase.GetBuiltinExtraResource<Sprite>("UI/Skin/UIMask.psd");
        }
        return SpringGUIMenuOptions.s_StandardResources;
    }

    // UnityEditor.UI.MenuOptions
    private static void PlaceUIElementRoot(GameObject element, MenuCommand menuCommand)
    {
        GameObject gameObject = menuCommand.context as GameObject;
        bool flag = true;
        if (gameObject == null)
        {
            gameObject = SpringGUIMenuOptions.GetOrCreateCanvasGameObject();
            flag = false;
            PrefabStage currentPrefabStage = PrefabStageUtility.GetCurrentPrefabStage();
            if (currentPrefabStage != null && !currentPrefabStage.IsPartOfPrefabContents(gameObject))
            {
                gameObject = currentPrefabStage.prefabContentsRoot;
            }
        }
        if (gameObject.GetComponentInParent<Canvas>() == null)
        {
            GameObject gameObject2 = SpringGUIMenuOptions.CreateNewUI();
            gameObject2.transform.SetParent(gameObject.transform, false);
            gameObject = gameObject2;
        }
        SceneManager.MoveGameObjectToScene(element, gameObject.scene);
        Undo.RegisterCreatedObjectUndo(element, "Create " + element.name);
        if (element.transform.parent == null)
        {
            Undo.SetTransformParent(element.transform, gameObject.transform, "Parent " + element.name);
        }
        GameObjectUtility.EnsureUniqueNameForSibling(element);
        Undo.SetCurrentGroupName("Create " + element.name);
        GameObjectUtility.SetParentAndAlign(element, gameObject);
        if (!flag)
        {
            SpringGUIMenuOptions.SetPositionVisibleinSceneView(gameObject.GetComponent<RectTransform>(), element.GetComponent<RectTransform>());
        }
        Selection.activeGameObject = element;
    }

    // UnityEditor.UI.MenuOptions
    public static GameObject GetOrCreateCanvasGameObject()
    {
        GameObject activeGameObject = Selection.activeGameObject;
        Canvas canvas = (!(activeGameObject != null)) ? null : activeGameObject.GetComponentInParent<Canvas>();
        GameObject result;
        if (SpringGUIMenuOptions.IsValidCanvas(canvas))
        {
            result = canvas.gameObject;
        }
        else
        {
            Canvas[] array = StageUtility.GetCurrentStageHandle().FindComponentsOfType<Canvas>();
            for (int i = 0; i < array.Length; i++)
            {
                if (SpringGUIMenuOptions.IsValidCanvas(array[i]))
                {
                    result = array[i].gameObject;
                    return result;
                }
            }
            result = SpringGUIMenuOptions.CreateNewUI();
        }
        return result;
    }

    // UnityEditor.UI.MenuOptions
    private static bool IsValidCanvas(Canvas canvas)
    {
        return !(canvas == null) && canvas.gameObject.activeInHierarchy && !EditorUtility.IsPersistent(canvas) && (canvas.hideFlags & HideFlags.HideInHierarchy) == HideFlags.None && !(StageUtility.GetStageHandle(canvas.gameObject) != StageUtility.GetCurrentStageHandle());
    }

    // UnityEditor.UI.MenuOptions
    public static GameObject CreateNewUI()
    {
        GameObject gameObject = new GameObject("Canvas");
        gameObject.layer = LayerMask.NameToLayer("UI");
        Canvas canvas = gameObject.AddComponent<Canvas>();
        canvas.renderMode = RenderMode.ScreenSpaceOverlay;
        gameObject.AddComponent<CanvasScaler>();
        gameObject.AddComponent<GraphicRaycaster>();
        StageUtility.PlaceGameObjectInCurrentStage(gameObject);
        bool flag = false;
        PrefabStage currentPrefabStage = PrefabStageUtility.GetCurrentPrefabStage();
        if (currentPrefabStage != null)
        {
            gameObject.transform.SetParent(currentPrefabStage.prefabContentsRoot.transform, false);
            flag = true;
        }
        Undo.RegisterCreatedObjectUndo(gameObject, "Create " + gameObject.name);
        if (!flag)
        {
            SpringGUIMenuOptions.CreateEventSystem(false);
        }
        return gameObject;
    }

    [MenuItem("GameObject/UI/Event System", false, 2100)]
    public static void CreateEventSystem(MenuCommand menuCommand)
    {
        GameObject parent = menuCommand.context as GameObject;
        SpringGUIMenuOptions.CreateEventSystem(true, parent);
    }

    private static void CreateEventSystem(bool select)
    {
        SpringGUIMenuOptions.CreateEventSystem(select, null);
    }

    private static void CreateEventSystem(bool select, GameObject parent)
    {
        EventSystem eventSystem = ((!(parent == null)) ? StageUtility.GetStageHandle(parent) : StageUtility.GetCurrentStageHandle()).FindComponentOfType<EventSystem>();
        if (eventSystem == null)
        {
            GameObject gameObject = new GameObject("EventSystem");
            if (parent == null)
            {
                StageUtility.PlaceGameObjectInCurrentStage(gameObject);
            }
            else
            {
                GameObjectUtility.SetParentAndAlign(gameObject, parent);
            }
            eventSystem = gameObject.AddComponent<EventSystem>();
            gameObject.AddComponent<StandaloneInputModule>();
            Undo.RegisterCreatedObjectUndo(gameObject, "Create " + gameObject.name);
        }
        if (select && eventSystem != null)
        {
            Selection.activeGameObject = eventSystem.gameObject;
        }
    }

    // UnityEditor.UI.MenuOptions
    private static void SetPositionVisibleinSceneView(RectTransform canvasRTransform, RectTransform itemTransform)
    {
        SceneView lastActiveSceneView = SceneView.lastActiveSceneView;
        if (!(lastActiveSceneView == null) && !(lastActiveSceneView.camera == null))
        {
            Camera camera = lastActiveSceneView.camera;
            Vector3 zero = Vector3.zero;
            Vector2 vector;
            if (RectTransformUtility.ScreenPointToLocalPointInRectangle(canvasRTransform, new Vector2((float)(camera.pixelWidth / 2), (float)(camera.pixelHeight / 2)), camera, out vector))
            {
                vector.x += canvasRTransform.sizeDelta.x * canvasRTransform.pivot.x;
                vector.y += canvasRTransform.sizeDelta.y * canvasRTransform.pivot.y;
                vector.x = Mathf.Clamp(vector.x, 0f, canvasRTransform.sizeDelta.x);
                vector.y = Mathf.Clamp(vector.y, 0f, canvasRTransform.sizeDelta.y);
                zero.x = vector.x - canvasRTransform.sizeDelta.x * itemTransform.anchorMin.x;
                zero.y = vector.y - canvasRTransform.sizeDelta.y * itemTransform.anchorMin.y;
                Vector3 vector2;
                vector2.x = canvasRTransform.sizeDelta.x * (0f - canvasRTransform.pivot.x) + itemTransform.sizeDelta.x * itemTransform.pivot.x;
                vector2.y = canvasRTransform.sizeDelta.y * (0f - canvasRTransform.pivot.y) + itemTransform.sizeDelta.y * itemTransform.pivot.y;
                Vector3 vector3;
                vector3.x = canvasRTransform.sizeDelta.x * (1f - canvasRTransform.pivot.x) - itemTransform.sizeDelta.x * itemTransform.pivot.x;
                vector3.y = canvasRTransform.sizeDelta.y * (1f - canvasRTransform.pivot.y) - itemTransform.sizeDelta.y * itemTransform.pivot.y;
                zero.x = Mathf.Clamp(zero.x, vector2.x, vector3.x);
                zero.y = Mathf.Clamp(zero.y, vector2.y, vector3.y);
            }
            itemTransform.anchoredPosition = zero;
            itemTransform.localRotation = Quaternion.identity;
            itemTransform.localScale = Vector3.one;
        }
    }

}