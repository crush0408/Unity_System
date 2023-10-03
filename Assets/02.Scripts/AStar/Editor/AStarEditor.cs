using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(AStarManager))]
public class AStarEditor : Editor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();
        //DrawDefaultInspector();

        AStarManager aStarManager = (AStarManager)target;

        //if(GUILayout.Button(""))
    }
}
