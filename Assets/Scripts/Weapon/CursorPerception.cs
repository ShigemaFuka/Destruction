using System;
using UnityEngine;
using UnityEngine.EventSystems;

/// <summary>
/// カーソルの出入りを感知して処理を行う
/// </summary>
public class CursorPerception : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField] private GameObject _gameObject = default;

    private void Start()
    {
        _gameObject.SetActive(false);
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        // todo: show info
        _gameObject.SetActive(true);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        // todo: close info
        _gameObject.SetActive(false);
    }
}