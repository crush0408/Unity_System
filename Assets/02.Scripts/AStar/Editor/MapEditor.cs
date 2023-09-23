using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using Unity.VisualScripting;

public class MapEditor : EditorWindow
{
    // Instance;
    public static MapEditor Instance;
    private static MapEditor window;


    // GUI (Graphic User Interface)
    GUIStyle basicStyle;
    GUIStyle headerStyle;
    int controlID;
    Vector2 scrollPos;
    Tool lasTool;


    [MenuItem("MapEditor/Open Map Editor &m", false, 1)]
    private static void Init()
    {
        // Create Window
        window = GetWindow<MapEditor>();
        window.Show();
        window.minSize = new Vector2(200, 315);
        window.titleContent = new GUIContent("MapEditor", "내가 만든 맵툴입니다");
    }
    private void OnEnable() // Editor Open
    {
        Debug.Log("Editor Open");
        // Style
        // Font Load
        Font[] loadFont = Resources.LoadAll<Font>("Fonts/Select");
        Debug.Log(loadFont.Length == 1 ? loadFont[0].name : $"너무 많은 폰트가 선택 되어 있습니다. 현재 폰트는 {loadFont[0].name}입니다.");

        // Basic
        basicStyle = new GUIStyle();
        basicStyle.font = loadFont[0];
        basicStyle.fontStyle = FontStyle.Normal;
        basicStyle.normal.textColor = Color.white;
        basicStyle.onHover.textColor = Color.blue;
        basicStyle = new GUIStyle(basicStyle);

        // Header
        headerStyle = new GUIStyle();
        headerStyle.font = loadFont[0];
        headerStyle.fontStyle = FontStyle.Bold;
        headerStyle.normal.textColor = Color.white;
        headerStyle.hover.textColor = Color.blue;
        headerStyle = new GUIStyle(headerStyle);


        // Instance
        Instance = this;

        // Tool Setting
        lasTool = Tools.current;
        Tools.current = Tool.None;



        // Scene GUI
        SceneView.duringSceneGui += SceneGUI;
    }
    private void OnGUI()
    {
        EditorGUILayout.BeginVertical();
        EditorGUILayout.LabelField("Test Label", headerStyle);
        LabelField("Test", basicStyle);
        EditorGUILayout.EndVertical();
    }

    private void SceneGUI(SceneView sceneView)
    {
        Event e = Event.current;
        controlID = GUIUtility.GetControlID(FocusType.Passive);
        if (e.type == EventType.Layout)
        {
            HandleUtility.AddDefaultControl(controlID);
        }
        switch (e.type)
        {
            case EventType.MouseDown:
                {
                    if (e.button == 0) // Left Click
                    {
                        //mouseDown = true;
                    }
                    break;
                }
            case EventType.MouseUp:
                {
                    if (e.button == 0) // Left Up
                    {
                        //mouseDown = false;
                    }
                    break;
                }
        }

        Repaint();
    }


    // Label Func
    private void LabelField(string label, GUIStyle style)
    {
        EditorGUILayout.LabelField(label, style);
    }
}
