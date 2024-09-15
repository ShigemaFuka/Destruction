using UnityEngine;

/// <summary>
/// どのステージが解放されているかを管理
/// 一定コインを消費してステージを解放する
/// ステージの解放状況をJSONファイルに保存、読み込みをする
/// </summary>
public class StageManager : MonoBehaviour
{
    [SerializeField] private SaveManager _saveManager = default;
    [SerializeField] private GameManager _gameManager = default;
    [SerializeField, Header("ステージUI")] private GameObject[] _stageUis = default;
    [SerializeField, Header("ブロックオブジェクト")] private GameObject[] _blockObjects = default;
    private float _stageCost = default;

    private void Start()
    {
        ChangeEnabled();
    }

    /// <summary>
    /// ステージ解放、コイン消費
    /// </summary>
    /// <param name="num"></param>
    public void UnlockStage(int num)
    {
        var data = _saveManager.LoadGameData();
        if (data._coin < _stageCost)
        {
            Debug.LogWarning("コインが足りません。");
            return;
        }

        if (data._stageBoolList[num])
        {
            return;
        }

        _gameManager.Decrease(_stageCost); // コインを消費する処理
        _gameManager.Unlock(num); // ステージ解放
        ChangeColor(num, Color.white);
        _gameManager.Save();
        _blockObjects[num].SetActive(false); // シーン遷移可
    }

    /// <summary>
    /// ステージ解放に必要なコストを設定する
    /// </summary>
    public void SetStageCost(float amount)
    {
        _stageCost = amount;
    }

    /// <summary>
    /// データのフラグに応じて、シーン遷移機能を有効にする
    /// </summary>
    private void ChangeEnabled()
    {
        var data = _saveManager.LoadGameData();
        for (var i = 0; i < data._stageBoolList.Count; i++)
        {
            if (data._stageBoolList[i] == false)
            {
                ChangeColor(i, Color.gray);
                // _blockObjects[i].GetComponent<Collider>().enabled = false; // シーン遷移不可
                _blockObjects[i].SetActive(true); // シーン遷移不可
            }
            else
            {
                ChangeColor(i, Color.white);
            }
        }
    }

    private void ChangeColor(int index, Color color)
    {
        var material = _stageUis[index].GetComponent<Renderer>().material;
        material.color = color;
    }
}