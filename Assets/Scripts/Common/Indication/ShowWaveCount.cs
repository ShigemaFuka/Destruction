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
        if (_currentText) _currentText.text = "1";
        else Debug.LogWarning("CurrentTextがありません。");
    }

    private void Update()
    {
        // 値に変更があったときに、表示を更新
        if (_currentWave != _waveManager.CurrentWave)
        {
            _currentWave = _waveManager.CurrentWave;
            if (_currentWave > _maxValue) return;
            ShowCount();
        }
    }

    private void ShowCount()
    {
        if (_currentText) _currentText.text = $"{_currentWave}";
        else Debug.LogWarning("CurrentTextがありません。");
    }
}