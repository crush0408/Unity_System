using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening; // DOTween

public abstract class UIPopup : UIBase
{
    [Header("팝업 베이스")]
    public Button exitBtn; // 나가기 버튼
    public Button bgBtn; // 배경 버튼
    public GameObject popupObj; // 팝업 오브젝트
    public Image bgImg; // 배경 이미지
    [Space]
    private Color originColor;
    
    Sequence openSequence; // 팝업 오픈 트윈
    

    public override void Init()
    {
        originColor = bgImg.color;
        
        // 오픈 트윈 세팅
        openSequence = DOTween.Sequence().SetAutoKill(false).Pause()
            .Append(popupObj.transform.DOScale(Vector3.one * 1.1f, 0.2f).SetEase(Ease.OutQuad))
            .Join(bgImg.DOColor(originColor, 0.2f).SetEase(Ease.Linear))
            .Append(popupObj.transform.DOScale(Vector3.one * 1f, 0.1f).SetEase(Ease.OutQuad));

        
        exitBtn.onClick.AddListener(() =>
        {
            UIHandler.HidePopup(Key);
        });
        bgBtn.onClick.AddListener(() =>
        {
            UIHandler.HidePopup(Key);
        });
    }
    public override void Show()
    {
        popupObj.transform.localScale = Vector3.zero;
        bgImg.color = new Color(1, 1, 1, 0);
        base.Show();
        openSequence.Restart();
    }


    public override void Hide()
    {
        base.Hide();
    }
    
}