using UnityEngine;

/// <summary>
/// カメラから見て正面へ少し上向きに放射する
/// </summary>
public class ThrowObjectForward : MonoBehaviour
{
    [SerializeField] private GameObject _objectToThrow; // 投げるオブジェクト
    [SerializeField] private Transform _throwPoint; // 投げる開始位置
    [SerializeField] private float _throwForce = 5f; // 投げる力
    private Camera _camera = default;
    private Vector3 _throwDirection = default;

    private void Start()
    {
        _camera = Camera.main;
    }

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
        _throwDirection = _throwPoint.forward + new Vector3(0, 1, 0);
        rb.velocity = _throwDirection * _throwForce;
    }
}