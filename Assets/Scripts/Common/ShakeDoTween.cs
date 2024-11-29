using UnityEngine;
using DG.Tweening;

public class ShakeDoTween : MonoBehaviour, IDamage
{
    [SerializeField] private Transform _cam = default;
    [SerializeField] private Vector3 _positionStrength = new(0.2f, 0.2f, 0.2f);
    [SerializeField] private Vector3 _rotationStrength = new(2, 2, 2);
    [SerializeField] private float _shakeDuration = 0.3f;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.B))
        {
            CameraShaker();
        }
    }

    private void CameraShaker()
    {
        _cam.DOComplete();
        _cam.DOShakePosition(_shakeDuration, _positionStrength);
        _cam.DOShakeRotation(_shakeDuration, _rotationStrength);
    }

    public void Damage(float value)
    {
        CameraShaker();
    }
}