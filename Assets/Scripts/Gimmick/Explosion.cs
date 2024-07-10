using UnityEngine;

/// <summary>
/// 爆発する
/// 自身を中心として周囲に力を加える
/// ※自身が自滅する前提
/// </summary>
public class Explosion : MonoBehaviour, IExplosion
{
    [SerializeField] private float _forceMagnitude = 10f;
    [SerializeField, Header("範囲")] private float _range = 5f;
    private Collider[] _colliders = new Collider[20];
    [SerializeField, Header("爆発エフェクト")] private GameObject _explosionEffectPrefab = default;

    private void Update()
    {
    }

    public void DoExplosion()
    {
        var size = Physics.OverlapSphereNonAlloc(transform.position, _range, _colliders);
        for (var i = 0; i < size; i++)
        {
            if (_colliders[i] == null) continue;
            var receiveBlast = _colliders[i].GetComponent<IReceiveBlast>();
            if (receiveBlast != null) receiveBlast.DoReceiveBlast(_forceMagnitude, gameObject);
        }

        if (_explosionEffectPrefab)
            Instantiate(_explosionEffectPrefab, gameObject.transform.position, Quaternion.identity);
        Destroy(gameObject);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, _range);
    }
}