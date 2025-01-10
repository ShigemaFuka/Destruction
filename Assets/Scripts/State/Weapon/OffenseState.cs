using System.Linq;
using UnityEngine;

public class OffenseState : IState
{
    private Weapon _weapon = default;
    private GameObject _target = default;
    private GameObject _bulletPrefab = default;
    private Transform _muzzle = default;
    private GameObject _weaponObject = default; // 武器自体

    public OffenseState(StateBase owner)
    {
        _weapon = owner.gameObject.GetComponent<Weapon>();
        _bulletPrefab = _weapon.BulletPrefab;
        _muzzle = owner.transform.GetComponentsInChildren<Transform>()
            .FirstOrDefault(t => t.name == "Muzzle");
        _weaponObject = owner.gameObject;
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