using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class SceneController : MonoBehaviour
{
    public static SceneController I { get; private set; }

    string currentSceneName;
    public int score { get; private set; }

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
        StartCoroutine(_initCurrentStage());
        
    }

    private IEnumerator _initCurrentStage()
    {
        yield return new WaitForSeconds(0.01f);
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

    public void SetScore(int clothCount)
    {
        score = clothCount;
    }


    private void _sceneTo(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }


}
