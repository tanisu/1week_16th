using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class BoundClothesController : MonoBehaviour
{
    public float horizontalDuration;
    public float horizontalEndPoint;
    public float verticalDuration;
    public float verticalEndPoint;

    void Start()
    {
        //右に生成された場合は左へ。左に生成された場合は右へ
        if(transform.position.x > 0)
        {
            this.GetComponent<Transform>().DOMoveX(-horizontalEndPoint, horizontalDuration).SetEase(Ease.Linear).SetLink(gameObject);
        }
        else
        {
            this.GetComponent<Transform>().DOMoveX(horizontalEndPoint, horizontalDuration).SetEase(Ease.Linear).SetLink(gameObject);
        }

        //上下に跳ねる
        this.GetComponent<Transform>().DOMoveY(verticalEndPoint, verticalDuration).SetEase(Ease.OutFlash).SetLoops(-1, LoopType.Yoyo).SetLink(gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        //以下の条件の画面外に出た時にオブジェクトを破壊
        if (transform.position.x < -17.0f || transform.position.x > 17.0f)
        {
            Destroy(gameObject);
        }

    }
}
