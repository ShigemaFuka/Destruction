using UnityEngine;

/// <summary>
/// 設置する武器の一覧を表示
/// </summary>
public class ShowObjectOnClick : MonoBehaviour
{
    [SerializeField] private GameObject _panel = default; // 武器一覧

    private void Start()
    {
        _panel.SetActive(false);
    }

    public void OnClick()
    {
        _panel.SetActive(!_panel.activeSelf);
    }
}