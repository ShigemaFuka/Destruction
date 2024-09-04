using UnityEngine;

public class Hp : MonoBehaviour, IDamage
{
    [SerializeField, Header("最大HP")] private float _maxHp = 10;
    [SerializeField, Header("現在のHP")] private float _currentHp = default;

    // public float CurrentHp => _currentHp;
    public float CurrentHp
    {
        get => _currentHp;
        //set => _currentHp = value;
    }
    public float MaxHp
    {
        get => _maxHp;
        //set => _currentHp = value;
    }
    
    private void Start()
    {
        _currentHp = _maxHp;
    }

    /// <summary>
    /// 呼ばれたら自身のHPを減らす
    /// </summary>
    /// <param name="value"></param>
    public void Damage(float value)
    {
        if (_currentHp <= 0)
        {
            // todo: 何らかの処理
            Debug.LogWarning("HPが０になりました。");
            return;
        }

        _currentHp -= value;
    }
}