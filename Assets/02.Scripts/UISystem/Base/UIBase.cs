using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class UIBase : MonoBehaviour
{
    public string Key => GetType().Name; // UI 딕셔너리에 들어갈 Key, Key는 해당 스크립트의 이름
    public abstract void Init();
    public virtual void Show()
    {
        gameObject.SetActive(true);
    }
    public virtual void Hide()
    {
        gameObject.SetActive(false);
    }
}
