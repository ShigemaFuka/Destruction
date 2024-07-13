using UnityEngine;

/// <summary>
/// 一定間隔で生成する
/// </summary>
public class Generator : MonoBehaviour
{
    [SerializeField] private GameObject _prefab = default;
    [SerializeField, Header("初期インターバル")] private float _initialInterval = 2f;
    [SerializeField, Header("インターバル")] private float _interval = 1f;
    private float _timer = default;
    private bool _useInitial = default; // 初期インターバルを使うか

    private void Start()
    {
        _useInitial = true;
    }

    private void Update()
    {
        _timer += Time.deltaTime;
        if (_useInitial)
        {
            if (_timer >= _initialInterval)
            {
                Generate();
                _timer = 0;
                _useInitial = false;
            }
        }
        else
        {
            if (_timer >= _interval)
            {
                Generate();
                _timer = 0;
            }
        }
    }

    private void Generate()
    {
        Instantiate(_prefab, transform);
    }
}