using UnityEngine;

/// <summary>
/// 常にカメラへ向く
/// </summary>
public class UIRotation : MonoBehaviour
{
    private void LateUpdate()
    {
        transform.rotation = Camera.main.transform.rotation;
    }
}