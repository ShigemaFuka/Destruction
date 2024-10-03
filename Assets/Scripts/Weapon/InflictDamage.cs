using UnityEngine;

/// <summary>
/// ダメージを与える
/// </summary>
public class InflictDamage : MonoBehaviour, IOffense
{
    [SerializeField] private Animator _animator = default;
    private static readonly int Attack = Animator.StringToHash("Attack");

    public void Offense(GameObject target)
    {
        if (_animator) _animator.SetTrigger(Attack);
        var d = target.GetComponent<IDamage>();
        d?.Damage(GetComponent<WeaponStatus>().Attack);
    }
}