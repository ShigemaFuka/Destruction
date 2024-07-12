using System.Collections;
using UnityEngine;

/// <summary>
/// 時限爆弾
/// Explosionとともにアタッチ
/// </summary>
public class TimedExplosion : MonoBehaviour
{
    [SerializeField] private float _limit = 3f;
    private WaitForSeconds _wfs = default;
    private GameObject _player = default;
    private IExplosion _selfExplosion = default;
    private IExplosion _playerExplosion = default;

    private void Start()
    {
        _wfs = new WaitForSeconds(_limit);
        _selfExplosion = GetComponent<IExplosion>();
        _player = GameObject.FindGameObjectWithTag("Player");
        _playerExplosion = _player.GetComponent<IExplosion>();
    }

    // 何かに触れたらタイマー起動
    private void OnCollisionEnter(Collision other)
    {
        StartCoroutine(Launch());
    }

    private IEnumerator Launch()
    {
        yield return _wfs;
        _selfExplosion.DoExplosion();
        _playerExplosion?.DoExplosion();
    }
}