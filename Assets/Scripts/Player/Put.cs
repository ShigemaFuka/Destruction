using UnityEngine;

public class Put : MonoBehaviour
{
    [SerializeField, Header("配置予定場所のマーク")] private GameObject _markObjectPrefab = default;
    [SerializeField, Header("置きたいオブジェクト")] private GameObject _prefab = default;
    [SerializeField] private float _rayDistance = 10f;
    [SerializeField, Header("高さ調整")] private float _positionY = 1f;
    private Vector3 _putPosition = default;
    private GameObject _mark = default;
    private Camera _mainCamera = default;
    private Vector3 _hitPosition = new Vector3(0, 0, 10f);
    private Ray _ray = default;

    private void Start()
    {
        _mainCamera = Camera.main;
        _mark = Instantiate(_markObjectPrefab);
    }

    private void Update()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            PutObject();
        }

        Reserve();
        _putPosition = _hitPosition + new Vector3(0, _positionY, 0);
        _mark.transform.position = _putPosition;
    }

    private void Reserve()
    {
        _ray = _mainCamera.ScreenPointToRay(Input.mousePosition);
        Debug.DrawRay(_ray.origin, _ray.direction * _rayDistance, Color.red, _rayDistance);
        if (Physics.Raycast(_ray, out var hit, _rayDistance))
        {
            if (!hit.collider.gameObject.CompareTag("Ground")) return;
            _hitPosition = hit.point;
        }
    }

    private void PutObject()
    {
        Instantiate(_prefab, _putPosition, Quaternion.identity);
    }
}