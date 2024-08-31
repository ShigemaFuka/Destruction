using UnityEngine;

public class WeaponStatus : MonoBehaviour
{
    [SerializeField] private int _level = 1;
    [SerializeField] private float _cost = 1f;
    [SerializeField] private float _attack = 1f;
    [SerializeField] private float _reload = 2f;
    [SerializeField] private float _range = 1f;

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
}