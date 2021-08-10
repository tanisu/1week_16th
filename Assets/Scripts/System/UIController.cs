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
    Tweener tweener;


    public void UpdateText(int clothCount)
    {
        clothesText.text = $"•žF{clothCount}–‡";
    }

    public void UpdateTimer(float timer)
    {
        timerImage.DOFillAmount(timer, 0.1f).SetEase(Ease.Linear).SetLink(timerImage.gameObject);
    }

    public void UpdateOndo(bool isUp)
    {
        if (isUp)
        {
            
            tweener = ondoImage.DOFillAmount(1f, 1.57f).SetEase(Ease.InCubic).SetLink(ondoImage.gameObject).OnComplete(() =>
            {
                GameManager.I.DelCloth();
                ondoImage.DOFillAmount(0f, 0.13f).SetEase(Ease.InCubic).OnComplete(() => { SunController.isPlayerIn = false; });
            });
            tweener.Play();
        }
        else
        {
            ondoImage.DOFillAmount(0f, 0.13f).SetEase(Ease.InCubic).SetLink(ondoImage.gameObject);
        }
        
    }
}
