using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 武器を生成
/// </summary>
public class WeaponGenerator : MonoBehaviour
{
    [SerializeField] private List<GameObject> _weapons = default;
    private Vector3 _position = default;
    private GameObject _weaponPoint = default;
    private CostManager _costManager = default;
    private List<WeaponStatus> _weaponStatusList = default;


    private void Start()
    {
        _costManager = FindObjectOfType<CostManager>();
        _weaponStatusList = new List<WeaponStatus>();
        foreach (var weapon in _weapons)
        {
            var weaponStatus = weapon.GetComponent<WeaponStatus>();
            _weaponStatusList.Add(weaponStatus);
        }
    }

    public void GenerateOnClick(int indexNum)
    {
        if (!_costManager.Judge(_weaponStatusList[indexNum].Cost))
        {
            Debug.LogWarning("コストが足りません");
            return;
        } // コスト制限
        _costManager.Decrease(_weaponStatusList[indexNum]); // 設置コスト＝レベル１コスト
        var go = Instantiate(_weapons[indexNum]);
        go.transform.position = _position;
        _weaponPoint.SetActive(false);
    }

    /// <summary>
    /// 生成場所を設定
    /// </summary>
    public void SetPosition(Vector3 pos)
    {
        _position = pos;
    }

    /// <summary>
    /// 配置機能の親Objを指定
    /// </summary>
    /// <param name="go"></param>
    public void SetGameObject(GameObject go)
    {
        // 生成後、WeaponPointを非アクティブ化
        _weaponPoint = go;
    }
}