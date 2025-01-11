using UnityEngine;
using UnityEngine.AI;

/// <summary>
/// ステートコントローラー
/// エネミーは別で
/// </summary>
public class Enemy : StateBase
{
    #region 宣言部

    [SerializeField] private float _speed = 1f;
    [SerializeField, Header("モデル")] private GameObject _model;
    [SerializeField, Header("経路オブジェクトの親")] private string _parentRouteName = default;
    [SerializeField, Header("弾丸プレハブ")] private GameObject _bulletPrefab;
    [SerializeField, Header("発射インターバル")] private float _shootInterval;
    [SerializeField, Header("攻撃力")] private float _attackValue = 1f;
    [SerializeField, Header("回復地点")] private string _healPointName;
    [SerializeField, Header("回復地点で要する時間")] private float _waitTime = 3f;
    private NavMeshAgent _agent;
    private WalkState _walkState; // 歩行
    private AttackState _attackState; // 攻撃
    private ChangeOfCourseState _changeOfCourseState; // 回転
    private EvacuationState _evacuationState; // 退避
    private DeathState _deathState; // 死亡
    private Hp _hp;
    private bool _canEvacuate; // 退避できるか ※一回キリ

    #endregion

    #region プロパティ

    public GameObject Model => _model;
    public string ParentRouteName => _parentRouteName;
    public GameObject BulletPrefab => _bulletPrefab;
    public float ShootInterval => _shootInterval;
    public float AttackValue => _attackValue;
    public float WaitTime => _waitTime;

    #endregion

    protected override void OnStart()
    {
        _hp = GetComponent<Hp>();
        _agent = GetComponent<NavMeshAgent>();
        _agent.speed = _speed;
        _attackState = new AttackState(this);
        _changeOfCourseState = new ChangeOfCourseState(this, _attackState);
        _walkState = new WalkState(this, _changeOfCourseState);
        var go = GameObject.Find(_healPointName);
        if (go)
        {
            _canEvacuate = true;
            _evacuationState =
                new EvacuationState(this, _walkState);
        }

        _deathState = new DeathState(this);
        ChangeState(_walkState);
    }

    protected override void OnUpdate()
    {
        if (_currentState == _deathState) return;
        if (_hp.CurrentHp <= 0)
        {
            ChangeState(_deathState);
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
    /// 退避状態を一回きりに制限する
    /// </summary>
    public void FalseFlag()
    {
        _canEvacuate = false;
    }
}