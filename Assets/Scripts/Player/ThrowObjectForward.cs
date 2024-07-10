using UnityEngine;

/// <summary>
/// カメラから見て正面へ少し上向きに放射する
/// </summary>
public class ThrowObjectForward : MonoBehaviour
{
    [SerializeField] private GameObject _objectToThrow; // 投げるオブジェクト
    [SerializeField] private Transform _throwPoint; // 投げる開始位置
    [SerializeField] private float _throwForce = 5f; // 投げる力

    private void Update()
    {
        // スペースキーを押したときに投げる
        if (Input.GetButtonDown("Fire1"))
        {
            Throw();
        }
    }

    private void Throw()
    {
        // オブジェクトを投げる位置に生成
        var thrownObject = Instantiate(_objectToThrow, _throwPoint.position, _throwPoint.rotation);
        var rb = thrownObject.GetComponent<Rigidbody>();
        var throwDirection = _throwPoint.forward + _throwPoint.up;
        rb.velocity = throwDirection * _throwForce;
    }
}