using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class TitleButton : MonoBehaviour
{
    void Start()
    {
        transform.DOMoveY(-0.09f,2f).SetEase(Ease.InOutQuad).SetRelative(true).SetLink(gameObject).SetLoops(-1, LoopType.Yoyo);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
