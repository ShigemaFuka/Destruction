using UnityEngine;

/// <summary>
/// ぶつかったら爆発する
/// </summary>
public class ExplosionUponImpact : MonoBehaviour
{
    private IExplosion _explosion = default;
    private GameObject _player = default;

    private void Start()
    {
        _explosion = GetComponent<IExplosion>();
        _player = GameObject.FindGameObjectWithTag("Player");
    }

    private void OnCollisionEnter(Collision other)
    {
        _explosion.DoExplosion();
        _player.GetComponent<IExplosion>()?.DoExplosion();
    }
}