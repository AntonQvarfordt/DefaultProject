using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{

    public bool UseGestureSurface;

    private InputGestureReader _gestureSurface;

    private Canvas _canvas;

    public static GameManager Instance
    {
        get
        {
            if (instance != null)
                return instance;

            instance = FindObjectOfType<GameManager>();

            if (instance != null)
                return instance;

            Create();

            return instance;
        }
    }

    protected static GameManager instance;

    public static GameManager Create()
    {
        GameObject sceneControllerGameObject = new GameObject("GameManager");
        instance = sceneControllerGameObject.AddComponent<GameManager>();

        return instance;
    }

    private void Awake()
    {
        _canvas = FindObjectOfType<Canvas>();
        _gestureSurface = FindObjectOfType<InputGestureReader>();
        if (_gestureSurface == null && UseGestureSurface)
        {
            var gestureSurface = new GameObject();
            var surfaceTransform = gestureSurface.AddComponent<RectTransform>();
            gestureSurface.transform.SetParent(_canvas.transform);
            
            surfaceTransform.localScale = Vector3.one;
            surfaceTransform.localPosition = Vector3.zero;
            var canvasRect = RectTransformUtility.PixelAdjustRect(_canvas.GetComponent<RectTransform>(), _canvas);
            surfaceTransform.anchorMax = Vector2.one;
            surfaceTransform.anchorMin = Vector2.one;
            surfaceTransform.anchoredPosition3D = new Vector3(canvasRect.position.x, canvasRect.position.y, 50);
            surfaceTransform.sizeDelta = canvasRect.size;
            var gestureReader = gestureSurface.AddComponent<InputGestureReader>();
            gestureReader.color = Color.clear;
        }
    }
}
