using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using System.Linq;


public class UIController : MonoBehaviour
{

    [SerializeField] Text clothesText;
    [SerializeField] Slider hpSlider;
    [SerializeField] Image timerImage;
    [SerializeField] Image ondoImage;
    [SerializeField] GameObject clothesPanel;
    ClotheWrapperController cwController;
    
    [SerializeField] float upSpeed;
    [SerializeField] float downSpeed;
    Tween tw;
    bool isRestart = false;
    public static bool isMaxOndo = false;
    
    private void Start()
    {
        cwController = clothesPanel.GetComponent<ClotheWrapperController>();
    }

    public void UpdateText(int clothCount)
    {
        clothesText.text = $"服：{clothCount}枚";
    }

    public void UpdateTimer(float timer)
    {
        timerImage.DOFillAmount(timer, 0.1f).SetEase(Ease.Linear).SetLink(timerImage.gameObject);
    }

    public void UpdateAddClothView(GameObject cloth)
    {
        cwController.ViewCloth(cloth.tag);
    }
    public void UpdateDelClothView()
    {
        cwController.HideCloth();
    }



    public void UpdateOndo(bool isUp)
    {
        if(GameManager.I.gameState == GameState.GAMEOVER)
        {
            return;
        }
        if (isUp)
        {
            tw = ondoImage.DOFillAmount(1f, upSpeed)
                .SetEase(Ease.OutSine)
                .SetLink(ondoImage.gameObject)
                .OnComplete(() =>
                {
                    isMaxOndo = true;
                    ondoImage.DOFillAmount(0f, downSpeed)
                    .SetEase(Ease.OutSine)
                    .OnComplete(() => 
                        { 
                            SunController.isPlayerInTheSun = false; 
                        }
                    );
                }
            );
            if (!isRestart)
            {
                isRestart = true;
                tw.Play();
            }
            else
            {
                tw.Restart();
            }
            
        }
        else
        {
            tw.Pause();
            ondoImage.DOFillAmount(0f, downSpeed).SetEase(Ease.OutSine).SetLink(ondoImage.gameObject);
            SunController.isPlayerInTheSun = false;
        }
        
    }
}
