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
            //SceneController.I.ChangeScene("Result");
            return;
        }
        slider.value = slider.maxValue - time;

    }
}