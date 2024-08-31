using UnityEngine;

/// <summary>
/// 武器の強化機能
/// </summary>
public class Upgrader : MonoBehaviour
{
    [Header("強化するたびに変化させる加算減算の量")] [Space] [SerializeField]
    private int _level = 1;

    private float _cost = 1.2f;

    [SerializeField] private float _attack = 1.2f;
    [SerializeField] private float _reload = 1.2f;
    [SerializeField] private float _range = 1.2f;

    public void Upgrade(WeaponStatus status)
    {
        // todo: コスト制限と、資金減算処理
        status.Level += _level;
        status.Cost *= _cost;
        status.Attack *= _attack;
        status.Reload /= _reload;
        status.Range *= _range;
    }
}