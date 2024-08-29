using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 武器を生成
/// </summary>
public class WeaponGenerator : MonoBehaviour
{
    [SerializeField] private List<GameObject> _weapons = default;
    private int _indexNum = default;
    private Vector3 _position = default;

    public void GenerateOnClick(int indexNum)
    {
        var go = Instantiate(_weapons[indexNum]);
        go.transform.position = _position;
    }

    /// <summary>
    /// 生成場所を設定
    /// </summary>
    public void SetPosition(Vector3 pos)
    {
        _position = pos;
    }
    // todo: 生成後、WeaponPointを消す
    // todo: 強化機能を使えるようにする
}