using UnityEngine;
using Google.Play.Review;
using Google.Play.Common;
using Google.Play.AppUpdate;
using System.Collections;



// 추가 작업 필요
// :: Google Reveiw, AppUpdate SDK 통합
public class GoogleManager : Singleton<GoogleManager>
{

    ReviewManager _reviewManager = null;
    PlayReviewInfo _playReviewInfo;
    AppUpdateManager appUpdateManager = null;

    private const string REVIEW_URL = "Insert Review URL";


    private void Start()
    {
        _reviewManager = new ReviewManager();
#if UNITY_ANDROID && !UNITY_EDITOR
        StartCoroutine(UpdateApp());
#endif
    }
    public void RequestReview() // 리뷰 팝업 출력 전 15초 정도 전 요청
    {
        StartCoroutine(RequestReviewInfoInstance());
    }
    private IEnumerator RequestReviewInfoInstance()
    {
        var requestFlowOperation = _reviewManager.RequestReviewFlow();
        yield return requestFlowOperation;
        if (requestFlowOperation.Error != ReviewErrorCode.NoError)
        {
            // Log Error.
            yield break;
        }
        _playReviewInfo = requestFlowOperation.GetResult();
    }
    public void StartReview() // 리뷰 팝업 출력
    {
        StartCoroutine(StartInAppReviewFlow());
    }
    private IEnumerator StartInAppReviewFlow()
    {
        var launchFlowOperation = _reviewManager.LaunchReviewFlow(_playReviewInfo);
        yield return launchFlowOperation;
        _playReviewInfo = null; // Reset the object
        if (launchFlowOperation.Error != ReviewErrorCode.NoError)
        {

            // Log Error.
            yield break;
        }

    }

    /// <summary>
    /// 인앱 업데이트 코루틴
    /// </summary>
    /// <returns></returns>
    public IEnumerator UpdateApp()
    {
        appUpdateManager = new AppUpdateManager();

        PlayAsyncOperation<AppUpdateInfo, AppUpdateErrorCode> appUpdateInfoOperation = appUpdateManager.GetAppUpdateInfo();
        yield return appUpdateInfoOperation;
        if (appUpdateInfoOperation.IsSuccessful)
        {
            var appUpdateInfoResult = appUpdateInfoOperation.GetResult();
            var appUpdateOptions = AppUpdateOptions.ImmediateAppUpdateOptions();
            var startUpdateRequest = appUpdateManager.StartUpdate(appUpdateInfoResult, appUpdateOptions);

            yield return startUpdateRequest;
        }
        else
        {
            Debug.Log(appUpdateInfoOperation.Error);
        }

    }

    public void OpenUrl() 
    {
        Application.OpenURL(REVIEW_URL);
    }

}