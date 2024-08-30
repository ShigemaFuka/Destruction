using UnityEngine;
using UnityEngine.EventSystems;

/// <summary>
/// カーソルの出入りを感知して処理を行う
/// </summary>
public class CursorPerception : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField] private GameObject _info = default;
    [SerializeField, Header("生成場所のObj")] private GameObject _posObj = default;
    private Vector3 _position = default; // 武器を配置する場所
    private WeaponGenerator _weaponGenerator = default;

    private void Start()
    {
        _position = _posObj.transform.position;
        _weaponGenerator = FindObjectOfType<WeaponGenerator>();
        _info.SetActive(false);
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        // todo: show info
        _info.SetActive(true);
        if (_weaponGenerator)
        {
            _weaponGenerator.SetPosition(_position);
            _weaponGenerator.SetGameObject(_posObj);
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        // todo: close info
        _info.SetActive(false);
    }
}