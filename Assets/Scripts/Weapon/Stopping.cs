using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

/// <summary>
/// 一定範囲内にいる敵全てを一定時間「歩行」させない
/// 停止中に他の敵がエリア内に入ってきても、それらは停止させない
/// todo:一回しか停止できていない
/// </summary>
public class Stopping : MonoBehaviour, IOffense
{
    private Generator _generator = default;
    private WeaponStatus _weaponStatus = default;
    [SerializeField] private List<GameObject> _targets = default;
    private LineRenderer _lineRenderer = default;

    private void Start()
    {
        _generator = FindObjectOfType<Generator>();
        _weaponStatus = GetComponent<WeaponStatus>();
        _targets = new List<GameObject>();
        _lineRenderer = GetComponent<LineRenderer>();
        _lineRenderer.startWidth = 0.1f;
    }

    private void GetTarget()
    {
        foreach (var enemy in _generator.EnemiesList)
        {
            if (enemy == null) continue; // 中身がnullなら飛ばす
            var offset = enemy.transform.position - transform.position;
            var sqrLen = offset.sqrMagnitude;

            if (sqrLen < _weaponStatus.Range * _weaponStatus.Range)
            {
                _targets.Add(enemy);
            } // 範囲内にいたら
        }

        // foreach (var target in _targets)
        // {
        //     // _lineRenderer.SetPosition(0, transform.position);
        //     // _lineRenderer.SetPosition(1, target.transform.position);
        // }
    }

    public void Offense(GameObject target)
    {
        // _wfs = new WaitForSeconds(_weaponStatus.Attack);
        ClearList();
        GetTarget();
        StartCoroutine(StopWalkingCoroutine());
        Debug.Log("歩行を停止させます。");
    }

    private void ClearList()
    {
        _targets.Clear();
    }

    private IEnumerator StopWalkingCoroutine()
    {
        if (!_weaponStatus) _weaponStatus = GetComponent<WeaponStatus>();
        Debug.Log($"Attack : {_weaponStatus.Attack}");
        ChangeEnable(true);
        yield return new WaitForSeconds(_weaponStatus.Attack);
        ChangeEnable(false);
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="flag"> 真：停止、偽：歩行 </param>
    private void ChangeEnable(bool flag)
    {
        foreach (var target in _targets)
        {
            target.GetComponent<NavMeshAgent>().isStopped = flag;
        }
    }

    private void OnDrawGizmos()
    {
        foreach (var target in _targets)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(target.transform.position, 1f);
        }
    }
}