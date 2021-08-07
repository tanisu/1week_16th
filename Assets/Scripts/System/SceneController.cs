using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class SceneController : MonoBehaviour
{
    public static SceneController I { get; private set; }

    string currentSceneName;

    private void Awake()
    {
        if(I == null)
        {
            I = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        currentSceneName = SceneManager.GetActiveScene().name;
        _switchBGM(currentSceneName);
    }

    public void ChangeScene(string sceneName)
    {
        _switchBGM(sceneName, currentSceneName);
        SceneManager.LoadScene(sceneName);
    }

    private void _switchBGM(string sceneName,string stopSceneName = "")
    {
        Debug.Log(stopSceneName);
        switch (stopSceneName)
        {
            case "Title":
                AudioManager.I.StopTitle();
                break;
            case "Main":
                AudioManager.I.StopGame();
                break;
            case "Result":
                AudioManager.I.StopResult();
                break;
            default:
                break;
        }

        switch (sceneName)
        {
            case "Title":
                AudioManager.I.PlayTitle();
                break;
            case "Main":
                AudioManager.I.PlayGame();
                break;
            case "Result":
                AudioManager.I.PlayResult();
                break;
        }
    }
}
