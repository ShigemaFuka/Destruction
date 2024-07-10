using UnityEngine;

/// <summary>
/// 「カメラから見た方向」にキャラクターを動かす。
/// </summary>
[RequireComponent(typeof(Rigidbody))]
public class CharacterMove : MonoBehaviour
{
    [SerializeField] private float _speed = 5f;
    private Rigidbody _rb;
    private Transform _cameraTransform;

    private void Start()
    {
        _rb = GetComponent<Rigidbody>();
        _cameraTransform = Camera.main.transform;
    }

    private void Update()
    {
        // プレイヤーの移動
        var moveHorizontal = Input.GetAxis("Horizontal");
        var moveVertical = Input.GetAxis("Vertical");
        var movement = (_cameraTransform.forward * moveVertical 
                        + _cameraTransform.right * moveHorizontal).normalized * _speed;
        _rb.velocity = new Vector3(movement.x, _rb.velocity.y, movement.z);
    }
}