using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Waveおきに敵の生成数を制限
/// </summary>
public class WaveManager : MonoBehaviour
{
    #region 宣言部

    [SerializeField, Header("タワーのHP")] private Hp _hp;

    [SerializeField, Header("各Waveの最大生成数")]
    private List<int> _maxNumsEachWaves;

    [SerializeField, Header("敵の種類を指定する　※↑の数だけ指定")]
    private List<int> _enemyTypeList;

    [SerializeField, Header("次のWaveを開始するまでの間隔")]
    private List<float> _intervals;

    public static WaveManager Instance;
    private bool _canGeneration; // 生成ができるか
    private int _currentCount; // 現在までに生成した数（各Waveにおいて）
    private int _totalCount; // 現在までに生成した総数
    private int _killCount; // 倒された数
    private int _currentWave; // 現在のWave番号
    private Generator _generator;
    private SceneChanger _sceneChanger;
    private GameManager _gameManager;

    #endregion

    #region プロパティ

    /// <summary> 生成ができるか </summary>
    public bool CanGeneration => _canGeneration;

    /// <summary> 敵の種類 </summary>
    public List<int> EnemyTypeList
    {
        get => _enemyTypeList;
        //set => _enemyTypeList = value;
    }

    public int TotalCount
    {
        get => _totalCount;
    }

    /// <summary> 倒された数 </summary>
    public int KillCount
    {
        get => _killCount;
    }

    public int CurrentWave => _currentWave;

    public int MaxWaveCount => _maxNumsEachWaves.Count;

    #endregion

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        _generator = Generator.Instance;
        _sceneChanger = SceneChanger.Instance;
        _gameManager = GameManager.Instance;
        if (_hp == null) Debug.LogWarning("HPがありません。");

        if (_maxNumsEachWaves.Count == 0)
        {
            Debug.LogWarning("_maxNumsEachWaves.Count == 0");
        }

        if (_intervals.Count == 0)
        {
            Debug.LogWarning("_intervals.Count == 0");
        }

        _canGeneration = false;
        StartCoroutine(WaitForNextWave());
    }

    private void Update()
    {
        // 最終Waveが終わったら
        if (_currentWave >= _maxNumsEachWaves.Count + 1)
        {
            Debug.Log("最終Wave 終");
            // 敵を殲滅したらシーン遷移
            if (_generator.EnemiesList.Count == 0)
            {
                StartCoroutine(_sceneChanger.LateChange()); // リザルトへ
                _gameManager.ChangeReward(_hp.CurrentHp, _hp.MaxHp);
            }

            return;
        }

        if (!_canGeneration) return;
        // 各Waveの生成上限に達したら
        if (_maxNumsEachWaves[_currentWave - 1] == _currentCount)
        {
            _canGeneration = false; // 生成停止
            _currentCount = 0;
            StartCoroutine(WaitForNextWave());
        }
    }

    /// <summary>
    /// 生成した数を加算
    /// 次のWaveに移行するたびにカウントを０にする
    /// </summary>
    /// <returns></returns>
    public void AddCount()
    {
        _currentCount++;
        _totalCount++;
    }

    public void AddKillCount()
    {
        _killCount++;
    }

    /// <summary>
    /// 各Waveの生成上限に達したら、インターバル経過後、
    /// 生成できるようにする
    /// </summary>
    /// <returns></returns>
    private IEnumerator WaitForNextWave()
    {
        // 最後は次のWaveがないため
        if (_currentWave < _maxNumsEachWaves.Count)
        {
            yield return new WaitForSeconds(_intervals[_currentWave]);
            _canGeneration = true;
        }

        _currentWave++;
    }
}