using System;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 一定間隔で敵を生成する
/// </summary>
public class Generator : MonoBehaviour
{
    [SerializeField] private GameObject[] _prefabs = default;
    [SerializeField, Header("初期インターバル")] private float _initialInterval = 2f;
    [SerializeField, Header("インターバル")] private float _interval = 1f;
    public static Generator Instance = default;
    private float _timer = default;
    private bool _useInitial = default; // 初期インターバルを使うか
    private List<GameObject> _enemiesList = default; // 生成した物
    private WaveManager _waveManager = default; // 生成数の制限

    public List<GameObject> EnemiesList => _enemiesList;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        _useInitial = true;
        _enemiesList = new List<GameObject>();
        _waveManager = FindObjectOfType<WaveManager>();
        if (!_waveManager) Debug.LogWarning($"{_waveManager.name}が見つかりませんでした。");
    }

    private void Update()
    {
        if (!_waveManager.CanGeneration) return; // 生成できないならリターン
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
        var go = Instantiate(_prefabs[_waveManager.EnemyTypeList[_waveManager.TotalCount]], transform);
        _waveManager.AddCount(); // 生成のたびに個数を加算（各Waveにおいて）
        return go;
    }

    public void RemoveObj(GameObject go)
    {
        if (_enemiesList.Contains(go))
        {
            _enemiesList.Remove(go);
            _waveManager.AddKillCount(); // 死亡数加算
        }
    }
}