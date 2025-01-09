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
    public static CostManager Instance;

    private void Awake()
    {
        Instance = this;
    }

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
    /// ステータスのコストに応じて減算
    /// </summary>
    public void Decrease(WeaponStatus status)
    {
        _currentCost -= status.Cost;
    }

    /// <summary>
    /// コスト減らす
    /// 適当な値で減算
    /// </summary>
    /// <param name="amount"></param>
    public void Decrease(float amount)
    {
        _currentCost -= amount;
    }

    public bool Judge(float value)
    {
        var flag = _currentCost >= value; // コストが足りている＝真
        return flag;
    }

    private void ShowCost()
    {
        _costText.text = _currentCost.ToString("00.00");
    }
}