using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// カーソルがかざされたときに、ステージの説明文を表示する
/// </summary>
public class ShowStageInfo : MonoBehaviour
{
    [SerializeField, Header("説明文")] [TextArea(3, 7)]
    private string _info = default;

    [SerializeField] private Text _text = default;

    private void Start()
    {
        if (_info == string.Empty) Debug.LogWarning("テキストが空です。");
    }

    public void Show()
    {
        _text.text = _info;
    }
}