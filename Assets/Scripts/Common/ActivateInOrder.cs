using System;
using System.Collections.Generic;
using UniRx;
using UnityEngine;

/// <summary>
/// リストの要素を順にアクティブ化する
/// 複数の要素が同時にアクティブ化することはない 
/// </summary>
public class ActivateInOrder : MonoBehaviour
{
    [SerializeField] private List<GameObject> _gameObjects;
    private int _index;

    private void Start()
    {
        Initialize();
    }

    public void ActivateObject()
    {
        if (_index >= _gameObjects.Count - 1) return;
        _index++;
        if (_gameObjects[_index - 1]) _gameObjects[_index - 1].SetActive(false);
        _gameObjects[_index].SetActive(true);
    }

    public void Initialize()
    {
        _index = 0;
        foreach (var go in _gameObjects)
        {
            go.SetActive(false);
        }

        _gameObjects[0].SetActive(true);
    }
}