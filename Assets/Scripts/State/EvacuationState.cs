using System.Collections;
using UnityEngine;
using UnityEngine.AI;

/// <summary>
/// 退避状態
/// 回復エリアに向かい、一定時間留まる
/// </summary>
public class EvacuationState : IState
{
    #region 変数

    private StateBase _stateBase = default;
    private Enemy _enemy = default;
    private Vector3 _healPosition = default; // 回復エリアの場所
    private float _distance = 20f; // 到達したとみなす距離
    private NavMeshAgent _agent = default;
    private Transform _transform = default;
    private float _waitTime = default; // 留まる時間
    private float _speedUp = 2f; // 速度上昇
    private float _defaultSpeed = default;
    private bool _isCoroutineStarted = false; // コルーチンが開始されたかどうか
    private MonoBehaviour _monoBehaviour = default;
    private WalkState _walkState = default;
    private IHeal _heal = default;

    #endregion

    public EvacuationState(StateBase owner,
        float waitTime, WalkState walkState)
    {
        _healPosition = GameObject.Find("HealPoint").transform.position;
        _transform = owner.transform;
        _waitTime = waitTime;
        _walkState = walkState;
        _stateBase = owner.GetComponent<StateBase>();
        _heal = owner.GetComponent<IHeal>();
        _monoBehaviour = owner.GetComponent<MonoBehaviour>();
        _enemy = owner.GetComponent<Enemy>();
        _agent = owner.GetComponent<NavMeshAgent>();
        _defaultSpeed = _agent.speed;
    }

    public void Enter()
    {
        _agent.speed = _defaultSpeed * _speedUp;
        _agent.SetDestination(_healPosition);
        // Debug.Log("Enter Evacuation State");
    }

    public void Execute()
    {
        Destination();
        Rotation();
        // Debug.Log("Execute Evacuation State");
    }

    public void Exit()
    {
        _agent.speed = _defaultSpeed;
        // Debug.Log("Exit Evacuation State");
    }

    /// <summary>
    /// 目的地へ向かう
    /// </summary>
    private void Destination()
    {
        if (float.IsInfinity(_agent.remainingDistance))
        {
            Debug.LogWarning("残った距離が不明です。");
            return;
        }

        // だいたい近づいたら到達と見做す
        var distance = (_transform.position - _healPosition).sqrMagnitude;
        if (distance <= _distance)
        {
            if (!_isCoroutineStarted)
            {
                _monoBehaviour.StartCoroutine(Wait());
            }
        }
    }

    /// <summary>
    /// 進行方向への回転
    /// </summary>
    private void Rotation()
    {
        var nextCorner = _transform.position;
        if (_agent.path != null && _agent.path.corners.Length > 1)
        {
            nextCorner = _agent.path.corners[1];
            Debug.DrawLine(_transform.position, nextCorner, Color.yellow);
        }

        var to = nextCorner - _transform.position;
        var angle = Vector3.SignedAngle(_transform.forward, to, Vector3.up);
        // 角度が30゜を越えていたら
        if (Mathf.Abs(angle) > 30)
        {
            var rotMax = _agent.angularSpeed * Time.deltaTime;
            var rot = Mathf.Min(Mathf.Abs(angle), rotMax);
            _transform.Rotate(0f, rot * Mathf.Sign(angle), 0f);
        }
    }

    private IEnumerator Wait()
    {
        _isCoroutineStarted = true;
        yield return new WaitForSeconds(0.7f);
        _heal.Heal(2); // 2回に分けて回復 1回目
        yield return new WaitForSeconds(0.7f);
        _heal.Heal(2); // 2回に分けて回復 2回目
        yield return new WaitForSeconds(_waitTime);
        _stateBase.ChangeState(_walkState);
        _enemy.FalseFlag();
    }
}