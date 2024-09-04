using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// 生成済みの武器のステータスを表示
/// </summary>
public class UpdateInfoText : MonoBehaviour
{
    [SerializeField, Header("ステータス")] private WeaponStatus _weaponStatus = default;
    [SerializeField, Header("InfoText")] private Text _infoText = default;
    [SerializeField, Header("押すボタン")] private Button _targetButton = default;

    private void Start()
    {
        if (_weaponStatus == null) Debug.LogWarning($"{_weaponStatus.name}がありません。");
        if (_targetButton == null) Debug.LogWarning($"{_targetButton.name}がありません。");
        _targetButton.onClick.AddListener(OnClick);
        _infoText.text = "";
        GetSetStatus();
    }

    private void GetSetStatus()
    {
        var status = _weaponStatus;
        _infoText.text = $"Lv.{status.Level}\n" +
                         $"Cost : {status.Cost:0.0}   Att : {status.Attack:0.0}\n" +
                         $"RNG : {status.Range:0.0}   RT : {status.Reload:0.0}";
    }

    private void OnClick()
    {
        GetSetStatus();
    }
}