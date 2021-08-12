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
    PhaseObjController kitakaze;
    public static bool sunMoveX = false;
    Tween[] tw = new Tween[2];
    private void Start()
    {
        sun = phaseCharacters[0].GetComponent<PhaseObjController>();
        kitakaze = phaseCharacters[1].GetComponent<PhaseObjController>();
        sp = new SpriteRenderer[2];
        for(int i = 0;i< sp.Length; i++)
        {
            sp[i] = phaseBG[i].GetComponent<SpriteRenderer>();
        }

    }

    public void SunPhase()
    {
        
        sp[1].DOFade(0, VIEW_BG_TIME).OnComplete(() =>
        {
            phaseBG[1].SetActive(false);
            phaseBG[0].SetActive(true);
            sp[0].DOFade(phaseBGAlpha[0], VIEW_BG_TIME);
        });
        phaseCharacters[0].transform.DOMoveY(1.7f, 5f).OnComplete(() =>
            {
                sunMoveX = true;
                phaseCharacters[0].transform.DOMoveX(sun.endX, 10.59f).OnComplete(()=> {
                    phaseCharacters[0].transform.DOMoveX(-10f, 10.3f);
                });
            }
        );
        
        
    }

    public void CloudPhase()
    {
        sp[1].DOFade(phaseBGAlpha[1], VIEW_BG_TIME);
        phaseCharacters[1].transform.DOMoveX(kitakaze.endX, 7.1f).SetLoops(kitakaze.loopTime, LoopType.Yoyo);



        //↓往復用
        //phaseCharacters[1].transform.DOMoveX(cloud.endX, 10f).SetLoops(-1, LoopType.Yoyo);
    }

    public void BothPhase()
    {
        
        phaseCharacters[1].transform.DOMoveX(5f, IN_OUT_TIME).OnComplete(()=> {
            tw[0] =  phaseCharacters[1].transform.DOMoveX(kitakaze.endX, 5f).SetLoops(-1, LoopType.Yoyo).SetLink(phaseCharacters[1].gameObject);
        }
        );
        phaseCharacters[0].transform.DOMoveX(-5f, IN_OUT_TIME).OnComplete(()=> {
            tw[1] = phaseCharacters[0].transform.DOMoveX(5, 5f).SetLoops(-1, LoopType.Yoyo).SetLink(phaseCharacters[0].gameObject);
        });

    }

    public void BothEnd()
    {
        
        tw[0].Pause();
        tw[1].Pause();
        
    }


}
