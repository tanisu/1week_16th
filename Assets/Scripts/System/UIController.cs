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
    [SerializeField] Image clothImage;
    [SerializeField] float upSpeed;
    [SerializeField] float downSpeed;
    Tween tw;
    bool isRestart = false;
    public static bool isMaxOndo = false;
    private List<GameObject> getClothes;
    private List<Image> ViewImages;

    private void Start()
    {
        getClothes = new List<GameObject>();
        ViewImages = new List<Image>();
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
        //GameObject tmpObj = cloth;
        //getClothes.Add(tmpObj);
        //Image tmpImage = Instantiate(clothImage);
        
        //tmpImage.sprite = cloth.GetComponent<SpriteRenderer>().sprite;
        
        //ViewImages.Insert(0,tmpImage.GetComponent<Image>());
        //UpdateClotheVeiw();
    }
    public void UpdateDelClothView()
    {
        //if(getClothes.Count() > 0)
        //{
        //    getClothes.Remove(getClothes.Last());
        //    ViewImages.Remove(ViewImages.Last());
        //    for(int i = 0;i < clothesPanel.transform.childCount; i++)
        //    {
        //        if(i == (clothesPanel.transform.childCount - 1))
        //        {
        //            clothesPanel.transform.GetChild(i);
        //        }
        //    }
        //    //UpdateClotheVeiw();
        //}
    }

    void UpdateClotheVeiw()
    {
        //foreach(Image i in ViewImages)
        //{
        //    //i.transform.SetParent(clothesPanel.transform,false);
        //}
    }

    public void UpdateOndo(bool isUp)
    {
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
