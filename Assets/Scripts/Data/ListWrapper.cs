using System.Collections.Generic;

/// <summary>
/// ListをJSONファイルに保存するための、ラッパークラス
/// </summary>
[System.Serializable]
public class ListWrapper
{
    public List<WeaponStatusData> _weaponStatusDataList = default;
}