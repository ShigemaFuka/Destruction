using System;
using UnityEngine;

/// <summary>
/// Transformで動く弾
/// </summary>
public class BulletTr : MonoBehaviour
{
    public float _speed = 10f;

    private void Update()
    {
        transform.position += transform.forward * (_speed * Time.deltaTime);
    }

    private void OnTriggerEnter(Collider other)
    {
        // todo: 何かに接触したら仕舞う
    }
}