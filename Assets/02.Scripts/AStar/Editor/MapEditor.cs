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
    GameObject currentPrefab;
    int selGridIdx;




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
        // Instance
        Instance = this;

        // Style
        style = new GUIStyle(); // Basic
        //style.font = loadFont[0];
        style.fontStyle = FontStyle.Normal;
        style.normal.textColor = Color.white;
        style.onHover.textColor = Color.blue;
        style = new GUIStyle(style);

        // Unity Tool Setting
        lasTool = Tools.current;
        Tools.current = Tool.None;

        // Scene GUI
        SceneView.duringSceneGui += SceneGUI; // Scene GUI Setting
    }
    private void OnDisable()
    {
        Debug.Log("Disable");
        Tools.current = lasTool; // 사용중이던 툴 재 설정해주기
        SceneView.duringSceneGui -= SceneGUI; // 씬 GUI 설정 해제 (메모리)
    }
    //private void OnDestroy() // -> Disable과 동일, but 범용성은 Disable이 더 높음으로 Disable 사용
    //{
    //    Debug.Log("Destroy");
    //    Tools.current = lasTool;
    //    SceneView.duringSceneGui -= SceneGUI;
    //}

    private void OnFocus() // Map Editor가 선택 되었을 경우
    {
        Debug.Log("MapEditor Focusing");
        LoadPrefab();
    }
    private void OnGUI() // GUI Code
    {
        EditorGUILayout.BeginVertical(); // Vertical 시작
        scrollPos = EditorGUILayout.BeginScrollView(scrollPos, false, false);
        LabelField("Select a prefab:", style);
        if(prefabList != null && prefabList.Count > 0)
        {
            GUIContent[] content = new GUIContent[prefabList.Count];
            for(int i = 0; i < prefabList.Count; i++)
            {
                if(prefabList[i] != null && prefabList[i].gameObject.name != "") // 비어있지 않고, 이름이 있다면
                {
                    content[i] = new GUIContent(prefabList[i].gameObject.name, AssetPreview.GetAssetPreview(prefabList[i].gameObject));
                }

                if(content[i] == null) // 만약 넣었는데, 비어있다면
                    content[i] = GUIContent.none; // none
            }

            EditorGUI.BeginChangeCheck(); // 변화 체크 시작

            selGridIdx = GUILayout.SelectionGrid(selGridIdx, content, 5
            , GUILayout.Height(50 * (Mathf.Ceil(prefabList.Count / (float) 5)))
            , GUILayout.Width(this.position.width - 30));

            if(EditorGUI.EndChangeCheck())
            {
                // 변경 시 코드 
            }
            currentPrefab = prefabList[selGridIdx];

            Texture2D previewImage = AssetPreview.GetAssetPreview(currentPrefab);
            GUILayout.Box(previewImage);
        }
        EditorGUILayout.EndScrollView();

        selGridIdx = EditorGUILayout.IntField(selGridIdx, style);

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
