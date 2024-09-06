using UnityEngine;
using UnityEngine.AI;

public class Enemy : StateBase
{
    #region 宣言部

    [SerializeField] private float _speed = 1f;
    [SerializeField, Header("経路オブジェクトの親")] private string _parentRouteName = default;
    [SerializeField, Header("弾丸プレハブ")] private GameObject _bulletPrefab = default;
    [SerializeField, Header("マズル")] private Transform _bulletSpawnPoint = default;
    [SerializeField, Header("発射インターバル")] private float _shootInterval = default;
    [SerializeField, Header("死亡プレハブ")] private GameObject _deadPrefab = default;
    [SerializeField, Header("攻撃力")] private float _attackValue = 1f;
    private Vector3[] _positions = default; // 経路の位置情報
    private GameObject _tower = default;
    private GameObject _parentRoute = default;
    private NavMeshAgent _agent = default;
    private WalkState _walkState = default;
    private AttackState _attackState = default;
    private ChangeOfCourseState _changeOfCourseState = default;
    private DeathState _deathState = default;
    private Hp _hp = default;
    private Generator _generator = default;

    #endregion

    protected override void OnStart()
    {
        _hp = GetComponent<Hp>();
        _parentRoute = GameObject.Find(_parentRouteName);
        GetRoute();
        _agent = GetComponent<NavMeshAgent>();
        _agent.speed = _speed;
        _tower = GameObject.FindWithTag("Tower");
        _generator = FindObjectOfType<Generator>();
        _attackState = new AttackState(this, _shootInterval, _bulletPrefab, _bulletSpawnPoint, _tower, _attackValue);
        _changeOfCourseState = new ChangeOfCourseState(this, transform, _tower.transform, _attackState);
        _walkState = new WalkState(this, transform, _agent, _positions, _changeOfCourseState);
        _deathState = new DeathState(this, _deadPrefab, transform, gameObject, _generator);
        ChangeState(_walkState);
    }

    protected override void OnUpdate()
    {
        if (_hp.CurrentHp <= 0)
        {
            ChangeState(_deathState);
        } // 残機０なら

        if (_agent.velocity.magnitude > 0.1f)
        {
            ChangeState(_walkState);
        } // Agentが動いていたら（終点未到達なら）
        // 吹き飛ばされたときなど 後退 → 戦闘復帰を想定
    }

    /// <summary>
    /// 経路を取得
    /// </summary>
    private void GetRoute()
    {
        var childCount = _parentRoute.transform.childCount;
        _positions = new Vector3[childCount];
        for (var i = 0; i < _positions.Length; i++)
        {
            _positions[i] = _parentRoute.transform.GetChild(i).transform.position;
        }
    }
}