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
        clothesText.text = $"���F{clothCount}��";
    }

    public void UpdateTimer(float timer)
    {
        timerImage.DOFillAmount(timer, 0.1f).SetEase(Ease.Linear).SetLink(timerImage.gameObject);
    }

    /*���x�̏グ��������*/
    public void UpdateOndo(bool isUp)
    {
        /*�オ���Ă��������i���z�ɓ������Ă�j*/
        if (isUp)
        {
            //�Q�[�W���グ��
            tw = ondoImage.DOFillAmount(1f, upSpeed)
                .SetEase(Ease.OutSine)
                .SetLink(ondoImage.gameObject)
                //�R�[���o�b�N����
                .OnComplete(() =>
                {
                    //�����炷
                    GameManager.I.DelCloth();
                    //�Q�[�W��������
                    ondoImage.DOFillAmount(0f, downSpeed)
                    .SetEase(Ease.OutSine)
                    //������؂�����
                    .OnComplete(() => 
                        { 
                            //���z�������Ă�t���O���]
                            SunController.isPlayerInTheSun = false; 
                        }
                    );
                }
            );
            //�������񑾗z�ɓ������Ă�Ȃ�Dotween��Play
            if (!isRestart)
            {
                isRestart = true;
                tw.Play();
            }
            //���ڈȍ~�Ȃ�Restart
            else
            {
                tw.Restart();
            }
            
        }
        /*���A�ɋ��鏈��*/
        else
        {
            //�Q�[�W���オ��̂��~�߂�
            tw.Pause();
            //�Q�[�W��������
            ondoImage.DOFillAmount(0f, downSpeed).SetEase(Ease.OutSine).SetLink(ondoImage.gameObject);
            //���z�ɓ������Ă�t���O���]
            SunController.isPlayerInTheSun = false;
        }
        
    }
}
