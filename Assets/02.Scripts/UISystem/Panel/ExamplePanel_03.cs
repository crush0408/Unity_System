using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ExamplePanel_03 : UIPanel
{
    public Button popupBtn;

    public Button prevBtn;
    public override void Init()
    {
        popupBtn.onClick.AddListener(() =>
        {
            UIHandler.ShowPopup<ExamplePopup_03>();
        });
        prevBtn.onClick.AddListener(() =>
        {
            UIHandler.ShowPanel<ExamplePanel_02>();
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
