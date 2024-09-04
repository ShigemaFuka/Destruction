using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 吹き飛ばす
/// </summary>
public class BlowAway : MonoBehaviour, IOffense
{
    [SerializeField, Header("移動させる距離（攻撃値に比例）")]
    private float _pushDistance = 5f;

    [SerializeField, Header("移動速度")] private float _pushSpeed = 50f;
    private Transform _pointA = default; // A地点からの出発点
    private List<Vector3> _initialPositions = default; // 初期位置
    private List<Vector3> _targetPositions = default; // 目標位置
    private List<bool> _isPushings = default;
    private List<GameObject> _targets = default;
    private Generator _generator = default;
    private WeaponStatus _weaponStatus = default;

    private void Start()
    {
        _generator = FindObjectOfType<Generator>();
        _weaponStatus = GetComponent<WeaponStatus>();
        _initialPositions = new List<Vector3>();
        _targetPositions = new List<Vector3>();
        _isPushings = new List<bool>();
        _targets = new List<GameObject>();
        _pointA = transform;
    }

    private void Update()
    {
        for (var i = 0; i < _targets.Count; i++)
        {
            // オブジェクトBを目標位置に向かって移動させる
            if (_isPushings[i])
            {
                _targets[i].transform.position = Vector3.MoveTowards(_targets[i].transform.position,
                    _targetPositions[i],
                    _pushSpeed * Time.deltaTime);

                // 目標位置に到達したか確認
                if (Vector3.Distance(_targets[i].transform.position, _targetPositions[i]) < 0.001f)
                {
                    _isPushings[i] = false;
                    // Debug.Log("移動が完了しました");
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
            // 対象の初期位置を記録
            _initialPositions.Add(_targets[i].transform.position);
            // A地点から対象への方向ベクトルを計算
            var direction = (_targets[i].transform.position - _pointA.position).normalized;
            // 目標位置を計算 攻撃値に応じて吹き飛ばす距離が変動
            _targetPositions.Add(_initialPositions[i] + direction * (_pushDistance * _weaponStatus.Attack));
            _isPushings.Add(true); // 移動を開始
        }
    }

    private void ClearList()
    {
        _initialPositions.Clear();
        _targetPositions.Clear();
        _isPushings.Clear();
        _targets.Clear();
    }
}