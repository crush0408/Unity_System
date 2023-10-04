using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ExamplePanel_02 : UIPanel
{
    public Button popupBtn;
    public Button prevBtn;
    public Button nextBtn;
    public override void Init()
    {
        popupBtn.onClick.AddListener(() =>
        {
            UIHandler.ShowPopup<ExamplePopup_02>();
        });
        prevBtn.onClick.AddListener(() =>
        {
            UIHandler.ShowPanel<ExamplePanel_01>();
        });
        nextBtn.onClick.AddListener(() =>
        {
            UIHandler.ShowPanel<ExamplePanel_03>();
        });
    }
    public override void Show()
    {
        base.Show();
    }
    public override void Hide()
    {
        base.Hide();
    }
}
