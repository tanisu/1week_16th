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
        currentSceneName = GetCurrentScene();
        AudioManager.I.PlayCurrentSceneBGM(currentSceneName);
    }

    public string GetCurrentScene()
    {
        return SceneManager.GetActiveScene().name;
    }

    public void ChangeScene(string sceneName)
    {
        FadeManager.I.FadeOI(() =>  _sceneTo(sceneName));
    }

    private void _sceneTo(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }


}
