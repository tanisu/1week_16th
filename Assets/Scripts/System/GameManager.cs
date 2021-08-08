using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] UIController ui;
    [SerializeField] float playTime;
    public static GameManager I;
    private int clothCount = 0;
    private float hp;
    private GameState gameState;
    private bool isGameOver = false;
    private float seconds = 0f;
    private float startTime = 1.0f;

    
    private void Awake()
    {
        if (I == null)
        {
            I = this;           
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

            if(startTime > 0)
            {
                ui.UpdateTimer(_updateTimer());
            }
            else
            {
                gameState = GameState.GAMEOVER;
            }
            
           
        }
        if(gameState == GameState.GAMEOVER && !isGameOver)
        {
            
            isGameOver = true;
            SceneController.I.ChangeScene("Result");
        }

    }


    /*服増やす処理*/
    public void GetCloth()
    {
        clothCount++;
        ui.UpdateText(clothCount);
    }
    /*服減らす処理*/
    public void DelCloth()
    {
        clothCount--;
        ui.UpdateText(clothCount);
    }
    /*HPカウンター*/
    public void HpCounter()
    {
        hp += Time.deltaTime;
        ui.UpdateHPSlider((int)hp);
    }

    /*ゲームのステート変更*/
    public void ChangeState(GameState state)
    {
        gameState = state;
    }


    /*タイマー処理*/
    private float _updateTimer()
    {
        seconds += Time.deltaTime;
        float timerfloat = seconds / playTime;
        startTime = 1f - timerfloat;
        return startTime;
    }

}
