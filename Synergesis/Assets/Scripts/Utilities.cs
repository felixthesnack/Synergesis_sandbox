using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.EventSystems;

public static class Utilities
{
    public static void DeleteChildren(this Transform t)
    {
        foreach (Transform child in t) Object.Destroy(child.gameObject);
    }

    private static readonly Dictionary<float, WaitForSeconds> WaitDictionary = new Dictionary<float, WaitForSeconds>();

    public static WaitForSeconds GetWait(float time)
    {
        if (WaitDictionary.TryGetValue(time, out var wait)) return wait;

        WaitDictionary[time] = new WaitForSeconds(time);
        return WaitDictionary[time];
    }

    public static T Rand<T>(this List<T> list)
    {
        return list[Random.Range(0, list.Count)];
    }
}
