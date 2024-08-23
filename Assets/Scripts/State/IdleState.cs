using UnityEngine;

/// <summary>
/// 待機状態
/// </summary>
public class IdleState : IState
{
    private readonly StateBase stateBase;
    private Transform _transform = default;
    private Transform _target = default;
    private AttackState _attackState = default;

    public IdleState(StateBase stateBase, Transform transform, Transform target, AttackState attackState)
    {
        this.stateBase = stateBase;
        _transform = transform;
        _target = target;
        _attackState = attackState;
    }

    public void Enter()
    {
        Debug.Log("Enter Idle State");
        // // アニメーションの変更など
        // enemy.SetAnimation("Idle");
    }

    public void Execute()
    {
        Rotation();
    }

    public void Exit()
    {
        Debug.Log("Exit Idle State");
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
            var rotMax = 30 * Time.deltaTime;
            var rot = Mathf.Min(Mathf.Abs(angle), rotMax);
            _transform.Rotate(0f, rot * Mathf.Sign(angle), 0f);
        }
        else
        {
            stateBase.ChangeState(_attackState);
        }
    }
}