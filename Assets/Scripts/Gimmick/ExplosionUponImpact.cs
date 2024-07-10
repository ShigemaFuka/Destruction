using System;
using UnityEngine;

/// <summary>
/// ぶつかったら爆発する
/// </summary>
public class ExplosionUponImpact : MonoBehaviour
{
    private IExplosion _explosion = default;

    private void Start()
    {
        _explosion = GetComponent<IExplosion>();
    }

    private void OnCollisionEnter(Collision other)
    {
        _explosion.DoExplosion();
    }
}