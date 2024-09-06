using UnityEngine;

/// <summary>
/// 俯瞰
/// WASDで移動、回転は無し
/// </summary>
public class CameraController2 : MonoBehaviour
{
    [SerializeField, Header("速度")] private float _speed = 5f;
    [SerializeField, Header("上")] private float _topLimit = 10f;
    [SerializeField, Header("下")] private float _bottomLimit = -10f;
    [SerializeField, Header("左")] private float _leftLimit = -10f;
    [SerializeField, Header("右")] private float _rightLimit = 10f;

    private void Update()
    {
        var movement = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
        transform.Translate(movement * (_speed * Time.deltaTime), Space.World);
        var clampedX = Mathf.Clamp(transform.position.x, _leftLimit, _rightLimit); // X軸の移動範囲を制限
        var clampedZ = Mathf.Clamp(transform.position.z, _bottomLimit, _topLimit); // Y軸の移動範囲を制限
        // 位置を制限された範囲内に設定
        transform.position = new Vector3(clampedX, transform.position.y, clampedZ);
    }
}