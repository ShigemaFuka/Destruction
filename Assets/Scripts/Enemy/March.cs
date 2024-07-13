using UnityEngine;
using UnityEngine.AI;

/// <summary>
/// ルートに沿って移動する
/// </summary>
[RequireComponent(typeof(NavMeshAgent))]
public class March : MonoBehaviour
{
    [SerializeField] private float _speed = 1f;
    [SerializeField, Header("到達したとみなす距離")] private float _distance = 0.1f;
    [SerializeField, Header("経路オブジェクトの親")] private string _parentRouteName = default;
    [Tooltip("経路の位置情報")] private Vector3[] _positions = default;
    [Tooltip("めざす場所のインデックス番号")] private int _indexNum = default;
    private GameObject _parentRoute = default;
    private NavMeshAgent _agent = default;

    private void Start()
    {
        _parentRoute = GameObject.Find(_parentRouteName);
        _indexNum = 0; // 最初の目標地点
        _agent = GetComponent<NavMeshAgent>();
        _agent.speed = _speed;
        GetRoute();
    }

    private void Update()
    {
        Destination();
        Rotation();
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
        if (_indexNum == _positions.Length) return;
        _agent.SetDestination(_positions[_indexNum]);
        var distance = (transform.position - _positions[_indexNum]).sqrMagnitude;
        // だいたい近づいたら到達と見做す
        if (distance <= _distance) _indexNum++; // 次の目標地点を更新
    }

    /// <summary>
    /// 進行方向への回転
    /// </summary>
    private void Rotation()
    {
        var nextCorner = transform.position;
        if (_agent.path != null && _agent.path.corners.Length > 1)
        {
            nextCorner = _agent.path.corners[1];
            Debug.DrawLine(transform.position, nextCorner, Color.yellow);
        }

        var to = nextCorner - transform.position;
        var angle = Vector3.SignedAngle(transform.forward, to, Vector3.up);
        // 角度が30゜を越えていたら
        if (Mathf.Abs(angle) > 30)
        {
            var rotMax = _agent.angularSpeed * Time.deltaTime;
            var rot = Mathf.Min(Mathf.Abs(angle), rotMax);
            transform.Rotate(0f, rot * Mathf.Sign(angle), 0f);
        }
    }
}