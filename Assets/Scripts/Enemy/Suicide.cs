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
    [SerializeField, Header("可視化範囲のObj")] private GameObject _rangeObj;
    [SerializeField] private Animator _animator;
    private Hp _hp;
    private GameObject _tower;
    private bool _canDamage; // タワーにダメージを与えられるか
    private float _remainingHp;

    #endregion

    private void Start()
    {
        _tower = GameObject.FindWithTag("Tower");
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
        var t = _lifeTime - 5;
        yield return new WaitForSeconds(t); // 死亡５秒前：t
        _animator.Play("Warning");
        yield return new WaitForSeconds(2); // tの２秒後
        _animator.speed = 2f;
        yield return new WaitForSeconds(_lifeTime - t - 2);
        _remainingHp = _hp.CurrentHp; // 時間切れによる自滅直前のHPを格納
        _hp.Damage(_hp.CurrentHp);
    }

    /// <summary>
    /// 死亡したあとに実行されるメソッド
    /// 自爆時間前に死亡したとき
    /// </summary>
    public void Death()
    {
        if (_remainingHp <= 0)
        {
            Attack(_hp.MaxHp * 0.1f);
            // Debug.Log("1 wari : " + _hp.MaxHp * 0.1f);
        }
        else
        {
            Attack(_remainingHp);
            // Debug.Log($"_remainingHp : {_remainingHp}");
        }

        _rangeObj.SetActive(false); // 非表示
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
            var ds = _tower.GetComponents<IDamage>();
            foreach (var d in ds)
            {
                d?.Damage(value);
            }
        }

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
        // Debug.Log($"sprLen: {sqrLen}   range: {_range}");
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