using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 残りHPに応じて城のオブジェクトを切り替える 
/// </summary>
public class ChangeCastle : MonoBehaviour
{
    [Header("0: maxHp   1: half   2: zero")] [SerializeField, Header("城のObj")]
    private List<GameObject> _castles = default;

    [SerializeField, Header("HPを有する城")] private GameObject _castle = default;
    private Hp _hp = default;
    private float _half = default;

    private void Start()
    {
        _hp = _castle.GetComponent<Hp>();
        _half = _hp.MaxHp / 2;
    }

    private void Update()
    {
        if (_hp.CurrentHp > _half)
        {
            ChangeActive(_castles[0]);
        } // half より大
        else if (_hp.CurrentHp <= 0)
        {
            ChangeActive(_castles[2]);
        } // death
        else
        {
            ChangeActive(_castles[1]);
        } // 0 より大 half 以下
    }

    /// <summary>
    /// 引数以外のObjを非アクティブ化
    /// </summary>
    /// <param name="go"></param>
    private void ChangeActive(GameObject go)
    {
        foreach (var castle in _castles)
        {
            if (castle.gameObject == go.gameObject)
            {
                castle.SetActive(true);
                continue;
            }

            castle.SetActive(false);
        }
    }
}