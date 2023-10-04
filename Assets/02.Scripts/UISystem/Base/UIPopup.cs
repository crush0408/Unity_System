using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public abstract class UIPopup : UIBase
{
    [Header("팝업 베이스")]
    public Button exitBtn; // 나가기 버튼
    public Button bgBtn; // 배경 버튼
    public GameObject popupObj; // 팝업 오브젝트
    public Image bgImg; // 배경 이미지
    [Space]
    private Color originColor;
    /*
    Sequence openSequence;

    Sequence closeSequence;

    public override void Init()
    {
        originColor = backImg.color;

        openSequence = DOTween.Sequence().SetAutoKill(false).Pause()
            .Append(popupObject.transform.DOScale(Vector3.one * 1.1f, 0.2f).SetEase(Ease.OutQuad))
            .Join(backGround.DOColor(basicColor, 0.2f).SetEase(Ease.Linear))
            .Append(popupObject.transform.DOScale(Vector3.one * 1f, 0.1f).SetEase(Ease.OutQuad));
        closeSequence = DOTween.Sequence().SetAutoKill(false).Pause()
            .Append(target.transform.DOScale(Vector3.one * 1.1f, 0.1f).SetEase(Ease.OutQuad))
            .Append(target.transform.DOScale(Vector3.zero, 0.2f).SetEase(Ease.OutQuad))
            .Join(backGround.DOColor(new Color(1, 1, 1, 0), 0.2f).SetEase(Ease.Linear))
            .AppendCallback(() => base.Hide());
        exitBtn.onClick.AddListener(() =>
        {
            UIHandler.HidePopup(Key);
        });
        backBtn.onClick.AddListener(() =>
        {
            UIHandler.HidePopup(Key);
        });
    }
    public override void Show()
    {
        popupObject.transform.localScale = Vector3.zero;
        backGround.color = new Color(1, 1, 1, 0);
        base.Show();
        openSequence.Restart();
    }


    public override void Hide()
    {
        closeSequence.Restart();
    }
    */
}