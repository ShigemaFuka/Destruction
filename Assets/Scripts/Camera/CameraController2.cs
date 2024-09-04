using UnityEngine;

/// <summary>
/// 俯瞰
/// WASDで移動、回転は無し
/// </summary>
public class CameraController2 : MonoBehaviour
{
    [SerializeField, Header("速度")] private float _speed = 5f;

    private void Update()
    {
        var horizontalInput = Input.GetAxis("Horizontal"); // AD
        var verticalInput = Input.GetAxis("Vertical"); // WS
        var movement = verticalInput * transform.up + horizontalInput * transform.right;
        transform.Translate(movement * (_speed * Time.deltaTime), Space.World);
    }
}