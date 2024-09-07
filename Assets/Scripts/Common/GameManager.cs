using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private float _coin = 100f;
    [SerializeField] private List<bool> _stageList = default;
    [SerializeField] private List<WeaponStatusData> _statusList = default;
    [SerializeField, Header("ステータス初期化用")] private List<WeaponStatusData> _initialStatusList = default;
    private SaveManager _saveManager = default;

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
            _coin = 100f;
            _stageList = new List<bool> { false, false };
            _statusList = _initialStatusList;
            _saveManager.SaveGameData(_coin, _stageList, _statusList);
        }
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

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.D))
        {
            _saveManager.DeleteData();
        }

        if (Input.GetKeyDown(KeyCode.S))
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
        if (Input.GetKeyDown(KeyCode.A))
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