using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// 残りの敵数の表示
/// </summary>
public class ShowEnemyCount : MonoBehaviour
{
    [SerializeField, Header("Text:現在の数")] private Text _currentText;
    [SerializeField, Header("Text:最大の数")] private Text _maxText;
    private WaveManager _waveManager;
    private int _maxValue; // 最大生成数
    private int _numberOfRemaining; // 残りの数
    private int _killCount; // 倒した数

    private void Start()
    {
        _waveManager = WaveManager.Instance;
        if (_waveManager) _maxValue = _waveManager.EnemyTypeList.Count;
        else Debug.LogWarning("WaveManagerがありません。");
        if (_maxText) _maxText.text = $"/ {_maxValue}";
        else Debug.LogWarning("MaxTextがありません。");
        _numberOfRemaining = _maxValue;
        ShowCount();
    }

    private void Update()
    {
        // 値に変更があったときに、表示を更新
        if (_killCount != _waveManager.KillCount)
        {
            _numberOfRemaining = _maxValue - _waveManager.KillCount;
            ShowCount();
            _killCount = _waveManager.KillCount;
        }
    }

    private void ShowCount()
    {
        if (_currentText) _currentText.text = $"{_numberOfRemaining}";
        else Debug.LogWarning("CurrentTextがありません。");
    }
}