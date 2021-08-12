using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class PhaseObjController : MonoBehaviour
{
    public float endX;
    public Vector3 startPos;
    public int loopTime;
    private void Start()
    {
        startPos = transform.position;
    }
    private void Update()
    {
        
        //if(Mathf.Abs(endX) -  Mathf.Abs(transform.position.x)  < 0.1f )
        //{
        //    Debug.Log("最後");
        //    transform.localScale = new Vector3(1, 1);
        //}
        //if(transform.position.x == startPos.x)
        //{
        //    transform.localScale = new Vector3(-1, 1);
        //}
    }

}
