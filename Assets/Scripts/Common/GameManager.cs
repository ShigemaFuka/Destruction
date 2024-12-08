using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    #region 変数

    [SerializeField, Header("総合コイン数")] private float _coin;
    [SerializeField, Header("初期化")] private float _initialCoin = 100f;
    [SerializeField] private List<bool> _stageList;
    [SerializeField] private List<WeaponStatusData> _statusList;
    [SerializeField, Header("ステータス初期化用")] private List<WeaponStatusData> _initialStatusList;
    public static GameManager Instance;
    private static float _temporaryCustodyCoin; // 獲得したコイン　※戦果
    private SaveManager _saveManager;
    private bool _canChangeReward; // 報酬減算計算ができるか
    private static string _restartSceneName; // もう一度プレイするシーン名

    #endregion

    #region プロパティ

    /// <summary>
    /// 所持しているコイン
    /// </summary>
    public float Coin
    {
        get => _coin;
        // set => _coin = value;
    }

    /// <summary>
    /// 獲得したコイン　※戦果
    /// </summary>
    public float TemporaryCustodyCoin
    {
        get => _temporaryCustodyCoin;
        set => _temporaryCustodyCoin = value;
    }

    /// <summary> 保存されるステータス </summary>
    public List<WeaponStatusData> StatusList
    {
        get => _statusList;
        //set => _statusList = value;
    }

    /// <summary> 初期化用ステータス </summary>
    public List<WeaponStatusData> InitialStatusList
    {
        get => _initialStatusList;
        //set => _initialStatusList = value;
    }

    #endregion

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        _saveManager = SaveManager.Instance;
        if (_saveManager == null) Debug.LogWarning("SaveManagerがありません。");
        InitializedData();
        _canChangeReward = true;
    }

    /// <summary>
    /// データをロードして、
    /// データがなければ、初期化用の数値を設定してセーブする。
    /// データがあれば、保存された物を変数に格納する。
    /// </summary>
    private void InitializedData()
    {
        var loadedData = _saveManager.LoadGameData(); // 保存されたデータを読み込む
        if (loadedData != null)
        {
            _coin = loadedData._coin;
            _stageList = loadedData._stageBoolList;
            _statusList = loadedData._listWrapper._weaponStatusDataList;
        }
        else
        {
            // 初期値を設定する場合
            _coin = _initialCoin;
            _stageList = new List<bool> { true, true, false, false, false, false };
            _statusList = _initialStatusList;
            _saveManager.SaveGameData(_coin, _stageList, _statusList);
        }
    }

    /// <summary>
    /// コイン減らす
    /// ステータスのコスト分減らす
    /// </summary>
    public void Decrease(WeaponStatusData status, int indexNum)
    {
        _statusList[indexNum] = status;
        _coin -= status._cost;
        _saveManager.SaveGameData(_coin, _stageList, _statusList);
    }

    /// <summary>
    /// コイン減らす
    /// </summary>
    public void Decrease(float amount)
    {
        _coin -= amount;
    }

    public void Save()
    {
        _saveManager.SaveGameData(_coin, _stageList, _statusList);
    }

    /// <summary>
    /// ステージ解放
    /// </summary>
    /// <param name="num"></param>
    public void Unlock(int num)
    {
        _stageList[num] = true;
    }

    /// <summary>
    /// 一時保管していた戦果を所持コインと合算する
    /// </summary>
    public void AddTmpCoin()
    {
        _coin += _temporaryCustodyCoin;
        _saveManager.SaveGameData(_coin, _stageList, _statusList);
    }

    public void ResetTmpCoin()
    {
        _temporaryCustodyCoin = 0;
    }

    /// <summary>
    /// タワーのHPから計算（戦果 ＝ コイン数 ＊ 残りHPの割合）
    /// </summary>
    /// <param name="currentHp"></param>
    /// <param name="maxHp"></param>
    public void ChangeReward(float currentHp, float maxHp)
    {
        if (!_canChangeReward) return; // 減算済みならリターン
        _temporaryCustodyCoin *= (currentHp / maxHp);
        _canChangeReward = false;
    }

    /// <summary>
    /// データをリセット
    /// 初期かデータを保存
    /// </summary>
    public void ResetData()
    {
        _saveManager.DeleteData();
        InitializedData();
    }

    /// <summary>
    /// もう一度プレイするシーン名を設定する
    /// </summary>
    public void SetRestartSceneName(string sceneName)
    {
        _restartSceneName = sceneName;
        Debug.Log($"シーン名：{sceneName}がセットされました。");
    }

    /// <summary>
    /// もう一度プレイするシーン名を返す
    /// </summary>
    /// <returns></returns>
    public string GetRestartSceneName()
    {
        return _restartSceneName;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.J))
        {
            ResetData();
        }

        if (Input.GetKeyDown(KeyCode.K))
        {
            _saveManager.SaveGameData(_coin, _stageList, _statusList);
        }
    }
}