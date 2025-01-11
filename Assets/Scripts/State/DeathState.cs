using UnityEngine;
using UnityEngine.AI;

/// <summary>
/// 死亡状態
/// 地面に落ちているように見せかけるように、Y軸Posを変更
/// </summary>
public class DeathState : IState
{
    private readonly StateBase _stateBase;
    private Generator _generator;
    private Animator _animator;
    private GameObject _model; // キャラクターやモンスター
    private IDeath[] _death;
    private bool _canFlag;
    private NavMeshAgent _agent;

    public DeathState(StateBase owner)
    {
        _stateBase = owner;
        _model = owner.GetComponent<Enemy>().Model;
        _animator = _model.GetComponent<Animator>();
        _death = owner.GetComponents<IDeath>();
        _generator = Generator.Instance;
        _agent = owner.GetComponent<NavMeshAgent>();
    }

    public void Enter()
    {
        _agent.enabled = false;
        _canFlag = true;
        foreach (var d in _death)
        {
            d.Death();
        } // アタッチされていれば、死亡時に何かしらアクションをする

        if (_animator) _animator.Play("Die");
        else Debug.Log("animがない");
        var renderers = _stateBase.gameObject.GetComponentsInChildren<Renderer>();

        foreach (var renderer in renderers)
        {
            renderer.material.shader = Shader.Find("Standard"); // シェーダーを変更
            renderer.material.SetColor("_Color", Color.gray); // 色を変更
        }

        _generator.RemoveObj(_stateBase.gameObject);

        // Debug.Log("Enter Death State");
    }

    public void Execute()
    {
        if (_model && _canFlag)
        {
            var pos = _model.transform.position;
            _model.transform.position = new Vector3(pos.x, -0.5f, pos.z); // 地面に落ちる感じ
            _canFlag = false;
        }
    }

    public void Exit()
    {
        // Debug.Log("Exit Death State");
    }
}