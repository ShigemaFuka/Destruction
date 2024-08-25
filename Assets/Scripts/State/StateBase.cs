using UnityEngine;

public class StateBase : MonoBehaviour
{
    protected IState _currentState = default;
    private IdleState _idleState = default;

    private void Start()
    {
        _idleState = new IdleState(this);
        ChangeState(_idleState);
        OnStart();
    }

    protected virtual void OnStart()
    {
    }

    private void Update()
    {
        _currentState.Execute();
        OnUpdate();
    }

    protected virtual void OnUpdate()
    {
    }


    public void ChangeState(IState newState)
    {
        if (_currentState != null)
        {
            _currentState.Exit();
        }

        _currentState = newState;
        _currentState.Enter();
    }

    public void SetAnimation(string animationName)
    {
        // アニメーションを設定するロジック
        Debug.Log($"Set Animation: {animationName}");
    }
}