using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 一定間隔で敵を生成する
/// </summary>
public class Generator : MonoBehaviour
{
    [SerializeField] private GameObject[] _prefabs;
    [SerializeField, Header("インターバル")] private float _interval = 1f;
    public static Generator Instance;
    private float _timer;
    private List<GameObject> _enemiesList; // 生成した物
    private WaveManager _waveManager; // 生成数の制限

    public List<GameObject> EnemiesList => _enemiesList;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        _enemiesList = new List<GameObject>();
        _waveManager = WaveManager.Instance;
        if (!_waveManager) Debug.LogWarning($"{_waveManager.name}が見つかりませんでした。");
    }

    private void Update()
    {
        if (!_waveManager.CanGeneration) return; // 生成できないならリターン
        _timer += Time.deltaTime;
        if (_timer >= _interval)
        {
            _enemiesList.Add(Generate());
            _timer = 0;
        }
    }

    private GameObject Generate()
    {
        var num = _waveManager.EnemyTypeList[_waveManager.TotalCount];
        if (num >= _waveManager.EnemyTypeList.Count) return null;
        var go = Instantiate(_prefabs[num], transform);
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