using UnityEngine;

/// <summary>
/// 一定範囲内に敵がいたら方向転換して、攻撃
/// </summary>
public class Weapon : StateBase
{
    #region 変数

    [SerializeField, Header("弾丸プレハブ")] private GameObject _bulletPrefab = default;
    [SerializeField, Header("マズル")] private Transform _bulletSpawnPoint = default;

    [SerializeField, Header("可視化範囲のObj")] private GameObject _rangeObj = default;

    // [SerializeField, Header("発射インターバル")] private float _shootInterval = default;
    // [SerializeField, Header("ダメージ数")] private float _damage = 2f;
    [SerializeField, Header("範囲")] private float _range = 5f;
    private Generator _generator = default;

    private GameObject _target = default;
    private OffenseState _offenseState = default;
    private float _timer = default;
    private WeaponStatus _weaponStatus = default;

    #endregion

    public GameObject Target => _target;

    protected override void OnStart()
    {
        _weaponStatus = GetComponent<WeaponStatus>();
        _generator = FindObjectOfType<Generator>();
        // _offenseState = new OffenseState(this, _damage, _bulletPrefab, _bulletSpawnPoint);
        _offenseState = new OffenseState(this, _weaponStatus.Attack, _bulletPrefab, _bulletSpawnPoint);
        // _timer = _shootInterval;
        _timer = _weaponStatus.Reload; // 生成直後に発射可能
    }

    protected override void OnUpdate()
    {
        ShowRange();

        if (_target != null)
        {
            Rotation();
        }

        _target = IntrusionJudgment();
        if (_target == null)
        {
            return;
        }

        _timer += Time.deltaTime;
        // if (_timer >= _shootInterval)
        if (_timer >= _weaponStatus.Reload)
        {
            ChangeState(_offenseState);
            _timer = 0;
        }
    }

    /// <summary>
    /// 侵入判定
    /// 敵の生成順に範囲内にいるかどうかを比較
    /// </summary>
    private GameObject IntrusionJudgment()
    {
        if (_generator.EnemiesList.Count == 0)
        {
            Debug.LogWarning("listの要素数が０です。");
            return null;
        }

        foreach (var enemy in _generator.EnemiesList)
        {
            if (enemy == null) continue; // 中身がnullなら飛ばす
            var offset = enemy.transform.position - transform.position;
            var sqrLen = offset.sqrMagnitude;

            // if (sqrLen < _range * _range)
            if (sqrLen < _weaponStatus.Range * _weaponStatus.Range)
            {
                return enemy;
            } // 範囲内にいたら
        }

        return null;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, _range);
    }

    private void ShowRange()
    {
        var r = _weaponStatus.Range * 2;
        _rangeObj.transform.localScale = new Vector3(r, r, r);
    }

    /// <summary>
    /// 攻撃方向への回転
    /// </summary>
    private void Rotation()
    {
        var nextCorner = _target.transform.position;
        var to = nextCorner - transform.position;
        var angle = Vector3.SignedAngle(transform.forward, to, Vector3.up);
        // 角度が1゜を越えていたら
        if (Mathf.Abs(angle) > 1)
        {
            var rotMax = 100 * Time.deltaTime;
            var rot = Mathf.Min(Mathf.Abs(angle), rotMax);
            transform.Rotate(0f, rot * Mathf.Sign(angle), 0f);
        }
    }
}