using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] UIController ui;
    [SerializeField] float playTime;
    [SerializeField] PhaseController phase;
    [SerializeField] SunController sun;
    public static GameManager I;
    private int clothCount = 0;
    
    
    private bool isGameOver = false;
    private bool[] isPhaseMove = { false, false, false };
    private float seconds = 0f;
    //WindGenerator????????????????????????????????
    public float Seconds
    {
        get { return this.seconds; }
        private set { this.seconds = value; }
    }

    private float startTime = 1.0f;

    public GameState gameState { get; private set; }
    public PhaseState phaseState { get; private set; }

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
        SceneController.I.SetScore(clothCount);
        gameState = GameState.PLAY;
        phaseState = PhaseState.KITAKAZE;
    }

    private void Update()
    {
        if(gameState == GameState.PLAY)
        {
            if(startTime > 0)
            {
                ui.UpdateTimer(_updateTimer());
                if(startTime > 0.7 && !isPhaseMove[0])
                {
                    
                    isPhaseMove[0] = true;
                    phase.CloudPhase();
                }
                else if(startTime > 0.4 && startTime < 0.7 && !isPhaseMove[1])
                {
                    phaseState = PhaseState.SUN;
                    isPhaseMove[1] = true;
                    phase.SunPhase();
                    
                }
                else if(startTime < 0.4 && !isPhaseMove[2])
                {
                    phaseState = PhaseState.BOTH;
                    isPhaseMove[2] = true;
                    phase.BothPhase();
                }
            }
            else
            {
                gameState = GameState.GAMEOVER;
            }
            
           
        }
        if(gameState == GameState.GAMEOVER && !isGameOver)
        {
            _gameOver();
        }

    }

    private void _gameOver()
    {
        SceneController.I.SetScore(clothCount);
        isGameOver = true;
        SceneController.I.ChangeScene("Result");
    }

    //↓服の種類によって点数を変えられるようにしました。
    public void GetCloth(int i)
    {
        clothCount = clothCount + i;
        ui.UpdateText(clothCount);
    }
    
    public void DelCloth(int i)
    {
        if((clothCount - i) > 0)
        {
            clothCount -= i;
            ui.UpdateText(clothCount);
        }
        else
        {
            ui.UpdateText(0);
        }
        
    }

    public void GetClothObj(GameObject cloth)
    {
        ui.UpdateAddClothView(cloth);
    }
    public void DelClothObj()
    {
        ui.UpdateDelClothView();
    }
    
    public void ChangeState(GameState state)
    {
        gameState = state;
    }
    
    public void UpdateOndo(bool isUp)
    {
        ui.UpdateOndo(isUp);
    }

    
    private float _updateTimer()
    {
        seconds += Time.deltaTime;
        float timerfloat = seconds / playTime;
        startTime = 1f - timerfloat;
        return startTime;
    }

}
