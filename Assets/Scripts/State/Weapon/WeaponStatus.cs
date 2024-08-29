using UnityEngine;

public class WeaponStatus : MonoBehaviour
{
    [SerializeField] private float _cost = 1f;
    [SerializeField] private float _attack = 1f;
    [SerializeField] private float _reload = 2f;
    [SerializeField] private float _range = 1f;

    public float Cost { get; set; }
    public float Attack { get; set; }
    public float Reload { get; set; }
    public float Range { get; set; }
}