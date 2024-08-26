using UnityEngine;

/// <summary>
/// 方向転換状態
/// </summary>
public class ChangeOfCourseState : IState
{
    private readonly StateBase _stateBase = default;
    private Transform _transform = default;
    private Transform _target = default;
    private AttackState _attackState = default;

    public ChangeOfCourseState(StateBase stateBase, Transform transform, Transform target, AttackState attackState)
    {
        this._stateBase = stateBase;
        _transform = transform;
        _target = target;
        _attackState = attackState;
    }

    public void Enter()
    {
        // Debug.Log("Enter ChangeOfCourse State");
        // // アニメーションの変更など
        // enemy.SetAnimation("");
    }

    public void Execute()
    {
        Rotation();
    }

    public void Exit()
    {
        // Debug.Log("Exit ChangeOfCourse State");
    }

    /// <summary>
    /// 攻撃方向への回転
    /// </summary>
    public void Rotation()
    {
        var nextCorner = _target.position;
        var to = nextCorner - _transform.position;
        var angle = Vector3.SignedAngle(_transform.forward, to, Vector3.up);
        // 角度が10゜を越えていたら
        if (Mathf.Abs(angle) > 10)
        {
            var rotMax = 70 * Time.deltaTime;
            var rot = Mathf.Min(Mathf.Abs(angle), rotMax);
            _transform.Rotate(0f, rot * Mathf.Sign(angle), 0f);
        }
        else
        {
            _stateBase.ChangeState(_attackState);
        }
    }
}