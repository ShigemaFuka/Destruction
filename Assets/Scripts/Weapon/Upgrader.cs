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

    private void Start()
    {
        _costManager = FindObjectOfType<CostManager>();
    }

    public void Upgrade(WeaponStatus status)
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
        status.Reload /= _reload;
        status.Range *= _range;
    }
}