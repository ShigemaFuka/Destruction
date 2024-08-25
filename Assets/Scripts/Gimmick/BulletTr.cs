using System;
using UnityEngine;

/// <summary>
/// Transformで動く弾
/// </summary>
public class BulletTr : MonoBehaviour
{
    [SerializeField] private float _speed = 10f;
    [SerializeField, Header("攻撃力")] private float _dmg = 1f;

    private void Update()
    {
        transform.position += transform.forward * (_speed * Time.deltaTime);
    }

    private void OnTriggerEnter(Collider other)
    {
        // todo: 何かに接触したら仕舞う
        // ~~~
        var d = other.GetComponent<IDamage>();
        d?.Damage(_dmg);
    }
}