using System.Linq;
using UnityEngine;

/// <summary>
/// 攻撃状態
/// </summary>
public class AttackState : IState
{
    private readonly StateBase _stateBase = default;
    private float _interval = default;
    private GameObject _bulletPrefab = default;
    private float _timer = default;
    private Transform _muzzle = default;
    private GameObject _target = default;
    private float _attackValue = default;
    private Animator _animator = default;
    private static readonly int Attack1 = Animator.StringToHash("Attack");

    public AttackState(StateBase owner)
    {
        _stateBase = owner;
        var script = owner.GetComponent<Enemy>();
        _interval = script.ShootInterval;
        _bulletPrefab = script.BulletPrefab;
        _muzzle = owner.transform.GetComponentsInChildren<Transform>()
            .FirstOrDefault(t => t.name == "Muzzle");
        _target = GameObject.FindWithTag("Tower");
        _attackValue = script.AttackValue;
        _animator = owner.GetComponent<Animator>();
    }

    public void Enter()
    {
        _timer = 0;
        // Debug.Log("Enter Attack State");
    }

    public void Execute()
    {
        _timer += Time.deltaTime;
        if (_timer >= _interval)
        {
            Attack();
            _timer = 0;
        }
    }

    public void Exit()
    {
        // Debug.Log("Exit Attack State");
    }

    private void Attack()
    {
        // var d = _target.GetComponent<IDamage>();
        // d.Damage(_attackValue);

        var ds = _target.GetComponents<IDamage>();
        foreach (var d in ds)
        {
            d?.Damage(_attackValue);
        }

        if (_animator) _animator.SetTrigger(Attack1);
        Object.Instantiate(_bulletPrefab, _muzzle.position, _muzzle.rotation);
        // Debug.Log("発射");
    }
}