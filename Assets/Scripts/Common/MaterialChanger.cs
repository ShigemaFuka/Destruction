using UnityEngine;

public class MaterialChanger : MonoBehaviour
{
    [SerializeField, Header("対象")] private GameObject _target = default;
    [SerializeField, Header("元々のマテリアル")] private Material _defaultMaterial = default;
    [SerializeField, Header("変更するマテリアル")] private Material _changedMaterial = default;
    private MeshRenderer _renderer = default;

    private void Start()
    {
        _renderer = _target.GetComponent<MeshRenderer>();
        ToDefaultMaterial();
    }

    /// <summary>
    /// デフォルトのマテリアルに変更
    /// </summary>
    public void ToDefaultMaterial()
    {
        _renderer.material = _defaultMaterial;
    }

    /// <summary>
    /// デフォルトではないマテリアルに変更
    /// </summary>
    public void ToChangedMaterial()
    {
        _renderer.material = _changedMaterial;
    }
}