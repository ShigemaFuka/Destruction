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

    public void GenerateOnClick(int indexNum)
    {
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