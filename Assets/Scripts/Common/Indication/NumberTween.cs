using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// 数値を数秒かけて表示する
/// </summary>
public class NumberTween : MonoBehaviour
{
    [SerializeField] private Text _text = default;
    [SerializeField, Header("表示に何秒かけるか")] private float _duration = 0.5f;
    private float num = 0;

    private void Start()
    {
        if (_text == null) Debug.LogWarning("テキストコンポーネントがありません。");
    }

    public void NumTween(float endNum)
    {
        DOTween.To(() => num, x =>
        {
            num = x;
            UpdateNumberText(); // テキストを更新
        }, endNum, _duration);
    }

    private void UpdateNumberText()
    {
        _text.text = num.ToString("0.0");
    }
}