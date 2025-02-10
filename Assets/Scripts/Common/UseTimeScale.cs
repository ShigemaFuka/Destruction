using System;
using UniRx;
using UnityEngine;

public class UseTimeScale : MonoBehaviour
{
    [SerializeField] private float[] _nums;
    private int _index;
    public ReactiveProperty<float> Num { get; private set; } = new ReactiveProperty<float>(0);

    private void Start()
    {
        Num.Value = _nums[_index];
    }

    public void ChangeNum()
    {
        _index = (_index + 1) % _nums.Length;
        Num.Value = _nums[_index];
        TimeScaleChanger.Instance.ChangeTimeScale(Num.Value);
    }
}