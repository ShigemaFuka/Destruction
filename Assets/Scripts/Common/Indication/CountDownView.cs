using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// 第一Wave開始までのカウントダウン
/// </summary>
public class CountDownView : MonoBehaviour
{
    [SerializeField] private Text _text;

    public void UpdateText(float amount)
    {
        _text.text = amount.ToString("0");
    }

    /// <summary>
    /// カウントダウン終了したら、表示を止める
    /// </summary>
    /// <param name="isActive"></param>
    public void SetActive(bool isActive)
    {
        gameObject.SetActive(isActive);
    }
}