using UnityEngine;
using Cinemachine;

/// <summary>
/// 数秒間カメラを揺らす
/// 爆発の揺れのようなもの
/// </summary>
public class ShakeCamera : MonoBehaviour, IExplosion
{
    [SerializeField] private GameObject _virtualCamera = default;
    [SerializeField, Header("シェイクの持続時間")] private float _shakeDuration = 0.2f;

    [SerializeField, Header("振幅が大きいほど、カメラの揺れが強い")]
    private float _shakeAmplitude = 1.2f;

    [SerializeField, Header("周波数が高いほど、カメラの揺れが速い")]
    private float _shakeFrequency = 2.0f;

    private CinemachineImpulseSource _impulseSource = default;
    private CinemachineImpulseListener _impulseListener = default;


    private void Start()
    {
        _impulseSource = _virtualCamera.GetComponent<CinemachineImpulseSource>();
        _impulseListener = _virtualCamera.GetComponent<CinemachineImpulseListener>();
        _impulseListener.m_ReactionSettings.m_AmplitudeGain = _shakeAmplitude;
        _impulseListener.m_ReactionSettings.m_FrequencyGain = _shakeFrequency;
        _impulseListener.m_ReactionSettings.m_Duration = _shakeDuration;
    }

    /// <summary>
    /// カメラを揺らす
    /// </summary>
    public void DoExplosion()
    {
        _impulseSource.GenerateImpulse();
    }
}