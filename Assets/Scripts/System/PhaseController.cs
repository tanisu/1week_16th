using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class PhaseController : MonoBehaviour
{
    [SerializeField] GameObject[] phaseBG;
    SpriteRenderer[] sp;
    float[] phaseBGAlpha = { 0.3f, 0.47f }; //îwåièâä˙íl
    [SerializeField] GameObject[] phaseCharacters;
    PhaseObjController sun; 
    PhaseObjController cloud;
    private void Start()
    {
        sun = phaseCharacters[0].GetComponent<PhaseObjController>();
        cloud = phaseCharacters[1].GetComponent<PhaseObjController>();
        sp = new SpriteRenderer[2];
        for(int i = 0;i< sp.Length; i++)
        {
            sp[i] = phaseBG[i].GetComponent<SpriteRenderer>();
        }

    }

    public void SunPhase()
    {
        sp[0].DOFade(0.3f,1f);
        /*ëæózÇÕçsÇ¡ÇΩÇ¡Ç´ÇË*/
        //phaseCharacters[0].transform.DOMoveX(sun.endX, 20f).OnComplete(() => {
        //    phaseCharacters[0].transform.position = sun.startPos;
        //});

        /*ëæózÇÕñﬂÇ¡ÇƒÇ≠ÇÈ*/
        phaseCharacters[0].transform.DOMoveX(sun.endX, 8.5f).SetLoops(2, LoopType.Yoyo).SetLink(phaseCharacters[0].gameObject);           
    }

    public void CloudPhase()
    {
        sp[0].DOFade(0, 1f).OnComplete(() =>
        {
            phaseBG[0].SetActive(false);
            phaseBG[1].SetActive(true);
            sp[1].DOFade(phaseBGAlpha[1], 1f);
        });
        phaseCharacters[1].transform.DOMoveX(cloud.endX, 5f);
    }

    public void BothPhase()
    {
        phaseBG[0].SetActive(true);
        sp[0].DOFade(phaseBGAlpha[0],0.2f);
        phaseBG[0].transform.DOMoveX(-9, 0.5f);
        phaseBG[1].transform.DOMoveX(9, 0.5f);

        phaseCharacters[0].transform.DOMoveX(-5f, 0.5f);
        

    }


}
