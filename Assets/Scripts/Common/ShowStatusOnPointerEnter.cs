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

    private void Start()
    {
        _infoText.text = "";
        _weaponGenerator = FindObjectOfType<WeaponGenerator>();
    }

    private void GetSetInfo()
    {
        var status = _weaponGenerator.WeaponStatusList[_indexNum];
        _infoText.text = $"Cost : {status.Cost:0.0}  Att : {status.Attack:0.0}\n" +
                         $"RNG : {status.Range:0.0}  RT : {status.Reload:0.0}";
    }

    public void PointerEnter()
    {
        GetSetInfo();
    }
}