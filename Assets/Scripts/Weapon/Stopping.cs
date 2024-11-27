using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

/// <summary>
/// 一定範囲内にいる敵全てを一定時間「歩行」させない
/// 停止中に他の敵がエリア内に入ってきても、それらは停止させない
/// todo: WeaponStatus Attackがリロード未満 でないとダメ
/// </summary>
public class Stopping : MonoBehaviour, IOffense
{
    [SerializeField] private MaterialChanger _materialChanger = default;
    private Generator _generator = default;
    private WeaponStatus _weaponStatus = default;
    private List<GameObject> _targets = default;
    private List<GameObject> _stoppingTargets = default;
    private bool _isRunning = default;

    private void Start()
    {
        _generator = FindObjectOfType<Generator>();
        _weaponStatus = GetComponent<WeaponStatus>();
        _targets = new List<GameObject>();
        _stoppingTargets = new List<GameObject>();
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
    }

    public void Offense(GameObject target)
    {
        if (!_isRunning)
        {
            ClearList();
            GetTarget();
            StartCoroutine(StopWalkingCoroutine());
            Debug.Log("動きを停止");
        }
    }

    private void ClearList()
    {
        _targets.Clear();
        _stoppingTargets.Clear();
    }

    private IEnumerator StopWalkingCoroutine()
    {
        _isRunning = true;
        if (!_weaponStatus) _weaponStatus = GetComponent<WeaponStatus>();
        Debug.Log($"Attack : {_weaponStatus.Attack}");
        if (_materialChanger) _materialChanger.ToChangedMaterial();
        ChangeEnable();
        yield return new WaitForSeconds(_weaponStatus.Attack);
        ChangeEnable(_stoppingTargets);
        if (_materialChanger) _materialChanger.ToDefaultMaterial();
        _isRunning = false;
    }

    /// <summary>
    /// 止める
    /// </summary>
    private void ChangeEnable()
    {
        foreach (var target in _targets)
        {
            target.GetComponent<NavMeshAgent>().isStopped = true;
            target.GetComponent<Enemy>().enabled = false;
            if (!_stoppingTargets.Contains(target)) _stoppingTargets.Add(target);
            Debug.Log($"{target}を停止します。flag: {target.GetComponent<NavMeshAgent>().isStopped}");
        }
    }

    /// <summary>
    /// 再始動
    /// </summary>
    /// <param name="objects"></param>
    private void ChangeEnable(List<GameObject> objects)
    {
        foreach (var obj in objects)
        {
            obj.GetComponent<NavMeshAgent>().isStopped = false;
            obj.GetComponent<Enemy>().enabled = true;
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