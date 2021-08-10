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


    public void UpdateText(int clothCount)
    {
        clothesText.text = $"服：{clothCount}枚";
    }

    public void UpdateTimer(float timer)
    {
        timerImage.DOFillAmount(timer, 0.1f).SetEase(Ease.Linear).SetLink(timerImage.gameObject);
    }

    /*温度の上げ下げ処理*/
    public void UpdateOndo(bool isUp)
    {
        /*上がっていく処理（太陽に当たってる）*/
        if (isUp)
        {
            //ゲージを上げる
            tw = ondoImage.DOFillAmount(1f, upSpeed)
                .SetEase(Ease.OutSine)
                .SetLink(ondoImage.gameObject)
                //コールバック処理
                .OnComplete(() =>
                {
                    //服減らす
                    GameManager.I.DelCloth();
                    //ゲージを下げる
                    ondoImage.DOFillAmount(0f, downSpeed)
                    .SetEase(Ease.OutSine)
                    //下がり切ったら
                    .OnComplete(() => 
                        { 
                            //太陽当たってるフラグ反転
                            SunController.isPlayerInTheSun = false; 
                        }
                    );
                }
            );
            //もし初回太陽に当たってるならDotweenをPlay
            if (!isRestart)
            {
                isRestart = true;
                tw.Play();
            }
            //二回目以降ならRestart
            else
            {
                tw.Restart();
            }
            
        }
        /*日陰に居る処理*/
        else
        {
            //ゲージが上がるのを止める
            tw.Pause();
            //ゲージを下げる
            ondoImage.DOFillAmount(0f, downSpeed).SetEase(Ease.OutSine).SetLink(ondoImage.gameObject);
            //太陽に当たってるフラグ反転
            SunController.isPlayerInTheSun = false;
        }
        
    }
}
