using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// コスト管理、増減
/// </summary>
public class CostManager : MonoBehaviour
{
    [SerializeField] private float _currentCost = default;
    [SerializeField] private Text _costText = default;
    private float _previousCost = default; // 変化前のコスト

    private void Update()
    {
        if (_currentCost != _previousCost)
        {
            ShowCost();
            _previousCost = _currentCost; // 前回のスコアを更新
        } // コストに変化があれば
    }

    /// <summary>
    /// コスト減らす
    /// </summary>
    public void Decrease(WeaponStatus status)
    {
        _currentCost -= status.Cost;
    }

    public bool Judge(float value)
    {
        var flag = _currentCost > value; // コストが足りている＝真
        return flag;
    }

    private void ShowCost()
    {
        _costText.text = _currentCost.ToString("00.00");
    }
}