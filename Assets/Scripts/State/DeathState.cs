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
    private IDeath[] _death = default;

    public DeathState(StateBase stateBase, Generator generator, Animator animator, GameObject model, IDeath[] death)
    {
        _stateBase = stateBase;
        _generator = generator;
        _animator = animator;
        _model = model;
        _death = death;
    }

    public void Enter()
    {
        foreach (var d in _death)
        {
            d.Death();
        } // アタッチされていれば、死亡時に何かしらアクションをする
        if (_animator) _animator.Play("Die");
        var renderers = _stateBase.gameObject.GetComponentsInChildren<Renderer>();
        foreach (var renderer in renderers)
        {
            renderer.material.color = Color.gray;
        }

        _generator.RemoveObj(_stateBase.gameObject);

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
        // Debug.Log("Exit Death State");
    }
}