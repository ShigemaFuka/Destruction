using System.Collections;
using UnityEngine;

/// <summary>
/// 一定時間後にHPを0にする
/// 自決機能
/// </summary>
public class Suicide : MonoBehaviour, IDeath
{
    [SerializeField, Header("生存時間")] private float _lifeTime = 10f;
    private Hp _hp = default;
    private WaitForSeconds _wfs = default;
    private GameObject _tower = default;
    private bool _canDamege = default; // タワーにダメージを与えられるか
    private float _remainingHp = default;

    private void Start()
    {
        _tower = GameObject.FindWithTag("Tower");
        _wfs = new WaitForSeconds(_lifeTime);
        _hp = GetComponent<Hp>();
        StartCoroutine(DoSuicide());
        _canDamege = true;
    }

    private IEnumerator DoSuicide()
    {
        yield return _wfs;
        _remainingHp = _hp.CurrentHp;
        _hp.Damage(_hp.CurrentHp);
    }

    /// <summary>
    /// 死亡したあとに実行される
    /// </summary>
    public void Death()
    {
        if (!_canDamege) return;
        var d = _tower.GetComponent<IDamage>();
        d.Damage(_remainingHp);
        Debug.Log($"damage value : {_remainingHp}");
        _canDamege = false;
    }

    /// <summary>
    /// 残り時間に応じて色を変える
    /// </summary>
    private void ColorChanger()
    {
    }
}