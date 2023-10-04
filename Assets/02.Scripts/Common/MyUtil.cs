using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System;

public static class MyUtil
{
    // Random 관련 함수
    public static int GetRandomIntValue(int start, int end)
    {
        return UnityEngine.Random.Range(start, end);
    }
    public static float GetRandomFloatValue(float start, float end)
    {
        return UnityEngine.Random.Range(start, end);
    }
    public static bool CheckRandom(float _value) // 0-1사이
    {
        float rand = GetRandomFloatValue(0f, 1f);
        return rand > (1f - _value);
    }
    public static bool CheckRandom(int _value) // 1-100사이
    {
        int rand = GetRandomIntValue(0, 100);
        return rand > (100 - _value);
    }






    // List 관련 함수
    public static List<T> GetList<T>(in List<T> originList) // GetList By List
    {
        return originList.ToList();
    }
    public static List<T> GetList<T>(in T[] originArray) // GetList By Array
    {
        return originArray.ToList();
    }
    public static T GetRandomElement<T>(List<T> targetList) // Get Random Element At List
    {
        return targetList[GetRandomIntValue(0, targetList.Count)];
    }
    public static T GetRandomElement<T>(List<T> targetList, Predicate<T> predicate) // Get Random Element At Condition // 이건 더 공부해보기
    {
        List<T> targetNewList = GetList(targetList).FindAll(predicate);
        return targetNewList[GetRandomIntValue(0, targetNewList.Count)];
    }




    // Distance 관련
    public static float GetDistance(Vector3 a, Vector3 b) // 거리 값 가져오기 (정확한 거리 값 계산)
    {
        return (a-b).magnitude;
    }
    public static float GetSqrDistance(Vector3 a, Vector3 b) // 거리값의 제곱값 가져오기 (단순 거리 비교)
    {
        return (a-b).sqrMagnitude; // sqrMagnitude : Mathf.Pow(b.x - a.x, 2) + Mathf.Pow(b.y - a.y, 2);
    }
    public static bool CheckDistance(Vector3 a, Vector3 b, float distance) // 거리를 비교할 때엔 제곱값을 활용 ( 성능적 우위 )
    {
        return GetSqrDistance(a,b) <= distance * distance;
    }


    // DateTime 관련
    public static DateTime GetDateTime(string saveTime) // 저장 시간 정보를 통해 DateTime 가져오기
    {
        return DateTime.FromBinary(Convert.ToInt64(saveTime));
    }
    public static string GetSaveTime(DateTime dateTime) // 저장용 시간 정보 가져오기
    {
        return dateTime.ToBinary().ToString();
    }
    public static DateTime GetCurrentDateTime() // 현재 시간 정보 가져오기
    {
        return DateTime.Now.ToLocalTime();
    }

    public static string GetTimeText(float second)
    {
        TimeSpan timeSpan = TimeSpan.FromSeconds(second);

        string formattedTime = "";

        if (timeSpan.Hours > 0)
        {
            formattedTime += $"{timeSpan.Hours}H ";
        }

        if (timeSpan.Minutes > 0)
        {
            formattedTime += $"{timeSpan.Minutes}M ";
        }

        if (timeSpan.Seconds > 0 || string.IsNullOrEmpty(formattedTime))
        {
            formattedTime += $"{timeSpan.Seconds}S";
        }

        return formattedTime;
    }
    public static TimeSpan GetTimeInterval(DateTime a, DateTime b)
    {
        return (b - a);
    }

}
