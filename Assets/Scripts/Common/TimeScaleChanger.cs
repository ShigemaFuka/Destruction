using UnityEngine;

/// <summary>
/// 倍速にする機能
/// </summary>
public class TimeScaleChanger : MonoBehaviour
{
    public static TimeScaleChanger Instance;

    private void Awake()
    {
        Instance = this;
    }

    public void DefaultTimeScale()
    {
        Time.timeScale = 1;
    }

    public void StopTime()
    {
        Time.timeScale = 0;
    }

    /// <summary>
    /// Default: 1, Stop: 0, 2XSpeed: 2
    /// </summary>
    /// <param name="value"></param>
    public void ChangeTimeScale(float value)
    {
        Time.timeScale = value;
    }
}