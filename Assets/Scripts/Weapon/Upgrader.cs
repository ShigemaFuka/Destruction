using UnityEngine;

/// <summary>
/// 武器の強化機能
/// </summary>
public class Upgrader : MonoBehaviour
{
    [Header("強化するたびに変化させる加算減算の量")] [Space] [SerializeField]
    private int _level = 1;

    [SerializeField] private float _cost = 1.2f;
    [SerializeField] private float _attack = 1.2f;
    [SerializeField] private float _reload = 1.2f;
    [SerializeField] private float _range = 1.2f;
    private CostManager _costManager = default;
    private SaveManager _saveManager = default;
    private GameManager _gameManager = default;

    private void Start()
    {
        _costManager = CostManager.Instance;
        _saveManager = SaveManager.Instance;
        _gameManager = GameManager.Instance;
    }

    /// <summary>
    /// インゲーム中の強化
    /// </summary>
    /// <param name="status"></param>
    public void UpgradeOnInGame(WeaponStatus status)
    {
        if (!_costManager.Judge(status.Cost))
        {
            Debug.LogWarning("コストが足りません");
            return;
        } // コスト制限

        _costManager.Decrease(status); // 資金減算処理
        status.Level += _level;
        status.Cost *= _cost;
        status.Attack *= _attack;
        status.Reload *= _reload;
        status.Range *= _range;
    }

    /// <summary>
    /// アップグレードシーン中の強化
    /// </summary>
    /// <param name="num"></param>
    public void UpgradeOnUpgradeScene(int num)
    {
        var data = _saveManager.LoadGameData();
        var status = data._listWrapper._weaponStatusDataList[num];
        if (data._coin < status._cost)
        {
            Debug.LogWarning("コインが足りません");
            return;
        } // コイン制限

        status._level += _level;
        status._cost *= _cost;
        status._attack *= _attack;
        status._reload *= _reload;
        status._range *= _range;
        _gameManager.Decrease(status, num); // 資金減算処理
    }
}