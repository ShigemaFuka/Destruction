using UnityEngine;

/// <summary>
/// 倍速にする機能
/// </summary>
public class TimeScaleChanger : MonoBehaviour
{
    public static TimeScaleChanger Instance;
    private float _tmpTimeScale = 1; // 停止前の値

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

    public void Resume()
    {
        Time.timeScale = _tmpTimeScale;
    }

    /// <summary>
    /// Default: 1, Stop: 0, 2XSpeed: 2
    /// </summary>
    /// <param name="value"></param>
    public void ChangeTimeScale(float value)
    {
        Time.timeScale = value;
        _tmpTimeScale = value;
    }

    /// <summary>
    /// インスタンスが破棄されるときにデフォルトに戻す
    /// ※シーン遷移やリロード時
    /// </summary>
    private void OnDisable()
    {
        DefaultTimeScale();
        Debug.Log($"onDisable() Time.timeScale: {Time.timeScale}");
    }
}