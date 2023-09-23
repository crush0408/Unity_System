using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class MapEditor : EditorWindow
{
    // Instance;
    public static MapEditor Instance;
    private static MapEditor window;

    // GUI (Graphic User Interface)
    GUIStyle style; // basic Style
    //GUIStyle headerStyle; // Header Style // TODO : Header Style
    int controlID;
    Vector2 scrollPos;
    Tool lasTool;



    // Map Data
    public List<GameObject> prefabList = new List<GameObject>();




    // Map Open
    [MenuItem("MapEditor/Open Map Editor &m", false, 1)]
    private static void Init()
    {
        // Create Window
        window = GetWindow<MapEditor>();
        window.Show();
        window.minSize = new Vector2(200, 315);
        window.titleContent = new GUIContent("MapEditor", "This is Custom MapEditor");
    }
    private void OnEnable() // Editor Open
    {
        Debug.Log("Editor Open");
        // Style
        style = new GUIStyle(); // Basic
        //style.font = loadFont[0];
        style.fontStyle = FontStyle.Normal;
        style.normal.textColor = Color.white;
        style.onHover.textColor = Color.blue;
        style = new GUIStyle(style);

        // TODO : Font
        //Font[] loadFont = Resources.LoadAll<Font>("Fonts/Select");
        //Debug.Log(loadFont.Length == 1 ? loadFont[0].name : $"너무 많은 폰트가 선택 되어 있습니다. 현재 폰트는 {loadFont[0].name}입니다.");


        // Header // TODO : Header Style
        //headerStyle = new GUIStyle();
        //headerStyle.font = loadFont[0];
        //headerStyle.fontStyle = FontStyle.Bold;
        //headerStyle.normal.textColor = Color.white;
        //headerStyle.hover.textColor = Color.blue;
        //headerStyle = new GUIStyle(headerStyle);


        // Instance
        Instance = this;

        // Tool Setting
        lasTool = Tools.current;
        Tools.current = Tool.None;

        // Scene GUI
        SceneView.duringSceneGui += SceneGUI;
    }
    private void OnDisable()
    {
        Tools.current = lasTool;
        SceneView.duringSceneGui -= SceneGUI;
    }
    private void OnDestroy()
    {
        Tools.current = lasTool;
        SceneView.duringSceneGui -= SceneGUI;
    }
    private void OnGUI()
    {
        EditorGUILayout.BeginVertical();
        

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
                        Debug.Log("Left Click");
                    }
                    else if(e.button == 1) // Right Click
                    {
                        Debug.Log("Right Click");
                    }
                    break;
                }
            case EventType.MouseUp:
                {
                    if (e.button == 0) // Left Up
                    {
                        Debug.Log("Left Up");
                    }
                    else if(e.button == 1) // Right Up
                    {
                        Debug.Log("Right Up");
                    }
                    break;
                }
        }

        Repaint();
    }

    private void LoadPrefab()
    {
        prefabList = SUtil.GetList(Resources.LoadAll<GameObject>("Prefabs/"));
    }
    // Label Func
    private void LabelField(string label, GUIStyle style)
    {
        EditorGUILayout.LabelField(label, style);
    }
}
