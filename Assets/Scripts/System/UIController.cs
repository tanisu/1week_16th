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
    [SerializeField] Image ondoImage;


    //public void UpdateHPSlider(int time)
    //{
    //    if((hpSlider.maxValue - time) < 0)
    //    {
    //        return;
    //    }
    //    hpSlider.value = hpSlider.maxValue - time;

    //}

    public void UpdateText(int clothCount)
    {
        clothesText.text = $"•žF{clothCount}–‡";
    }

    public void UpdateTimer(float timer)
    {
        timerImage.DOFillAmount(timer, 0.1f).SetEase(Ease.Linear).SetLink(timerImage.gameObject);
    }

    public void UpdateOndo()
    {
        ondoImage.DOFillAmount(1f, 3f).SetEase(Ease.InCubic).SetLink(ondoImage.gameObject).OnComplete(() =>
        {
            GameManager.I.DelCloth();
            ondoImage.DOFillAmount(0f, 0.5f).SetEase(Ease.InCubic).OnComplete(()=> { PhaseBlockController.isPlayerIn = false; });
        });
    }
}
