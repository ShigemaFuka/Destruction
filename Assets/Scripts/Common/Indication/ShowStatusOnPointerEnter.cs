using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// 未生成の武器のステータスを表示する
/// </summary>
public class ShowStatusOnPointerEnter : MonoBehaviour, IPointerEnter
{
    [SerializeField, Header("Infoテキスト")] private Text _infoText = default;

    [SerializeField, Header("インデックス番号（WeaponGeneratorのリストに依存）")]
    private int _indexNum = default;

    private WeaponGenerator _weaponGenerator = default;
    private GameManager _gameManager = default;

    private void Start()
    {
        _infoText.text = "";
        _weaponGenerator = FindObjectOfType<WeaponGenerator>();
        _gameManager = GameManager.Instance;
    }

    private void GetSetInfo()
    {
        if (_weaponGenerator.WeaponStatusList[_indexNum].name.Contains("Custom"))
        {
            var status = _weaponGenerator.WeaponStatusList[_indexNum];
            _infoText.text = $"Cost : {status.Cost:0.0}  Att : {status.Attack:0.0}\n" +
                             $"RNG : {status.Range:0.0}  RT : {status.Reload:0.0}";
        }
        else
        {
            var status = _gameManager.InitialStatusList[_indexNum];
            _infoText.text = $"Cost : {status._cost:0.0}  Att : {status._attack:0.0}\n" +
                             $"RNG : {status._range:0.0}  RT : {status._reload:0.0}";
        }

        // todo:CustomWeaponかDefaultWeaponかによって、表示するステータスを変える必要がある
    }

    public void PointerEnter()
    {
        GetSetInfo();
    }
}