using UnityEngine;

/// <summary>
/// Waveごとに演出を行う
/// </summary>
public class PlayingByWave : MonoBehaviour
{
    [SerializeField] private Animator _anim;
    private WaveManager _waveManager;

    private void Start()
    {
        _waveManager = WaveManager.Instance;
    }

    private void Update()
    {
        // 最終Waveにならば
        if (_waveManager.CurrentWave >= _waveManager.MaxWaveCount - 1)
        {
            _anim.Play("Sliding");
        }
    }
}