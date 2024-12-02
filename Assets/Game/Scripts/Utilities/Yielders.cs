using System.Collections.Generic;
using UnityEngine;

public static class Yielders
{
    private static readonly Dictionary<float, WaitForSeconds> _timeInterval = new(100);

    public static WaitForSeconds WaitForSeconds(float seconds)
    {
        if (!_timeInterval.ContainsKey(seconds))
        {
            _timeInterval.Add(seconds, new WaitForSeconds(seconds));
        }

        return _timeInterval[seconds];
    }

    public static WaitForEndOfFrame EndOfFrame { get; } = new();

    public static WaitForFixedUpdate FixedUpdate { get; } = new();
}

