using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// シーン遷移機能
/// </summary>
public class SceneChanger : MonoBehaviour
{
    [SerializeField, Header("遷移先のシーン名")] private string _sceneName;
    [SerializeField, Header("遷移時間")] private float _duration = 1.5f;
    public static SceneChanger Instance;
    private WaitForSeconds _wfs;
    private GameManager _gameManager;

    private void Awake()
    {
        Instance = this;
    }
    
    private void Start()
    {
        _wfs = new WaitForSeconds(_duration);
        _gameManager = FindObjectOfType<GameManager>();
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

    /// <summary>
    /// 同じステージをもう一度プレイ
    /// </summary>
    public void ToRestartScene()
    {
        SceneManager.LoadScene(_gameManager.GetRestartSceneName());
    }

    public void SetRestartScene(string sceneName)
    {
        _gameManager.SetRestartSceneName(sceneName);
    }
}