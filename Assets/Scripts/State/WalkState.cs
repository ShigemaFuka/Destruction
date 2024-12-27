using UnityEngine;
using UnityEngine.AI;

/// <summary>
/// 歩行状態
/// </summary>
public class WalkState : IState
{
    #region 変数

    private readonly StateBase _stateBase;
    private float _distance = 1f; // 到達したとみなす距離
    private Vector3[] _positions; // 経路の位置情報
    private int _indexNum; // めざす場所のインデックス番号
    private NavMeshAgent _agent;
    private Transform _transform;
    private ChangeOfCourseState _changeOfCourseState;
    private Animator _animator;
    private int walk = Animator.StringToHash("Walk");
    private GameObject _parentRoute;

    #endregion

    public WalkState(StateBase owner,
        ChangeOfCourseState changeOfCourseState)
    {
        _stateBase = owner;
        _parentRoute = GameObject.Find(owner.GetComponent<Enemy>().ParentRouteName);
        _transform = owner.transform;
        _agent = owner.GetComponent<NavMeshAgent>();
        _changeOfCourseState = changeOfCourseState;
        _animator = owner.GetComponent<Animator>();
        GetRoute();
    }

    public void Enter()
    {
        // Debug.Log("Enter Walk State");
        if (_animator) _animator.SetBool(walk, true);
    }

    public void Execute()
    {
        Destination();
        Rotation();
    }

    public void Exit()
    {
        // Debug.Log("Exit Walk State");
        if (_animator) _animator.SetBool(walk, false);
    }

    /// <summary>
    /// 経路を取得
    /// </summary>
    private void GetRoute()
    {
        var childCount = _parentRoute.transform.childCount;
        _positions = new Vector3[childCount];
        for (var i = 0; i < _positions.Length; i++)
        {
            _positions[i] = _parentRoute.transform.GetChild(i).transform.position;
        }
    }

    /// <summary>
    /// 目的地
    /// </summary>
    private void Destination()
    {
        if (_indexNum == _positions.Length)
        {
            var d = (_transform.position - _positions[_indexNum - 1]).sqrMagnitude;
            if (d <= _distance) _stateBase.ChangeState(_changeOfCourseState);
            // Debug.Log("攻撃に移行");
            _agent.SetDestination(_positions[_indexNum - 1]);
        }
        else
        {
            _agent.SetDestination(_positions[_indexNum]);
            var distance = (_transform.position - _positions[_indexNum]).sqrMagnitude;
            // だいたい近づいたら到達と見做す
            if (distance <= _distance) _indexNum++; // 次の目標地点を更新
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
}