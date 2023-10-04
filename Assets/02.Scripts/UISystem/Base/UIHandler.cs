using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIHandler : Singleton<UIHandler>
{
    private Dictionary<string, UIPanel> panelDic = new Dictionary<string, UIPanel>(); // 패널 딕셔너리
    private List<UIPanel> panelList = new List<UIPanel>(); // 패널 리스트
    private Dictionary<string, UIPopup> popupDic = new Dictionary<string, UIPopup>(); // 팝업 딕셔너리
    private List<UIPopup> popupList = new List<UIPopup>(); // 팝업 리스트

    public UIPanel startPanel; // 시작 패널

    private UIPanel currentPanel; // 현재 패널
    private UIPopup currentPopup; // 현재 팝업

    private void Awake()
    {
        UIDicInit();
    }
    private void Start()
    {
        UIDicSetting();
    }
    private void UIDicInit() // UI 가져와서 Dictionary 구성
    {
        currentPanel = null;
        currentPopup = null;
        // Panel Init
        panelDic = new Dictionary<string, UIPanel>();
        panelList = MyUtil.GetList(GetComponentsInChildren<UIPanel>());
        foreach (var panel in panelList)
        {
            panelDic.Add(panel.Key, panel); // ui.GetType().Name
        }

        // Popup
        popupDic = new Dictionary<string, UIPopup>();
        popupList = MyUtil.GetList(GetComponentsInChildren<UIPopup>());
        foreach (var popup in popupList)
        {
            popupDic.Add(popup.Key, popup);
        }
    }
    private void UIDicSetting() // 구성된 UI Dicionary로 Setting (StartPanel을 제외하고 전부 Hide)
    {
        // Popup
        foreach (var popup in popupDic) // Popup Init, Hide
        {
            popup.Value.Init();
            popup.Value.Hide();
        }
        currentPopup = null;
        // panel
        foreach (var panel in panelDic) // Panel Init, Hide
        {
            panel.Value.Init();
            panel.Value.Hide();
        }

        if (startPanel == null) // 시작 패널 예외처리
            Debug.LogError("시작 패널이 지정되어있지 않습니다.");

        // 시작 패널 세팅
        startPanel.Show();
        currentPanel = startPanel;
    }

    #region Panel 함수
    public static T GetPanel<T>() where T : UIPanel // Panel을 가져온다.
    {
        string key = typeof(T).Name;
        if (Instance.panelDic.ContainsKey(key))
        {
            return Instance.panelDic[key] as T;
        }

        return null;
    }
    public static void ShowPanel<T>() where T : UIPanel // 현재 패널을 꺼주고, 제너릭 패널을 켜준다.
    {
        string key = typeof(T).Name;
        if (Instance.panelDic.ContainsKey(key))
        {
            if (Instance.currentPanel != null)
            {
                Instance.currentPanel.Hide();
            }

            Instance.panelDic[key].Show();
            Instance.currentPanel = Instance.panelDic[key];
        }
    }
    #endregion
    #region Popup 함수
    public static T GetPopup<T>() where T : UIPopup // 제네릭에 해당하는 팝업을 가져온다.
    {
        string key = typeof(T).Name;
        if (Instance.popupDic.ContainsKey(key))
        {
            return Instance.popupDic[key] as T;
        }
        else
        {
            return null;
        }
    }
    public static void ShowPopup<T>() where T : UIPopup // 현재 팝업이 켜져있다면 꺼주고, 새로운 팝업 오픈
    {
        string key = typeof(T).Name;

        if (Instance.popupDic.ContainsKey(key))
        {
            if (Instance.currentPopup != null)
            {
                Instance.currentPopup.Hide();
            }
            Instance.popupDic[key].Show();
            Instance.currentPopup = Instance.popupDic[key];
        }
    }
    public static void HidePopup<T>() where T : UIPopup // 제네릭 활용해서 팝업 끄기
    {
        string key = typeof(T).Name;
        if (Instance.popupDic.ContainsKey(key))
        {
            if (Instance.currentPopup == Instance.popupDic[key])
            {
                Instance.popupDic[key].Hide();
                Instance.currentPopup = null;
            }

        }
    }
    public static void HidePopup(string _key) // 키 활용해서 팝업 끄기
    {
        if (Instance.popupDic.ContainsKey(_key))
        {
            if (Instance.currentPopup == Instance.popupDic[_key])
            {
                Instance.popupDic[_key].Hide();
                Instance.currentPopup = null;
            }
        }
    }
    public static void HidePopup(UIPopup popup) // 팝업 객체로 팝업 끄기
    {
        HidePopup(popup.Key);
    }
    #endregion
}
