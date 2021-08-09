using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class PhaseObjController : MonoBehaviour
{
    public float endX;
    public Vector3 startPos;
    private void Start()
    {
        startPos = transform.position;
    }


}
