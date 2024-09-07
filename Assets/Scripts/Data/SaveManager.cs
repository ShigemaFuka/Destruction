using System.IO;
using UnityEngine;
using System.Collections.Generic;

public class SaveManager : MonoBehaviour
{
    private string _filePath = default;

    private void Awake()
    {
        // 保存するファイルのパス
        _filePath = Application.persistentDataPath + "/gameData.json";
    }

    /// <summary>
    /// 保存
    /// </summary>
    /// <param name="coin"></param>
    /// <param name="stageList"></param>
    /// <param name="dataList"></param>
    public void SaveGameData(float coin, List<bool> stageList, List<WeaponStatusData> dataList)
    {
        var wrapper = new ListWrapper { _weaponStatusDataList = dataList };
        var data = new GameData(coin, stageList, wrapper);
        var json = JsonUtility.ToJson(data, true); // GameDataオブジェクトをJSONに変換
        File.WriteAllText(_filePath, json); // JSONをファイルに書き込む
        Debug.Log("ゲームデータが保存されました: " + _filePath);
    }

    /// <summary>
    /// 読み込む
    /// </summary>
    /// <returns></returns>
    public GameData LoadGameData()
    {
        if (File.Exists(_filePath))
        {
            var json = File.ReadAllText(_filePath); // ファイルからJSONを読み込む
            var data = JsonUtility.FromJson<GameData>(json); // JSONをGameDataオブジェクトに変換
            Debug.Log("ゲームデータが読み込まれました。");
            return data;
        }

        Debug.LogWarning("保存されたゲームデータが見つかりません。");
        return null;
    }

    /// <summary>
    /// データを削除する
    /// </summary>
    public void DeleteData()
    {
        if (File.Exists(_filePath))
        {
            File.Delete(_filePath); // JSON ファイルを削除
            Debug.Log("JSON ファイルを削除");
        }
    }
}