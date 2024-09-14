using UnityEngine;

/// <summary>
/// 死亡状態
/// 地面に落ちているように見せかけるように、Y軸Posを変更
/// </summary>
public class DeathState : IState
{
    private readonly StateBase _stateBase;
    private Generator _generator = default;
    private Animator _animator = default;
    private GameObject _model = default; // キャラクターやモンスター

    public DeathState(StateBase stateBase, Generator generator, Animator animator, GameObject model)
    {
        _stateBase = stateBase;
        _generator = generator;
        _animator = animator;
        _model = model;
    }

    public void Enter()
    {
        if (_animator) _animator.Play("Die");
        var renderers = _stateBase.gameObject.GetComponentsInChildren<Renderer>();
        foreach (var renderer in renderers)
        {
            renderer.material.color = Color.gray;
        }
        // Debug.Log("Enter Death State");
    }

    public void Execute()
    {
    }

    public void Exit()
    {
        if (_model)
        {
            var pos = _model.transform.position;
            _model.transform.position = new Vector3(pos.x, -0.5f, pos.z); // 地面に落ちる感じ
        }

        _generator.RemoveObj(_stateBase.gameObject);
        // Object.Destroy(_stateBase.gameObject, 3f);
        // Debug.Log("Exit Death State");
    }
}