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
    [SerializeField, Header("回転するもの")] private GameObject _rotator = default;
    [SerializeField, Header("対象との角度差")] private float _angle = 1f;
    private float _rotateSpeed = 700f; // 回転速度
    private Generator _generator = default;
    private OffenseState _offenseState = default;
    private float _timer = default;
    private WeaponStatus _weaponStatus = default;

    #endregion

    public GameObject Target { get; private set; } = default;

    protected override void OnStart()
    {
        _weaponStatus = GetComponent<WeaponStatus>();
        _generator = FindObjectOfType<Generator>();
        _offenseState = new OffenseState(this, _bulletPrefab, _bulletSpawnPoint, gameObject);
        if (_generator.EnemiesList.Count == 0) Debug.LogWarning("listの要素数が０です。");
        if (!_rotator) Debug.LogWarning("回転するものがありません。");
    }

    protected override void OnUpdate()
    {
        ShowRange();

        if (Target != null)
        {
            SpeedByDistance();
            Rotation();
        }

        Target = IntrusionJudgment();
        if (Target == null)
        {
            return;
        }

        _timer += Time.deltaTime;
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
        foreach (var enemy in _generator.EnemiesList)
        {
            if (enemy == null) continue; // 中身がnullなら飛ばす
            var offset = enemy.transform.position - transform.position;
            var sqrLen = offset.sqrMagnitude;

            if (sqrLen < _weaponStatus.Range * _weaponStatus.Range)
            {
                return enemy;
            } // 範囲内にいたら
        }

        return null;
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
        if (_rotator == null) return;
        var nextCorner = Target.transform.position;
        var to = nextCorner - _rotator.transform.position;
        var angle = Vector3.SignedAngle(_rotator.transform.forward, to, Vector3.up);
        // 角度が●゜を越えていたら
        if (Mathf.Abs(angle) > _angle)
        {
            var rotMax = _rotateSpeed * Time.deltaTime;
            var rot = Mathf.Min(Mathf.Abs(angle), rotMax);
            _rotator.transform.Rotate(0f, rot * Mathf.Sign(angle), 0f);
        }
    }

    /// <summary>
    /// 対象との距離次第で回転速度を変更する
    /// </summary>
    private void SpeedByDistance()
    {
        var offset = Target.transform.position - transform.position;
        var sqrLen = offset.sqrMagnitude;

        if (sqrLen < 5f * 5f)
        {
            _rotateSpeed = 100f;
        }
        else if (sqrLen < 10f * 10f)
        {
            _rotateSpeed = 150f;
        }
        else if (sqrLen < 25f * 25f)
        {
            _rotateSpeed = 400f;
        }
        else
        {
            _rotateSpeed = 600f;
        }
    }

    /// <summary>
    /// デバッグ用
    /// 使用目的：距離ごとに回転速度が変更されているか
    /// </summary>
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