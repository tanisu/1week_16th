using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class ResultEnemy : MonoBehaviour
{
    public float moveDistance;
    public float moveTime;
    public float backTime;
    public float moveY;
    private int i;
    // Start is called before the first frame update
    void Start()
    {
        if(gameObject.transform.position.x < 0)
        {
            i = 1;
        }
        else
        {
            i = -1;
        }
        MoveCenter();
    }

    void MoveCenter()
    {
        transform.DOMove(new Vector3(moveDistance * i, moveY, 0), moveTime).SetEase(Ease.Linear).SetRelative(true).SetLink(gameObject).OnComplete(MoveBack);
    }

    void MoveBack()
    {
        transform.DOMove(new Vector3(-moveDistance * i, -moveY, 0), backTime).SetEase(Ease.Linear).SetRelative(true).SetLink(gameObject).OnComplete(MoveCenter);
    }

}
