using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(EtchoSketch))]
public class EtchoSketchEditor : Editor {

    public override void OnInspectorGUI ()
    {
        var targetScript = (EtchoSketch)target;

        DrawDefaultInspector();
    }

}
