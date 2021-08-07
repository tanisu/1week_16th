using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class AudioManager : MonoBehaviour
{
    public static AudioManager I { get; private set; }

    AudioSource[] audioSources;
    float vol = 0.0f;
    [SerializeField] float duration = 0.5f;
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
        audioSources = GetComponents<AudioSource>();
    }


    public void PlayTitle()
    {
        audioSources[0].Play();
    }

    public void StopTitle()
    {
        audioSources[0].DOFade(vol, duration).OnComplete(() => audioSources[0].Stop());
         
    }

    public void PlayGame()
    {
        audioSources[1].Play();
    }
    public void StopGame()
    {
        audioSources[1].Stop();
    }

    public void PlayResult()
    {
        audioSources[2].Play();
    }
    public void StopResult()
    {
        audioSources[2].Stop();
    }
}
