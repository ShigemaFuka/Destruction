using UnityEngine;

/// <summary>
/// 戦果となるコインの数を設定する
/// </summary>
public class SetTmpCoin : MonoBehaviour
{
    [SerializeField, Header("戦果報酬の最大値 ※引かれる前の値")]
    private float _reward = 100f;

    private GameManager _gameManager = default;

    private void Start()
    {
        _gameManager = FindObjectOfType<GameManager>();
        _gameManager.TemporaryCustodyCoin = _reward;
    }
}