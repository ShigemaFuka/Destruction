using UnityEngine;

/// <summary>
/// ダメージを与える
/// </summary>
public class InflictDamage : MonoBehaviour, IOffense
{
    public void Offense(GameObject target)
    {
        var d = target.GetComponent<IDamage>();
        d?.Damage(GetComponent<WeaponStatus>().Attack);
    }
}