using UnityEngine;

public class OffenseState : IState
{
    private Weapon _weapon = default;
    private GameObject _target = default;
    private GameObject _bulletPrefab = default;
    private Transform _muzzle = default;
    private GameObject _weaponObject = default; // 武器自体

    public OffenseState(Weapon weapon, GameObject bulletPrefab, Transform bulletSpawnPoint,
        GameObject weaponObject)
    {
        _weapon = weapon;
        _bulletPrefab = bulletPrefab;
        _muzzle = bulletSpawnPoint;
        _weaponObject = weaponObject;
    }

    public void Enter()
    {
        _target = _weapon.Target;
        if (_bulletPrefab) Object.Instantiate(_bulletPrefab, _muzzle.position, _muzzle.rotation);
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
        var offenses = _weaponObject.GetComponents<IOffense>();
        foreach (var offense in offenses)
        {
            offense?.Offense(_target);
        }

        Exit();
    }
}