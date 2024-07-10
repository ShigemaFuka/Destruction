using UnityEngine;

/// <summary>
/// 爆発の影響を受けて吹き飛ぶ
/// </summary>
public class ReceiveBlast : MonoBehaviour, IReceiveBlast
{
    private Rigidbody _rb = default;

    private void Start()
    {
        _rb = GetComponent<Rigidbody>();
    }

    /// <summary>
    /// 爆心地と反対の方に自身を吹き飛ばす
    /// </summary>
    /// <param name="power"></param>
    /// <param name="epicenter"> 爆心地 </param>
    public void DoReceiveBlast(float power, GameObject epicenter)
    {
        var forceDirection = (transform.position - epicenter.transform.position).normalized;
        _rb.velocity = forceDirection * power;
    }
}