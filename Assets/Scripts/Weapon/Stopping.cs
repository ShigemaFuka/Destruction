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
    // [SerializeField, Header("範囲球")] private GameObject _area = default;
    private Generator _generator = default;
    private WeaponStatus _weaponStatus = default;
    private List<GameObject> _targets = default;

    private List<GameObject> _stoppingTargets = default;

    // [SerializeField] private Material _defaultMaterial = default;
    // [SerializeField] private Material _changedMaterial = default;
    // private MeshRenderer _meshRenderer = default;
    // private static readonly int Color1 = Shader.PropertyToID("_Color");
    [SerializeField] private MaterialChanger _materialChanger = default;

    private void Start()
    {
        _generator = FindObjectOfType<Generator>();
        _weaponStatus = GetComponent<WeaponStatus>();
        _targets = new List<GameObject>();
        _stoppingTargets = new List<GameObject>();
        // _meshRenderer = _area.GetComponent<MeshRenderer>();
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
        ClearList();
        GetTarget();
        StartCoroutine(StopWalkingCoroutine());
        Debug.Log("歩行を停止させます。");
    }

    private void ClearList()
    {
        _targets.Clear();
        _stoppingTargets.Clear();
    }

    private IEnumerator StopWalkingCoroutine()
    {
        if (!_weaponStatus) _weaponStatus = GetComponent<WeaponStatus>();
        Debug.Log($"Attack : {_weaponStatus.Attack}");
        if (_materialChanger) _materialChanger.ToChangedMaterial();
        ChangeEnable();
        yield return new WaitForSeconds(_weaponStatus.Attack);
        ChangeEnable(_stoppingTargets);
        if (_materialChanger) _materialChanger.ToDefaultMaterial();
    }

    private void ChangeEnable()
    {
        foreach (var target in _targets)
        {
            target.GetComponent<NavMeshAgent>().isStopped = true;
            if (!_stoppingTargets.Contains(target)) _stoppingTargets.Add(target);
            Debug.Log($"{target}を停止します。flag: {target.GetComponent<NavMeshAgent>().isStopped}");
        }
    }

    private void ChangeEnable(List<GameObject> objects)
    {
        foreach (var obj in objects)
        {
            obj.GetComponent<NavMeshAgent>().isStopped = false;
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