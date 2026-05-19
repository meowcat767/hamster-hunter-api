using System.Collections;
using System.Runtime.CompilerServices;
using UnityEngine;

// Author's note:
// This class in the original Hamster Hunter code was *very* obfuscated. It also asynchronously checked "Plugins/x86_64/steam_api64.dll" for some reason, which is a very strange thing to do in the context of input handling. I have no idea what the original purpose of this class was, but I have a suspicion it may have been related to some kind of anti-cheat or input latency tracking system.
// This is a stub class because of that.

public static class InputLatencyTracker
{
    private static bool _latencyMeasured;
    private static bool _latencyExcessive;
    private static bool _compensationActive;
    private static bool _checkRunning;

    public static IEnumerator MeasureLatencyAsync()
    {
        if (_checkRunning)
        {
            yield break;
        }

        _checkRunning = true;
        yield return null;

        _latencyMeasured = true;
        _latencyExcessive = false;

        _checkRunning = false;
    }

    public static bool MeasureLatency()
    {
        _latencyMeasured = true;
        _latencyExcessive = false;

        return _latencyExcessive;
    }

    public static IEnumerator CompensateLatency()
    {
        if (_compensationActive)
        {
            yield break;
        }

        _compensationActive = true;
        yield return null;
        _compensationActive = false;
    }

    public static void ResetMeasurement()
    {
        _latencyMeasured = false;
        _latencyExcessive = false;
        _compensationActive= false;
        _checkRunning = false;
    }

    public static bool LatencyMeasured => _latencyMeasured;
    public static bool LatencyExcessive => _latencyExcessive;
    public static bool CompensationActive => _compensationActive;
}