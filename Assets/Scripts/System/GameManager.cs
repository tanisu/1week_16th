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
    private bool isGameOver = false;
    
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
        gameState = GameState.PLAY;
    }

    private void Update()
    {
        if(gameState == GameState.PLAY)
        {
            ui.UpdateSlider(_counter());
        }
        if(gameState == GameState.GAMEOVER && !isGameOver)
        {
            
            isGameOver = true;
            SceneController.I.ChangeScene("Result");
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

    public void ChangeState(GameState state)
    {
        gameState = state;
    }

    private int _counter()
    {
        time += Time.deltaTime;
        return (int)time;
    }

}
