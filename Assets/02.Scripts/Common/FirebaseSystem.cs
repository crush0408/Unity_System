using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Firebase;
using Firebase.Analytics;
using Firebase.RemoteConfig;
using Firebase.Extensions;
using System.Threading.Tasks;
using System;
using DG.Tweening.Core.Easing;
using System.Data.Common;

public static class Predefined
{
    // 크로스 프로모션 파라미터 : 현재 게임에서 보여줄 광고 프로모션 정보
    public static bool Icon_CrossPromotion;
    public static bool Popup_CrossPromotion;
    public static string Icon_URL;
    public static string Popup_URL;
    public static string Store_URL;

    // 인터스티셜 파라미터
    // 첫 광고
    public static bool Interstitial_first_On_Off;
    public static int Interstitial_first_ClearLevel;
    // 클리어 시점
    public static bool Interstitial_Clear_On_Off;
    public static int Interstitial_Clear_ClearLevel;
    public static int Interstitial_Clear_Timer;
    // 게임 시작
    public static bool Interstitial_Installday_On_Off;
    public static int Interstitial_Installday_Time;
}


// 추가 작업 필요
// :: Firebase unity SDK ( Analytics, RemoteConfig ) 통합 필요
public static class FirebaseSystem
{
    private static bool initialize = false;
    private static List<Parameter> parameters = new List<Parameter>();
    private static FirebaseApp _app;


    public static bool IsFetchCompleted { get; private set; }
    public static event Action OnFetchCompleted;

    public static async Task InitRemoteConfigValue()
    {
        try
        {
            Debug.Log("Start Init Remote Config");
            Dictionary<string, object> defaults = new Dictionary<string, object>();

            defaults.Add("Test", false);

            await FirebaseRemoteConfig.DefaultInstance.SetDefaultsAsync(defaults);
            Debug.Log("RemoteConfig configured and ready !");
            await FetchDataAsync();
        }
        catch (Exception e)
        {
            Debug.LogError("Firebase Remote Config Error");
            Debug.LogError(e.Message);
            throw;
        }
    }

    private static async Task FetchDataAsync()
    {
        Debug.Log("Fetching data...");
        Task fetchTask = FirebaseRemoteConfig.DefaultInstance.FetchAsync(TimeSpan.Zero);
        await fetchTask;
        await FetchComplete(fetchTask);
    }
    private static async Task FetchComplete(Task fetchTask)
    {
        if (fetchTask.IsCanceled)
        {
            Debug.Log("Fetch Canceled.");
        }
        else if (fetchTask.IsFaulted)
        {
            Debug.Log("Fetch encountered an error.");
        }
        else if (fetchTask.IsCompleted)
        {
            Debug.Log("Fetch completed successfully!");
        }

        var info = FirebaseRemoteConfig.DefaultInstance.Info;
        switch (info.LastFetchStatus)
        {
            case LastFetchStatus.Success:
                await FirebaseRemoteConfig.DefaultInstance.ActivateAsync();
                OnCompleteFetch();
                break;
            case LastFetchStatus.Failure:
                switch (info.LastFetchFailureReason)
                {
                    case FetchFailureReason.Invalid:
                        Debug.Log("Fetch Invalid");
                        break;
                    case FetchFailureReason.Throttled:
                        Debug.Log("Fetch throttled until " + info.ThrottledEndTime);
                        break;
                    case FetchFailureReason.Error:
                        Debug.Log("Fetch failed for unknown reason");
                        break;
                    default:
                        break;
                }
                break;
            case LastFetchStatus.Pending:
                Debug.Log("Latest Fetch call still pending");
                break;
            default:
                break;
        }
    }
    private static void OnCompleteFetch()
    {
        // RemoteConfig 데이터 불러오기
        Predefined.Icon_CrossPromotion = FirebaseRemoteConfig.DefaultInstance.GetValue("Icon_CrossPromotion").BooleanValue;
        Predefined.Popup_CrossPromotion = FirebaseRemoteConfig.DefaultInstance.GetValue("Popup_CrossPromotion").BooleanValue;
        Predefined.Icon_URL = FirebaseRemoteConfig.DefaultInstance.GetValue("Icon_URL").StringValue;
        Predefined.Popup_URL = FirebaseRemoteConfig.DefaultInstance.GetValue("Popup_URL").StringValue;
        Predefined.Store_URL = FirebaseRemoteConfig.DefaultInstance.GetValue("Store_URL").StringValue;

        // Temp Tier
        int tier = 1;
        // 첫 광고
        Predefined.Interstitial_first_On_Off = FirebaseRemoteConfig.DefaultInstance.GetValue($"Interstitial_first_{tier}Tier_On_Off").BooleanValue;
        Predefined.Interstitial_first_ClearLevel = (int)FirebaseRemoteConfig.DefaultInstance.GetValue($"Interstitial_first_{tier}Tier_ClearLevel").LongValue;
        // 클리어
        Predefined.Interstitial_Clear_On_Off = FirebaseRemoteConfig.DefaultInstance.GetValue($"Interstitial_Clear_{tier}Tier_On_Off").BooleanValue;
        Predefined.Interstitial_Clear_ClearLevel = (int)FirebaseRemoteConfig.DefaultInstance.GetValue($"Interstitial_Clear_{tier}Tier_ClearLevel").LongValue;
        Predefined.Interstitial_Clear_Timer = (int)FirebaseRemoteConfig.DefaultInstance.GetValue($"Interstitial_Clear_{tier}Tier_Timer").LongValue;
        // 인스톨
        Predefined.Interstitial_Installday_On_Off = FirebaseRemoteConfig.DefaultInstance.GetValue($"Interstitial_Installday_{tier}Tier_On_Off").BooleanValue;
        Predefined.Interstitial_Installday_Time = (int)FirebaseRemoteConfig.DefaultInstance.GetValue($"Interstitial_Installday_{tier}Tier_Time").LongValue;


        IsFetchCompleted = true;
        OnFetchCompleted?.Invoke();
    }




    // 초기화 함수
    public static void Init(Action callback)
    {
        OnFetchCompleted = callback;
        FirebaseApp.CheckAndFixDependenciesAsync().ContinueWith(async task =>
        {
            if (task.Result == DependencyStatus.Available)
            {
                _app = FirebaseApp.DefaultInstance;
                Debug.Log("Firebase Init Success !!");
                initialize = true;
                // Do Init Event Log
                EventLogInit();
                await InitRemoteConfigValue();
            }
            else
            {
                Debug.LogError("Could not resolve all Firebase dependencies: " + task.Result);
            }
        });

    }

    #region EventLog

    private static void EventLogInit()
    {

        EventLog("GameStart",
            "PlayerLevel", 1,
            "PlayerName", "FireBaseSystem",
            "PlayerMoney", 100);
    }

    public static void EventLog(string eventName, params object[] param) // 파라메터는 string만 등록 된다.
    {
        if (initialize == false)
        {
            Debug.LogError("Not initialize");
            return;
        }
        // param Count Check
        if (param.Length % 2 != 0)
        {
            Debug.LogError("Param is Wrong !! Check Param");
            return;
        }
        for (int i = 0; i < param.Length; i += 2)
        {
            parameters.Add(new Parameter(param[i].ToString(), param[i + 1].ToString()));
        }
        FirebaseAnalytics.LogEvent(eventName, parameters.ToArray());
        parameters.Clear();
    }
    #endregion
}