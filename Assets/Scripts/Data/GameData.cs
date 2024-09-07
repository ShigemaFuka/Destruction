using System.Collections.Generic;

/// <summary>
/// JSONに保存するためのclass
/// </summary>
[System.Serializable]
public class GameData
{
    public float _coin = default;
    public List<bool> _stageBoolList = default;
    public ListWrapper _listWrapper = default;

    public GameData(float coin, List<bool> stageList, ListWrapper listWrapper)
    {
        _coin = coin;
        _stageBoolList = stageList;
        _listWrapper = listWrapper;
    }
}