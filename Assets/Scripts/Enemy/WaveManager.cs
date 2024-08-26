using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Waveおきに敵の生成数を制限
/// </summary>
public class WaveManager : MonoBehaviour
{
    [SerializeField, Header("各Waveの最大生成数")]
    private List<int> _maxNumsEachWaves = default;

    [SerializeField, Header("次のWaveを開始するまでの間隔")]
    private List<float> _intervals = default;

    [SerializeField] private bool _canGeneration = default; // 生成ができるか
    [SerializeField] private int _currentCount = default; // 現在までに生成した数（各Waveにおいて）
    [SerializeField] private int _wave = default; // 現在のWave番号

    /// <summary> 生成ができるか </summary>
    public bool CanGeneration => _canGeneration;

    private void Start()
    {
        if (_maxNumsEachWaves.Count == 0)
        {
            Debug.LogWarning("_maxNumsEachWaves.Count == 0");
        }

        if (_intervals.Count == 0)
        {
            Debug.LogWarning("_intervals.Count == 0");
        }

        _canGeneration = true;
    }

    private void Update()
    {
        // 最終Waveが終わったら
        if (_wave == _maxNumsEachWaves.Count)
        {
            Debug.Log($"最終Wave 終" +
                      $"\n current wave: {_wave}  Waveのリスト:{_maxNumsEachWaves.Count}");
            _canGeneration = false;
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
    }

    /// <summary>
    /// 各Waveの生成上限に達したら、インターバル経過後、
    /// 生成できるようにする
    /// </summary>
    /// <returns></returns>
    private IEnumerator WaitForNextWave()
    {
        if (_wave == _maxNumsEachWaves.Count - 1) yield break; // 最後は次のWaveがない
        yield return new WaitForSeconds(_intervals[_wave]);
        _canGeneration = true;
    }
}