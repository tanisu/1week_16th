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
    [SerializeField] float upSpeed;
    [SerializeField] float downSpeed;
    Tween tw;
    bool isRestart = false;
    public static bool isMaxOndo = false;


    public void UpdateText(int clothCount)
    {
        clothesText.text = $"服：{clothCount}枚";
    }

    public void UpdateTimer(float timer)
    {
        timerImage.DOFillAmount(timer, 0.1f).SetEase(Ease.Linear).SetLink(timerImage.gameObject);
    }

    /*???x??????????????*/
    public void UpdateOndo(bool isUp)
    {
        /*?????????????????i???z?????????????j*/
        if (isUp)
        {
            //?Q?[?W????????
            tw = ondoImage.DOFillAmount(1f, upSpeed)
                .SetEase(Ease.OutSine)
                .SetLink(ondoImage.gameObject)
                //?R?[???o?b?N????
                .OnComplete(() =>
                {
                    //????????
                    isMaxOndo = true;
                    //?Q?[?W????????
                    ondoImage.DOFillAmount(0f, downSpeed)
                    .SetEase(Ease.OutSine)
                    //??????????????
                    .OnComplete(() => 
                        { 
                            //???z???????????t???O???]
                            SunController.isPlayerInTheSun = false; 
                        }
                    );
                }
            );
            //???????????z????????????????Dotween??Play
            if (!isRestart)
            {
                isRestart = true;
                tw.Play();
            }
            //?????????~????Restart
            else
            {
                tw.Restart();
            }
            
        }
        /*???A??????????*/
        else
        {
            //?Q?[?W?????????????~????
            tw.Pause();
            //?Q?[?W????????
            ondoImage.DOFillAmount(0f, downSpeed).SetEase(Ease.OutSine).SetLink(ondoImage.gameObject);
            //???z?????????????t???O???]
            SunController.isPlayerInTheSun = false;
        }
        
    }
}
