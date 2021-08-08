using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;


public class UIController : MonoBehaviour
{

    [SerializeField] Text clothesText;
    [SerializeField] Slider hpSlider;
    [SerializeField] Image timerImage;


    public void UpdateHPSlider(int time)
    {
        if((hpSlider.maxValue - time) < 0)
        {
            return;
        }
        hpSlider.value = hpSlider.maxValue - time;

    }

    public void UpdateText(int clothCount)
    {
        clothesText.text = $"•žF{clothCount}–‡";
    }

    public void UpdateTimer(float timer)
    {
        timerImage.DOFillAmount(timer, 0.1f).SetEase(Ease.Linear).SetLink(timerImage.gameObject);
    }
}
