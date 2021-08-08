using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class FallClothesController : MonoBehaviour
{
    public float duration;
    public float endPoint;

    // Start is called before the first frame update
    void Start()
    {
        this.GetComponent<Transform>().DOMoveY(endPoint, duration).SetEase(Ease.InSine).SetLink(gameObject);
    }

    // Update is called once per frame
    void Update()
    {

        if(transform.position.y < -6.0f)
        {
            Destroy(gameObject);
        }

    }
}
