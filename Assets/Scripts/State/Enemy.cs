using UnityEngine;
using UnityEngine.AI;

public class Enemy : StateBase
{
    #region 宣言部

    [SerializeField] private float _speed = 1f;
    [SerializeField] private Animator _animator = default;
    [SerializeField, Header("モデル")] private GameObject _model = default;
    [SerializeField, Header("経路オブジェクトの親")] private string _parentRouteName = default;
    [SerializeField, Header("弾丸プレハブ")] private GameObject _bulletPrefab = default;
    [SerializeField, Header("マズル")] private Transform _bulletSpawnPoint = default;
    [SerializeField, Header("発射インターバル")] private float _shootInterval = default;
    [SerializeField, Header("攻撃力")] private float _attackValue = 1f;
    [SerializeField, Header("回復地点")] private string _healPointName = default;
    [SerializeField, Header("回復地点で要する時間")] private float _waitTime = 3f;
    private Vector3[] _positions = default; // 経路の位置情報
    private GameObject _tower = default;
    private GameObject _parentRoute = default;
    private NavMeshAgent _agent = default;
    private WalkState _walkState = default; // 歩行
    private AttackState _attackState = default; // 攻撃
    private ChangeOfCourseState _changeOfCourseState = default; // 回転
    private EvacuationState _evacuationState = default; // 退避
    private DeathState _deathState = default; // 死亡
    private Hp _hp = default;
    private Generator _generator = default;
    private bool _canEvacuate = default; // 退避できるか ※一回キリ

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
        _attackState = new AttackState(this, _shootInterval, _bulletPrefab, _bulletSpawnPoint, _tower, _attackValue,
            _animator);
        _changeOfCourseState = new ChangeOfCourseState(this, transform, _tower.transform, _attackState, _animator);
        _walkState = new WalkState(this, transform, _agent, _positions, _changeOfCourseState, _animator);
        var go = GameObject.Find(_healPointName);
        if (go)
        {
            var healPoint = go.transform.position;
            _canEvacuate = true;
            _evacuationState =
                new EvacuationState(this, this, healPoint, _agent, transform, _hp, _waitTime, this, _walkState);
        }

        var death = GetComponents<IDeath>();
        _deathState = new DeathState(this, _generator, _animator, _model, death);
        ChangeState(_walkState);
    }

    protected override void OnUpdate()
    {
        if (_hp.CurrentHp <= 0)
        {
            ChangeState(_deathState);
            _agent.isStopped = true;
            return;
        } // HPが０なら

        if (_canEvacuate)
        {
            if (_hp.CurrentHp <= _hp.MaxHp * 0.4f)
            {
                ChangeState(_evacuationState);
                return;
            } // HPが4割以下なら
        }

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

    /// <summary>
    /// 退避状態を一回きりに制限する
    /// </summary>
    public void FalseFlag()
    {
        _canEvacuate = false;
    }
}