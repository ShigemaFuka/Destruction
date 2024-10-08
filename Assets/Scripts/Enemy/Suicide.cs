using System.Collections;
using UnityEngine;

/// <summary>
/// 一定時間後にHPを0にする
/// 自決機能
/// 時間切れ：残りHPに応じたダメージ
/// 倒された：フルHPの1割りのダメージ
/// </summary>
public class Suicide : MonoBehaviour, IDeath
{
    #region 変数

    [SerializeField, Header("生存時間")] private float _lifeTime = 10f;
    private Hp _hp = default;
    private WaitForSeconds _wfs = default;
    private GameObject _tower = default;
    private bool _canDamege = default; // タワーにダメージを与えられるか
    private float _remainingHp = default;

    #endregion


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
    /// 死亡したあとに実行されるメソッド
    /// </summary>
    public void Death()
    {
        // todo: 一定距離以内にタワーが存在するときだけ、ダメージを与える処理
        if (_remainingHp <= 0)
        {
            Attack(_hp.MaxHp * 0.1f);
            Debug.Log("1 wari : " + _hp.MaxHp * 0.1f);
        }
        else Attack(_remainingHp);
    }

    /// <summary>
    /// 残り時間に応じて色を変える
    /// todo:見た目が替わるなら何でもいい
    /// </summary>
    private void ColorChanger()
    {
    }

    /// <summary>
    /// 倒されたときと、時間切れになったときに、タワーダメージを与える。
    /// todo: 距離次第ではダメージを入れない
    /// </summary>
    private void Attack(float value)
    {
        if (!_canDamege) return;
        var d = _tower.GetComponent<IDamage>();
        d.Damage(value);
        Debug.Log($"damage value : {value}");
        _canDamege = false;
    }
}