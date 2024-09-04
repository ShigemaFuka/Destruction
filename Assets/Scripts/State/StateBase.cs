using UnityEngine;

public class StateBase : MonoBehaviour
{
    protected IState _currentState = default;
    private IdleState _idleState = default;

    [SerializeField, Tooltip("現在の状態名")]
    private string _currentStateName = "";
    
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
        if (_currentState != null)
        {
            _currentStateName = _currentState.GetType().Name;
        }
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