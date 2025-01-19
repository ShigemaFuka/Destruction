using System;
using UnityEngine;
using UnityEngine.UI;

public class TimeScaleView : MonoBehaviour
{
    public Text _text;
    public Button _targetButton;

    private void Start()
    {
        UpdateText(1);
    }

    public void UpdateText(float count)
    {
        _text.text = $"×{count}";
    }
}