using UnityEngine;

public class Hp : MonoBehaviour, IDamage, IHeal
{
    [SerializeField, Header("最大HP")] private float _maxHp = 10;
    [SerializeField, Header("現在のHP")] private float _currentHp;
    [SerializeField, Header("タワーか")] private bool _toResult;
    private SceneChanger _sceneChanger;
    private GameManager _gameManager;

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
        _gameManager = GameManager.Instance;
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
        _currentHp += _maxHp * 0.5f;
        if (_currentHp > _maxHp) _currentHp = _maxHp; // 超えないようにする
        Debug.Log($"回復 {_maxHp * 0.5f}");
    }

    /// <summary>
    /// 回復量は引数なしと同じ
    /// </summary>
    /// <param name="per"> 何回に分けて回復させるか </param>
    public void Heal(int per)
    {
        _currentHp += _maxHp * 0.5f / per;
        if (_currentHp > _maxHp) _currentHp = _maxHp; // 超えないようにする
        Debug.Log($"回復 {_maxHp * 0.5f / per}");
    }
}