using UnityEngine;
using UnityEngine.AI;

/// <summary>
/// 歩行状態
/// </summary>
public class WalkState : IState
{
    private readonly StateBase _stateBase = default;
    private float _distance = 1f; // 到達したとみなす距離
    private Vector3[] _positions = default; // 経路の位置情報
    private int _indexNum = default; // めざす場所のインデックス番号
    private NavMeshAgent _agent = default;
    private Transform _transform = default;
    private ChangeOfCourseState _changeOfCourseState = default;

    public WalkState(StateBase stateBase, Transform t, NavMeshAgent nma, Vector3[] vecs,
        ChangeOfCourseState changeOfCourseState)
    {
        _stateBase = stateBase;
        _transform = t;
        _agent = nma;
        _positions = vecs;
        _changeOfCourseState = changeOfCourseState;
    }

    public void Enter()
    {
        // Debug.Log("Enter Walk State");
        // enemy.SetAnimation("Walk");
    }

    public void Execute()
    {
        Destination();
        Rotation();
    }

    public void Exit()
    {
        // Debug.Log("Exit Walk State");
    }

    /// <summary>
    /// 目的地
    /// </summary>
    public void Destination()
    {
        if (_indexNum == _positions.Length)
        {
            _stateBase.ChangeState(_changeOfCourseState);
            // Debug.Log("攻撃に移行");
            return;
        }

        _agent.SetDestination(_positions[_indexNum]);
        var distance = (_transform.position - _positions[_indexNum]).sqrMagnitude;
        // だいたい近づいたら到達と見做す
        if (distance <= _distance) _indexNum++; // 次の目標地点を更新
    }

    /// <summary>
    /// 進行方向への回転
    /// </summary>
    public void Rotation()
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
}