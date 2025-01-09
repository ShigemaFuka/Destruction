using UnityEngine;

/// <summary>
/// 現在所持しているコインを表示する
/// </summary>
public class ShowCoinValue : MonoBehaviour
{
    [SerializeField] private NumberTween _numberTween = default;
    private GameManager _gameManager = default;
    private float _value = default;

    private void Start()
    {
        if (_numberTween == null) Debug.LogWarning("NumberTweenがありません。");
        _gameManager = GameManager.Instance;
        if (_gameManager == null) Debug.LogWarning("GameManagerがありません。");
    }

    private void Update()
    {
        if (_value != _gameManager.Coin)
        {
            _value = _gameManager.Coin;
            _numberTween.NumTween(_value);
        }
    }
}