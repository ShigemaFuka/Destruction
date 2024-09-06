using UnityEngine;

/// <summary>
/// Transformで動く弾
/// </summary>
public class BulletTr : MonoBehaviour
{
    [SerializeField] private float _speed = 10f;
    [SerializeField, Header("攻撃力")] private float _dmg = 1f;
    [SerializeField, Header("タイムリミット")] private float _timeLimit = 0.5f;
    private float _timer = default;

    private void Update()
    {
        transform.position += transform.forward * (_speed * Time.deltaTime);
        _timer += Time.deltaTime;
        if (_timer >= _timeLimit) Destroy(gameObject);
    }

    private void OnTriggerEnter(Collider other)
    {
        // todo: 何かに接触したら仕舞う
        // ~~~
        var d = other.GetComponent<IDamage>();
        d?.Damage(_dmg);
    }
}