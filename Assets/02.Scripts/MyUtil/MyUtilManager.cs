using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class MyUtilManager : MonoBehaviour
{
    private void Start()
    {
        TimeSpan timespan = MyUtil.GetTimeInterval(MyUtil.GetCurrentDateTime(), MyUtil.GetCurrentDateTime());
        Debug.Log(timespan.Days);
        Debug.Log(timespan.TotalDays);
    }
}
