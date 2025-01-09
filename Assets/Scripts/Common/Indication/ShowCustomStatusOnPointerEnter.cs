using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// 未生成の武器のステータスを表示する　※カスタム武器
/// </summary>
public class ShowCustomStatusOnPointerEnter : MonoBehaviour, IPointerEnter
{
    [SerializeField, Header("Infoテキスト")] private Text _infoText = default;

    [SerializeField, Header("インデックス番号（WeaponGeneratorのリストに依存）")]
    private int _indexNum = default;

    private GameManager _gameManager = default;

    private void Start()
    {
        _infoText.text = "";
        _gameManager = GameManager.Instance;
    }

    private void GetSetInfo()
    {
        // コストだけ初期値のまま
        var initialStatus = _gameManager.InitialStatusList[_indexNum];
        var status = _gameManager.StatusList[_indexNum];
        _infoText.text = $"Cost : {initialStatus._cost:0.0}  Att : {status._attack:0.0}\n" +
                         $"RNG : {status._range:0.0}  RT : {status._reload:0.0}";
    }

    public void PointerEnter()
    {
        GetSetInfo();
    }
}