using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using UnityEditor;




public class StyleManager : MonoBehaviour {

    public static StyleManager Instance
    {
        get
        {
            if (instance != null)
                return instance;

            instance = FindObjectOfType<StyleManager>();

            if (instance != null)
                return instance;

            Create();

            return instance;
        }
    }

    public ColorPalettePackage ActivePackage;
    public Texture2D WhiteSquareTexture;

    protected static StyleManager instance;

    public static StyleManager Create()
    {
        GameObject sceneControllerGameObject = new GameObject("StyleManager");
        instance = sceneControllerGameObject.AddComponent<StyleManager>();
        return instance;
       
    }
}
