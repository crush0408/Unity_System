using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;


[ExecuteInEditMode] // 에디트 모드에서도 실행
public class NodeBlock : MonoBehaviour
{
    public Node node;

    // Editor Code Start
    SpriteRenderer sr;
    Color myColor;

    private void Awake()
    {
        sr = GetComponent<SpriteRenderer>();
    }
    private void Update()
    {
#if UNITY_EDITOR
        if(EditorApplication.isPlaying == false)
        {
            myColor = isWall ? Color.red : Color.white;
            sr.color = myColor;
        }
#endif
    }

    public void ColorSet(bool way)
    {
        sr.color = way == true ? Color.yellow : myColor; 
    }

    // Editor Code End



    public bool isWall;
}
