using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Waveおきに敵の生成数を制限
/// </summary>
public class WaveManager : MonoBehaviour
{
    #region 宣言部

    [SerializeField, Header("タワーのHP")] private Hp _hp = default;

    [SerializeField, Header("各Waveの最大生成数")]
    private List<int> _maxNumsEachWaves = default;

    [SerializeField, Header("敵の種類を指定する　※↑の数だけ指定")]
    private List<int> _enemyTypeList = default;

    [SerializeField, Header("次のWaveを開始するまでの間隔")]
    private List<float> _intervals = default;

    private bool _canGeneration = default; // 生成ができるか
    private int _currentCount = default; // 現在までに生成した数（各Waveにおいて）
    private int _totalCount = default; // 現在までに生成した総数
    private int _wave = default; // 現在のWave番号
    private Generator _generator = default;
    private SceneChanger _sceneChanger = default;
    private GameManager _gameManager = default;

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

    #endregion

    private void Start()
    {
        _generator = FindObjectOfType<Generator>();
        _sceneChanger = FindObjectOfType<SceneChanger>();
        _gameManager = FindObjectOfType<GameManager>();
        if (_hp == null) Debug.LogWarning($"{_hp.name}がありません。");

        if (_maxNumsEachWaves.Count == 0)
        {
            Debug.LogWarning("_maxNumsEachWaves.Count == 0");
        }

        if (_intervals.Count == 0)
        {
            Debug.LogWarning("_intervals.Count == 0");
        }

        _canGeneration = true;
        _gameManager.ResetTmpCoin();
    }

    private void Update()
    {
        // 最終Waveが終わったら
        if (_wave == _maxNumsEachWaves.Count)
        {
            Debug.Log($"最終Wave 終" +
                      $"\n current wave: {_wave}  Waveのリスト:{_maxNumsEachWaves.Count}");
            _canGeneration = false;
            // 敵を殲滅したらシーン遷移
            if (_generator.EnemiesList.Count == 0)
            {
                StartCoroutine(_sceneChanger.LateChange()); // リザルトへ
                _gameManager.ChangeReward(_hp.CurrentHp, _hp.MaxHp);
                Debug.Log($"tmp reward: {_gameManager.TemporaryCustodyCoin}");
            }

            return;
        }

        // 各Waveの生成上限に達したら
        if (_maxNumsEachWaves[_wave] == _currentCount)
        {
            _canGeneration = false; // 生成停止
            _currentCount = 0;
            _wave++;
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

    /// <summary>
    /// 各Waveの生成上限に達したら、インターバル経過後、
    /// 生成できるようにする
    /// </summary>
    /// <returns></returns>
    private IEnumerator WaitForNextWave()
    {
        if (_wave == _maxNumsEachWaves.Count) yield break; // 最後は次のWaveがない
        yield return new WaitForSeconds(_intervals[_wave]);
        _canGeneration = true;
    }
}