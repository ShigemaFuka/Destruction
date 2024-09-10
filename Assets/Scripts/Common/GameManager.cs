using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField, Header("総合コイン数")] private float _coin = default;
    [SerializeField, Header("初期化")] private float _initialCoin = 100f;
    [SerializeField] private List<bool> _stageList = default;
    [SerializeField] private List<WeaponStatusData> _statusList = default;
    [SerializeField, Header("ステータス初期化用")] private List<WeaponStatusData> _initialStatusList = default;
    private static float _temporaryCustodyCoin = default; // 獲得したコイン　※戦果
    private SaveManager _saveManager = default;
    private bool _canChangeReward = default; // 報酬減算計算ができるか

    /// <summary>
    /// 獲得したコイン　※戦果
    /// </summary>
    public float TemporaryCustodyCoin
    {
        get => _temporaryCustodyCoin;
        set => _temporaryCustodyCoin = value;
    }

    public List<WeaponStatusData> StatusList
    {
        get => _statusList;
        //set => _statusList = value;
    }

    private void Start()
    {
        _saveManager = FindObjectOfType<SaveManager>();
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
            _stageList = new List<bool> { false, false };
            _statusList = _initialStatusList;
            _saveManager.SaveGameData(_coin, _stageList, _statusList);
        }

        _canChangeReward = true;
    }

    /// <summary>
    /// コイン減らす
    /// </summary>
    public void Decrease(WeaponStatusData status, int indexNum)
    {
        _statusList[indexNum] = status;
        _coin -= status._cost;
        _saveManager.SaveGameData(_coin, _stageList, _statusList);
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
        _temporaryCustodyCoin *= currentHp / maxHp;
        _canChangeReward = false;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.J))
        {
            _saveManager.DeleteData();
        }

        if (Input.GetKeyDown(KeyCode.K))
        {
            _saveManager.SaveGameData(_coin, _stageList, _statusList);
        }


        // 'C'キーを押したらリストにランダムな値を追加
        if (Input.GetKeyDown(KeyCode.C))
        {
            var newBool = (Random.Range(0, 2) == 0); // trueかfalseをランダムに追加
            _stageList.Add(newBool);
            Debug.Log("リストに値を追加しました: " + newBool);
            for (var i = 0; i < _stageList.Count; i++)
            {
                var stage = _stageList[i];
                Debug.Log($"[{i}] {stage}");
            }
        }

        // 'A'キーを押したらコインの値を増やす
        if (Input.GetKeyDown(KeyCode.L))
        {
            _coin += 10f;
            Debug.Log("コインが増えました: " + _coin);
            for (var i = 0; i < _statusList.Count; i++)
            {
                var status = _statusList[i];
                Debug.Log($"[{i}] lv.{status._level}\n" +
                          $"att: {status._attack}  cost: {status._cost}\n" +
                          $"ran: {status._range}  reload: {status._reload}");
            }
        }
    }
}