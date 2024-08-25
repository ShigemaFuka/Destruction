using UnityEngine;

/// <summary>
/// 待機状態
/// </summary>
public class IdleState : IState
{
    private readonly StateBase _stateBase = default;

    public IdleState(StateBase stateBase)
    {
        _stateBase = stateBase;
    }


    public void Enter()
    {
        Debug.Log("Enter Idle State");
    }

    public void Execute()
    {
    }

    public void Exit()
    {
        Debug.Log("Exit Idle State");
    }
}