using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 吹き飛ばす
/// todo: なんかおかしい
/// </summary>
public class BlowAway : MonoBehaviour, IOffense
{
    [SerializeField, Header("移動させる距離")] private float pushDistance = 5f;
    [SerializeField, Header("移動速度")] private float pushSpeed = 5f; // 
    private Transform pointA; // A地点からの出発点
    private List<Vector3> initialPosition; // オブジェクトBの初期位置
    private List<Vector3> targetPosition; // オブジェクトBの目標位置
    private List<bool> isPushing;
    private List<GameObject> _targets = default;
    private Generator _generator = default;
    private WeaponStatus _weaponStatus = default;

    private void Start()
    {
        _generator = FindObjectOfType<Generator>();
        _weaponStatus = GetComponent<WeaponStatus>();
        initialPosition = new List<Vector3>();
        targetPosition = new List<Vector3>();
        isPushing = new List<bool>();
        _targets = new List<GameObject>();
        pointA = transform;
    }

    private void Update()
    {
        for (var i = 0; i < _targets.Count; i++)
        {
            // オブジェクトBを目標位置に向かって移動させる
            if (isPushing[i])
            {
                _targets[i].transform.position = Vector3.MoveTowards(_targets[i].transform.position, targetPosition[i],
                    pushSpeed * Time.deltaTime);

                // 目標位置に到達したか確認
                if (Vector3.Distance(_targets[i].transform.position, targetPosition[i]) < 0.001f)
                {
                    isPushing[i] = false;
                    Debug.Log("オブジェクトBの移動が完了しました");
                }
            }
        }
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
        for (var i = 0; i < _targets.Count; i++)
        {
            // オブジェクトBの初期位置を記録
            initialPosition.Add(_targets[i].transform.position);
            // A地点からオブジェクトBへの方向ベクトルを計算
            var direction = (_targets[i].transform.position - pointA.position).normalized;
            // 目標位置を計算
            targetPosition.Add(initialPosition[i] + direction * pushDistance);

            // 移動を開始
            isPushing.Add(true);
        }

        // _target = target;
        // Debug.LogWarning("吹き飛ばす");
    }

    private void ClearList()
    {
        initialPosition.Clear();
        targetPosition.Clear();
        isPushing.Clear();
        _targets.Clear();
    }
}