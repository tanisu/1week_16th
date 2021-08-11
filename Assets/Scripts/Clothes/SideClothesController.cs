using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class SideClothesController : MonoBehaviour
{
    // ↓跳ねる動きのときに使用（最終的に消す）
    /*
    public float horizontalDuration;
    public float horizontalEndPoint;
    public float verticalDuration;
    public float verticalEndPoint;
    */
    
    public float movementTime = 40f;

    void Start()
    {
        //右に生成された場合は左へ。左に生成された場合は右へ
        if(transform.position.x > 0)
        {
            //↓跳ねる用（後で消す）
            //this.GetComponent<Transform>().DOMoveX(-horizontalEndPoint, horizontalDuration).SetEase(Ease.Linear).SetLink(gameObject);

            float i = transform.position.x - 13f;

            transform.DOLocalPath(new Vector3[]
            {
                new Vector3(7.14f + i,-0.28f ,0),
                new Vector3(3.35f + i, 0, 0),
                new Vector3(3.84f + i, -1.69f, 0),
                new Vector3(5.55f + i, -3.1f, 0),
                new Vector3(4.58f + i,-3.16f , 0),
                new Vector3(-1.31f + i, 0.99f, 0),
                new Vector3(-0.43f + i, 1.65f,0),
                new Vector3(0.56f + i,1.16f ,0),
                new Vector3(-0.65f + i,-3.11f, 0),
                new Vector3(-1.59f + i, -3.14f, 0),
                new Vector3(-4.76f + i, -1.26f, 0),
                new Vector3(-22.0f, 0, 0),
            }, movementTime,
            PathType.CatmullRom).SetLink(gameObject);

        }
        else
        {
            //↓跳ねる用（後で消す）
            //this.GetComponent<Transform>().DOMoveX(horizontalEndPoint, horizontalDuration).SetEase(Ease.Linear).SetLink(gameObject);

            float i = transform.position.x + 13f;

            transform.DOLocalPath(new Vector3[]
            {
                new Vector3(-7.14f + i,-0.28f ,0),
                new Vector3(-3.35f + i, 0, 0),
                new Vector3(-3.84f + i, -1.69f, 0),
                new Vector3(-5.55f + i, -3.1f, 0),
                new Vector3(-4.58f + i,-3.16f , 0),
                new Vector3(1.31f + i, 0.99f, 0),
                new Vector3(0.43f + i, 1.65f,0),
                new Vector3(-0.56f + i,1.16f ,0),
                new Vector3(0.65f + i,-3.11f, 0),
                new Vector3(1.59f + i, -3.14f, 0),
                new Vector3(4.76f + i, -1.26f, 0),
                new Vector3(22.0f, 0, 0),
            }, movementTime,
            PathType.CatmullRom).SetLink(gameObject);

        }

        //↓跳ねる用（後で消す）
        //this.GetComponent<Transform>().DOMoveY(verticalEndPoint, verticalDuration).SetEase(Ease.OutFlash).SetLoops(-1, LoopType.Yoyo).SetLink(gameObject);
    }


    void Update()
    {
        //以下の条件の画面外に出た時にオブジェクトを破壊
        if (transform.position.x < -17.0f || transform.position.x > 17.0f)
        {
            Destroy(gameObject);
        }

    }
}
