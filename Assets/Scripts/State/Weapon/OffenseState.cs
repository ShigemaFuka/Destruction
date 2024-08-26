using UnityEngine;

public class OffenseState : IState
{
    private Weapon _weapon = default;
    private GameObject _target = default;
    private float _dmg = default;
    private GameObject _bulletPrefab = default;
    private Transform _muzzle = default;

    public OffenseState(Weapon weapon, float dmg, GameObject bulletPrefab, Transform bulletSpawnPoint)
    {
        _weapon = weapon;
        _dmg = dmg;
        _bulletPrefab = bulletPrefab;
        _muzzle = bulletSpawnPoint;
    }

    public void Enter()
    {
        _target = _weapon.Target;
        Object.Instantiate(_bulletPrefab, _muzzle.position, _muzzle.rotation);
        Offense();
        Exit();
        // Debug.Log("Enter Offense State");
    }

    public void Execute()
    {
    }

    public void Exit()
    {
        // Debug.Log("Exit Offense State");
    }

    private void Offense()
    {
        var d = _target.GetComponent<IDamage>();
        d?.Damage(_dmg);
        Exit();
    }
}