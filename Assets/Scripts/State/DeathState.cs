using UnityEngine;

/// <summary>
/// 死亡状態
/// </summary>
public class DeathState : IState
{
    private readonly StateBase _stateBase;
    private GameObject _deadPrefab = default;
    private Transform _transform = default; // 生成場所
    private GameObject _deadObj = default; // 生成されたObj
    private GameObject _stpoObj = default;
    private Generator _generator = default;

    public DeathState(StateBase stateBase, GameObject deadPrefab, Transform pos, GameObject stopObj,
        Generator generator)
    {
        _stateBase = stateBase;
        _deadPrefab = deadPrefab;
        _transform = pos;
        _stpoObj = stopObj;
        _generator = generator;
    }

    public void Enter()
    {
        _deadObj = Object.Instantiate(_deadPrefab, _transform.position, _transform.rotation);
        // Debug.Log("Enter Death State");
    }

    public void Execute()
    {
        if (_deadPrefab)
        {
            Exit();
        }
    }

    public void Exit()
    {
        // todo: 仕舞う
        // _stpoObj.SetActive(false);
        _generator.RemoveObj(_stateBase.gameObject);
        Object.Destroy(_stateBase.gameObject);
        // Debug.Log("Exit Death State");
    }
}