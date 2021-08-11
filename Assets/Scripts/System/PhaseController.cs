using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class PhaseController : MonoBehaviour
{
    [SerializeField] GameObject[] phaseBG;
    SpriteRenderer[] sp;
    float[] phaseBGAlpha = { 0.3f, 0.47f }; //背景初期値
    const float VIEW_BG_TIME = 0.75f;
    const float IN_OUT_TIME = 0.5f;
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
        phaseCharacters[1].transform.DOMoveX(cloud.startPos.x, IN_OUT_TIME);
        sp[1].DOFade(0, VIEW_BG_TIME).OnComplete(() =>
        {
            phaseBG[1].SetActive(false);
            phaseBG[0].SetActive(true);
            sp[0].DOFade(phaseBGAlpha[0], VIEW_BG_TIME);
        });
        phaseCharacters[0].transform.DOMoveX(sun.endX, 8.5f).SetLoops(2, LoopType.Yoyo).SetLink(phaseCharacters[0].gameObject);           
    }

    public void CloudPhase()
    {
        sp[1].DOFade(phaseBGAlpha[1], VIEW_BG_TIME);
        phaseCharacters[1].transform.DOMoveX(cloud.endX, 5f);
    }

    public void BothPhase()
    {
        phaseBG[1].SetActive(true);
        sp[1].DOFade(phaseBGAlpha[1],0.2f);
        phaseBG[0].transform.DOMoveX(-9, IN_OUT_TIME);
        phaseBG[1].transform.DOMoveX(9, IN_OUT_TIME);

        phaseCharacters[1].transform.DOMoveX(5f, IN_OUT_TIME);
        phaseCharacters[0].transform.DOMoveX(-5f, IN_OUT_TIME);



    }


}
