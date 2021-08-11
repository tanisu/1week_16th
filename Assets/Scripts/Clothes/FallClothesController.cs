using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class FallClothesController : MonoBehaviour
{
    [Header("挙動")]
    public float duration;
    public float fallDistance;
    public float sideDistance;
    public float endPoint = -3.3f;

    [Header("消失")]
    public float waitTime;
    public float flickTime;

    private SpriteRenderer sprite;
    private bool killTween = false;

    // Start is called before the first frame update
    void Start()
    {
        sprite = gameObject.GetComponent<SpriteRenderer>();

        transform.DOMoveY(endPoint, duration).SetEase(Ease.Linear).SetLink(gameObject);

        //TODO:↓できればひらひら感を出す
        /*
        transform.DOLocalMoveY(-fallDistance, duration).SetEase(Ease.InOutCubic).SetRelative(true).SetLink(gameObject).SetLoops(-1, LoopType.Incremental);
        transform.DOLocalMoveX(sideDistance, duration).SetEase(Ease.InOutCubic).SetRelative(true).SetLink(gameObject).SetLoops(-1, LoopType.Yoyo);

        var sequence = DOTween.Sequence();
        sequence.Append(transform.DOLocalMoveY(fallDistance / 4, duration / 4).SetDelay(duration * 3 / 4).SetEase(Ease.Linear).SetRelative(true).SetLink(gameObject)).SetLoops(-1);
        */
    }

    // Update is called once per frame
    void Update()
    {

        if(transform.position.y <= endPoint)
        {
            if(killTween == false)
            {
                transform.DOKill();
                killTween = true;

                StartCoroutine("Vanish");
            }
        }

        if(transform.position.y < -6.0f)
        {
            Destroy(gameObject);
        }

    }

    //地面に落ちた後、明滅してしばらくするとオブジェクトを破壊
    private IEnumerator Vanish()
    {
        yield return new WaitForSeconds(waitTime);

        sprite.DOFade(0, 0.5f).SetLoops(-1).SetLink(gameObject);

        yield return new WaitForSeconds(flickTime);

        Destroy(gameObject);

    }



}
