using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// シーン遷移機能
/// </summary>
public class SceneChanger : MonoBehaviour
{
    [SerializeField, Header("遷移先のシーン名")] private string _sceneName = default;
    [SerializeField, Header("遷移時間")] private float _duration = 1.5f;
    private WaitForSeconds _wfs = default;

    private void Start()
    {
        _wfs = new WaitForSeconds(_duration);
    }

    public void Change()
    {
        SceneManager.LoadScene(_sceneName);
    }

    public void Change(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }

    public IEnumerator LateChange()
    {
        yield return _wfs;
        SceneManager.LoadScene(_sceneName);
    }

    public IEnumerator LateChange(string sceneName)
    {
        yield return _wfs;
        SceneManager.LoadScene(sceneName);
    }
}