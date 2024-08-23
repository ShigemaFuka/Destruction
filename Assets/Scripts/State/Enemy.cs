using UnityEngine;
using UnityEngine.AI;

public class Enemy : StateBase
{
    [SerializeField] private float _speed = 1f;
    [SerializeField, Header("経路オブジェクトの親")] private string _parentRouteName = default;
    [SerializeField] private GameObject _bulletPrefab = default;
    [SerializeField] private Transform _bulletSpawnPoint = default;
    [SerializeField] private float _shootInterval = default;
    private Vector3[] _positions = default; // 経路の位置情報
    private Transform _tower = default;
    private GameObject _parentRoute = default;
    private NavMeshAgent _agent = default;
    private WalkState _walkState = default;
    private AttackState _attackState = default;
    private IdleState _idleState = default;

    protected override void OnStart()
    {
        _parentRoute = GameObject.Find(_parentRouteName);
        GetRoute();
        _agent = GetComponent<NavMeshAgent>();
        _agent.speed = _speed;
        _tower = GameObject.FindWithTag("Tower").transform;
        _attackState = new AttackState(this, _shootInterval, _bulletPrefab, _bulletSpawnPoint);
        _idleState = new IdleState(this, transform, _tower, _attackState);
        _walkState = new WalkState(this, transform, _agent, _positions, _idleState);
        ChangeState(_walkState);
    }

    protected override void OnUpdate()
    {
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