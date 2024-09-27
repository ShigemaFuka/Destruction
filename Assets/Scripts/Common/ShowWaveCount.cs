using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// 現在のWave数と最大Wave数を表示する
/// </summary>
public class ShowWaveCount : MonoBehaviour
{
    [SerializeField, Header("Text:現在の数")] private Text _currentText = default;
    [SerializeField, Header("Text:最大の数")] private Text _maxText = default;
    private WaveManager _waveManager = default;
    private int _maxValue = default; // 最大Wave数
    private int _currentWave = default;

    private void Start()
    {
        _waveManager = FindObjectOfType<WaveManager>();
        if (_waveManager) _maxValue = _waveManager.MaxWaveCount;
        else Debug.LogWarning("WaveManagerがありません。");
        if (_maxText) _maxText.text = $"/ {_maxValue}";
        else Debug.LogWarning("MaxTextがありません。");
        ShowCount();
    }

    private void Update()
    {
        // 値に変更があったときに、表示を更新
        if (_currentWave != _waveManager.CurrentWave)
        {
            ShowCount();
            _currentWave = _waveManager.CurrentWave;
        }
    }

    private void ShowCount()
    {
        if (_currentText) _currentText.text = $"{_waveManager.CurrentWave}";
        else Debug.LogWarning("CurrentTextがありません。");
    }
}