using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DemoVibration : MonoBehaviour
{

    [SerializeField] UnityEngine.UI.Text VibrationAvailableText;
    [SerializeField] UnityEngine.UI.Text VibrationAmplitudeText;

    private void Start()
    {
        if (VibrationAmplitudeText)
        {
            VibrationAmplitudeText.text = string.Format("Vibration Amplitude available : {0}", Rekkuzan.NativeVibration.IsAmplitudeAvailable());
        }

        if (VibrationAvailableText)
        {
            VibrationAvailableText.text = string.Format("Vibration available : {0}", Rekkuzan.NativeVibration.IsVibrationAvailable());
        }
    }

    public void Single()
    {
        Rekkuzan.NativeVibration.Vibrate(100);
    }

    public void SingleShortWeak()
    {
        Rekkuzan.NativeVibration.Vibrate(50, 50);
    }

    public void SingleLongStrong()
    {
        Rekkuzan.NativeVibration.Vibrate(1000, 250);
    }

    public void PatternOne()
    {
        Rekkuzan.NativeVibration.Vibrate(new long[] { 50, 100, 150, 10, 50 }, new int[] { 50, 0, 50, 0, 50 });
    }


    public void PatternTwo()
    {
        Rekkuzan.NativeVibration.Vibrate(new long[] { 50, 500, 150, 10, 50, 1000 }, new int[] { 50, 0, 150, 0, 50, 10 });
    }

    public void HeartBeat()
    {
        List<long> pattern = new List<long>();
        List<int> amplitude = new List<int>();
        for (int i = 0; i < 100; i++)
        {
            pattern.Add(100);
            amplitude.Add(100);

            pattern.Add(360);
            amplitude.Add(0);

            pattern.Add(100);
            amplitude.Add(100);

            pattern.Add(600);
            amplitude.Add(0);
        }

        Rekkuzan.NativeVibration.Vibrate(pattern.ToArray(), amplitude.ToArray());
    }

    public void Cancel()
    {
        Rekkuzan.NativeVibration.Cancel();
    }
}

