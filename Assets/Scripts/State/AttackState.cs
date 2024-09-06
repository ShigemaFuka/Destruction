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

    public AttackState(StateBase stateBase, float interval, GameObject bulletPrefab, Transform muzzle,
        GameObject target, float attackValue)
    {
        _stateBase = stateBase;
        _interval = interval;
        _bulletPrefab = bulletPrefab;
        _muzzle = muzzle;
        _target = target;
        _attackValue = attackValue;
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
        var d = _target.GetComponent<IDamage>();
        d.Damage(_attackValue);
        Object.Instantiate(_bulletPrefab, _muzzle.position, _muzzle.rotation);
        // Debug.Log("発射");
    }
}