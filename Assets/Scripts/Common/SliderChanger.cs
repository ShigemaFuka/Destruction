using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

/// <summary>
/// 残りHPをスライダーで表示
/// </summary>
public class SliderChanger : MonoBehaviour
{
    [SerializeField] private Slider _slider = default;
    [SerializeField] private Hp _hp = default;
    [SerializeField, Header("アニメーションの時間")] private float _duration = 0.2f;
    private float _previousHp = default; // 変化前のHp

    private void Start()
    {
        _slider.maxValue = _hp.MaxHp;
    }

    private void Update()
    {
        if (_hp.CurrentHp != _previousHp)
        {
            _slider.DOValue(_hp.CurrentHp, _duration).OnComplete(() => { _previousHp = _hp.CurrentHp; });
        }
    }
}