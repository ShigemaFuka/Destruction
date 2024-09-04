using UnityEngine;

/// <summary>
/// FPSカメラ
/// カーソルに合わせて回転
/// </summary>
public class CameraController : MonoBehaviour
{
    [SerializeField] private float _sensitivity = 100f;
    [SerializeField] private Transform _playerBody;
    private float _xRotation = 0f;

    private void Start()
    {
        // マウスカーソルをロックする
        Cursor.lockState = CursorLockMode.Locked;
    }

    private void Update()
    {
        // マウスの動きを取得
        var mouseX = Input.GetAxis("Mouse X") * _sensitivity * Time.deltaTime;
        var mouseY = Input.GetAxis("Mouse Y") * _sensitivity * Time.deltaTime;

        // 垂直方向の回転を制限
        _xRotation -= mouseY;
        _xRotation = Mathf.Clamp(_xRotation, -90f, 90f);

        // カメラの回転を更新
        transform.localRotation = Quaternion.Euler(_xRotation, 0f, 0f);
        _playerBody.Rotate(Vector3.up * mouseX);
    }
}