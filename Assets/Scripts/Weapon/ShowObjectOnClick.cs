using UnityEngine;

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
        Debug.Log($"{gameObject.name}が押されたよ");
    }
}