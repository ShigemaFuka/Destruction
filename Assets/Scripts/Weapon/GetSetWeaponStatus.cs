using UnityEngine;

/// <summary>
/// 保存データからステータスを読み込んで、武器に適用する。
/// </summary>
public class GetSetWeaponStatus : MonoBehaviour
{
    private WeaponStatus _weaponStatus = default;
    private GameManager _gameManager = default;

    private void Start()
    {
        _weaponStatus = GetComponent<WeaponStatus>();
        _gameManager = FindObjectOfType<GameManager>();
        Invoke(nameof(GetSetStatus), 0.1f); //GetSetStatus();
    }

    /// <summary>
    /// Updateシーンで
    /// キャンバス表示・強化ボタン押下　のときに更新
    /// </summary>
    public void GetSetStatus()
    {
        var status = _gameManager.StatusList[_weaponStatus.IndexNum];
        _weaponStatus.Level = status._level;
        _weaponStatus.Cost = status._cost;
        _weaponStatus.Attack = status._attack;
        _weaponStatus.Reload = status._reload;
        _weaponStatus.Range = status._range;
    }
}