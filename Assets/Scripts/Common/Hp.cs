using UnityEngine;

public class Hp : MonoBehaviour, IDamage, IHeal
{
    [SerializeField, Header("最大HP")] private float _maxHp = 10;
    [SerializeField, Header("現在のHP")] private float _currentHp = default;
    [SerializeField, Header("タワーか")] private bool _toResult = default;
    private SceneChanger _sceneChanger = default;
    private GameManager _gameManager = default;


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
        _gameManager = FindObjectOfType<GameManager>();
    }

    /// <summary>
    /// 呼ばれたら自身のHPを減らす
    /// </summary>
    /// <param name="value"></param>
    public void Damage(float value)
    {
        if (_currentHp <= 0)
        {
            if (_toResult)
            {
                StartCoroutine(_sceneChanger.LateChange());
                _gameManager.ChangeReward(0, _maxHp); // 負け
                _toResult = false;
            }

            Debug.LogWarning("HPが０になりました。");
            return;
        }

        _currentHp -= value;
    }

    /// <summary>
    /// 最大HPの半分を回復
    /// </summary>
    public void Heal()
    {
        _currentHp += _maxHp / 2;
    }
}