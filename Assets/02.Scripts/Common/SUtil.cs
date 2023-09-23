using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public static class SUtil
{
    public static List<T> GetList<T>(in List<T> originList)
    {
        return originList.ToList();
    }
    public static List<T> GetList<T>(in T[] originArray)
    {
        return originArray.ToList();
    }
}
