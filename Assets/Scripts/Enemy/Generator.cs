using System.Collections.Generic;
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
    private List<GameObject> _enemiesList = default; // 生成した物

    public List<GameObject> EnemiesList => _enemiesList;

    private void Start()
    {
        _useInitial = true;
        _enemiesList = new List<GameObject>();
    }

    private void Update()
    {
        _timer += Time.deltaTime;
        if (_useInitial)
        {
            if (_timer >= _initialInterval)
            {
                _enemiesList.Add(Generate());
                _timer = 0;
                _useInitial = false;
            }
        }
        else
        {
            if (_timer >= _interval)
            {
                _enemiesList.Add(Generate());
                _timer = 0;
            }
        }
    }

    private GameObject Generate()
    {
        return Instantiate(_prefab, transform);
    }

    public void RemoveObj(GameObject go)
    {
        _enemiesList.Remove(go);
    }
}