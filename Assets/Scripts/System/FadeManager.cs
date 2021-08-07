using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class FadeManager : MonoBehaviour
{
    [SerializeField] CanvasGroup cg;
    public static FadeManager I;

    string currentScene;

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

    public void FadeIn()
    {

        cg.blocksRaycasts = true;
        cg.DOFade(0, 2f).OnComplete(() => {
            currentScene = SceneController.I.GetCurrentScene();
            AudioManager.I.PlayCurrentSceneBGM(currentScene);
            GameManager.I.ChangeState(currentScene);
            cg.blocksRaycasts = false; 
        });
    }


    public void FadeOI(TweenCallback action)
    {
        cg.blocksRaycasts = true;
        cg.DOFade(1, 2f).OnComplete(() => {
            currentScene = SceneController.I.GetCurrentScene();
            AudioManager.I.StopCurrentSceneBGM(currentScene);
            action();
            FadeIn();
        });
    }
}
