using UnityEngine;

/// <summary>
/// リザルトシーンで表示する値
/// </summary>
public class GetTmpCoin : MonoBehaviour
{
    private GameManager _gameManager = default;
    private NumberTween _numberTween = default;
    private float _reward = default;

    private void Start()
    {
        _gameManager = FindObjectOfType<GameManager>();
        if (_gameManager == null) Debug.LogWarning("GameManagerがありません。");
        _numberTween = FindObjectOfType<NumberTween>();
        if (_numberTween == null) Debug.LogWarning($"{_numberTween.name}がありません。");
        _gameManager.AddTmpCoin(); // 戦果を所有コインに加算
    }

    private void Update()
    {
        if (_reward != _gameManager.TemporaryCustodyCoin)
        {
            _reward = _gameManager.TemporaryCustodyCoin;
            _numberTween.NumTween(_reward);
        }
    }
}