using UnityEngine;

/// <summary>
/// コスト管理、増減
/// </summary>
public class CostManager : MonoBehaviour
{
    [SerializeField] private float _currentCost = default;

    /// <summary>
    /// コスト減らす
    /// </summary>
    public void Decrease(float value)
    {
        // if (_currentCost < value)
        // {
        //     Debug.LogWarning("コストが足りません");
        //     return;
        // }

        _currentCost -= value;
    }

    public bool Judge(float value)
    {
        var flag = _currentCost > value; // コストが足りている＝真
        return flag;
    }
}