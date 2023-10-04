using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ExamplePanel_01 : UIPanel
{
    public Button popupBtn;
    public Button nextBtn;
    public override void Init()
    {
        popupBtn.onClick.AddListener(() =>
        {
            UIHandler.ShowPopup<ExamplePopup_01>();
        });
        nextBtn.onClick.AddListener(() =>
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
