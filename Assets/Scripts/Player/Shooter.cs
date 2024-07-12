using UnityEngine;

public class Shooter : MonoBehaviour
{
    [SerializeField] private GameObject _bulletPrefab = default;
    [SerializeField] private Transform _bulletSpawnPoint = default;
    // [SerializeField] private float _shootForce = 20f;

    private void Update()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            Shoot();
        }
    }

    private void Shoot()
    {
        // 弾のインスタンスを生成
        var bullet = Instantiate(_bulletPrefab, _bulletSpawnPoint.position, _bulletSpawnPoint.rotation);
    }
}