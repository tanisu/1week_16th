using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class UIController : MonoBehaviour
{

    [SerializeField] Text clothesText;
    [SerializeField] Slider slider;


    public void UpdateSlider(int time)
    {
        if((slider.maxValue - time) < 0)
        {
            GameManager.I.ChangeState(GameState.GAMEOVER);
            return;
        }
        slider.value = slider.maxValue - time;

    }
}
