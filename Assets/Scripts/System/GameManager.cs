using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] UIController ui;
    
    public static GameManager I;
    private int clothCount = 0;
    private float time;
    private GameState gameState;
    
    private void Awake()
    {
        if (I == null)
        {
            I = this;
           // DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        
    }

    private void Update()
    {
        if(gameState == GameState.PLAY)
        {
            ui.UpdateSlider(_counter());
        }
    }

    public void GetCloth()
    {
        clothCount++;
    }
    public void DelCloth()
    {
        clothCount--;
    }

    public void ChangeState(string sceneName)
    {
        switch (sceneName)
        {
            case "Title":
                gameState = GameState.STOP;
                break;
            case "Main":
                gameState = GameState.PLAY;
                break;
            case "Result":
                gameState = GameState.GAMEOVER;
                break;
        }
        Debug.Log(gameState);
    }

    private int _counter()
    {
        time += Time.deltaTime;
        return (int)time;
    }

}
