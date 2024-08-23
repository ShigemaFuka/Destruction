using UnityEngine;

/// <summary>
/// 弾を前方に移動させる
/// </summary>
public class BulletRB : MonoBehaviour
{
    public float _speed = 10f;
    public float _lifetime = 5f;
    private Rigidbody _rd = default;

    private void Start()
    {
        // 一定時間後に弾を破壊する
        // Destroy(gameObject, _lifetime);
        _rd = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        _rd.velocity = transform.forward * _speed;
    }
}