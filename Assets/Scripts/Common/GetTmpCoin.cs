using UnityEngine;

/// <summary>
/// リザルトシーンで表示する値
/// </summary>
public class GetTmpCoin : MonoBehaviour
{
    private GameManager _gameManager;
    private NumberTween _numberTween;
    private float _reward; // 一戦で得た報酬

    private void Start()
    {
        _gameManager = FindObjectOfType<GameManager>();
        if (_gameManager == null) Debug.LogWarning("GameManagerがありません。");
        _numberTween = FindObjectOfType<NumberTween>();
        if (_numberTween == null) Debug.LogWarning($"{_numberTween.name}がありません。");
        _gameManager.AddTmpCoin(); // 戦果を所有コインに加算
        _reward = _gameManager.TemporaryCustodyCoin;
        _numberTween.NumTween(_reward);
        _gameManager.ResetTmpCoin();
    }
}