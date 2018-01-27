using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

using UnityEditor;

using System;
[CustomEditor(typeof(StyleManager))]
public class StyleManagerEditor : Editor
{
    private List<Color> _colorList = new List<Color>();

    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();
        var targetScript = (StyleManager)target;

       //if (GUILayout.Button("Load Package"))
       // {
       //     var colors = targetScript.ActivePackage.GetColors();
       //     _colorList.Clear();

       //     foreach (Color clr in colors)
       //     {
       //         _colorList.Add(clr);
       //     }

       // }
       // EditorGUILayout.BeginHorizontal();
       //foreach (Color clr in _colorList)
       // {
       //     EditorGUILayout.ColorField(clr);
       // }
       // EditorGUILayout.EndHorizontal();
 
    }
}
