using UnityEngine;

public class Hp : MonoBehaviour, IDamage
{
    [SerializeField, Header("最大HP")] private float _maxHp = 10;
    [SerializeField, Header("現在のHP")] private float _currentHp = default;
    [SerializeField, Header("タワーか")] private bool _toResult = default;
    private SceneChanger _sceneChanger = default;

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
        _sceneChanger = FindObjectOfType<SceneChanger>();
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
            if (_toResult)
            {
                StartCoroutine(_sceneChanger.LateChange());
            }

            Debug.LogWarning("HPが０になりました。");
            return;
        }

        _currentHp -= value;
    }
}