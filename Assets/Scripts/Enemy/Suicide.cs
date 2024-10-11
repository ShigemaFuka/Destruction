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
    [SerializeField, Header("範囲")] private float _range = 3f;
    [SerializeField, Header("可視化範囲のObj")] private GameObject _rangeObj = default;
    private Hp _hp = default;
    private WaitForSeconds _wfs = default;
    private GameObject _tower = default;
    private bool _canDamage = default; // タワーにダメージを与えられるか
    private float _remainingHp = default;

    #endregion

    private void Start()
    {
        _tower = GameObject.FindWithTag("Tower");
        _wfs = new WaitForSeconds(_lifeTime);
        _hp = GetComponent<Hp>();
        StartCoroutine(DoSuicide());
        _canDamage = true;
    }

    private void Update()
    {
        ShowRange();
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
        if (_remainingHp <= 0)
        {
            Attack(_hp.MaxHp * 0.1f);
            Debug.Log("1 wari : " + _hp.MaxHp * 0.1f);
        }
        else Attack(_remainingHp);

        _rangeObj.SetActive(false); // 非表示
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
    /// </summary>
    private void Attack(float value)
    {
        if (!_canDamage) return;
        if (CheckDistance())
        {
            // 一定距離以内にタワーが存在するときだけ、ダメージを与える
            var d = _tower.GetComponent<IDamage>();
            d.Damage(value);
        }

        Debug.Log($"damage value : {value}");
        _canDamage = false;
    }

    private void ShowRange()
    {
        var r = _range * 2;
        if (_rangeObj) _rangeObj.transform.localScale = new Vector3(r, r, r);
    }

    /// <summary>
    /// タワーが一定距離以内かどうか
    /// 一定距離以内なら真　※攻撃可能
    /// </summary>
    private bool CheckDistance()
    {
        var offset = _tower.transform.position - transform.position;
        var sqrLen = offset.sqrMagnitude;
        return sqrLen < _range * _range;
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, 5f);
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, 10f);
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, 25f);
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, 35f);
    }
}