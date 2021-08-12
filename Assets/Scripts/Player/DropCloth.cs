using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class DropCloth : MonoBehaviour
{
    void Start()
    {
        if(PlayerController.direction < 0)
        {
            transform.localScale = new Vector3(- 1, 1, 1);
            transform.DOMove(new Vector3(1, 0.6f, 0), 0.4f).SetRelative(true).SetLink(gameObject).OnComplete(DestroyObj);
        }
        else
        {
            transform.localScale = new Vector3(1, 1, 1);
            transform.DOMove(new Vector3(-1, 0.6f, 0), 0.4f).SetRelative(true).SetLink(gameObject).OnComplete(DestroyObj);
        }
    }

    private void DestroyObj()
    {
        Destroy(gameObject);
    }

}
