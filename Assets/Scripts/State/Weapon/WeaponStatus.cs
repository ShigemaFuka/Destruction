using UnityEngine;

/// <summary>
/// 実体の武器のステータス
/// </summary>
public class WeaponStatus : MonoBehaviour
{
    #region 変数

    [SerializeField] private int _level = 1;
    [SerializeField] private float _cost = 1f;
    [SerializeField] private float _attack = 1f;
    [SerializeField] private float _reload = 2f;
    [SerializeField] private float _range = 1f;
    [SerializeField] private int _indexNum = default; // Custom用

    #endregion

    #region プロパティ

    public int Level
    {
        get => _level;
        set => _level = value;
    }

    public float Cost
    {
        get => _cost;
        set => _cost = value;
    }

    public float Attack
    {
        get => _attack;
        set => _attack = value;
    }

    public float Reload
    {
        get => _reload;
        set => _reload = value;
    }

    public float Range
    {
        get => _range;
        set => _range = value;
    }

    public int IndexNum
    {
        get => _indexNum;
        //set => _indexNum = value;
    }

    #endregion
}