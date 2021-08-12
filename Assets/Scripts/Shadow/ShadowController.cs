using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;


public class ShadowController : MonoBehaviour
{
    [SerializeField] Transform[] tfs;
    [SerializeField] GameObject cloneShadow;
    public float maxScaleX = 1.76f;
    public static bool isPlayerSafe = false;
    public float maxX = 4.5f;
    public float minX = -4.5f;


    SpriteRenderer sp;
    SpriteRenderer cloneSp;
    Transform cloneTf;
    float cloneStartX;
    float alpha = 0.6f;



    private void Start()
    {
        sp = GetComponent<SpriteRenderer>();
        cloneSp = cloneShadow.GetComponent<SpriteRenderer>();
        cloneTf = cloneShadow.transform;
        cloneStartX = cloneTf.localPosition.x;
    }

    private void Update()
    {

        if (GameManager.I.phaseState == PhaseState.SUN)
        {
            sp.color = new Color(1, 1, 1, alpha);
            cloneSp.color = new Color(1, 1, 1, alpha);
        }


        float x = tfs[0].position.x - tfs[1].position.x;

        if (PhaseController.sunMoveX && tfs[0].GetComponent<CloudController>().overMe)
        {
            if (x > maxX)
            {
                x = maxX;
            }
            if (x < minX)
            {
                x = minX;
            }

            transform.localPosition = new Vector3(x, transform.localPosition.y, transform.localPosition.z);
            cloneTf.localPosition = new Vector3(cloneStartX + x, transform.localPosition.y, transform.localPosition.z);

        }
        //if(tfs[1].position.x == -10f)
        //{
        //    transform.DOLocalMoveX(0, 0.1f).SetLink(gameObject);
        //    cloneTf.DOLocalMoveX(cloneStartX,0.1f).SetLink(cloneShadow);
        //}

        //}

    }


    /*Playerの安全フラグ*/
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            isPlayerSafe = true;
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            isPlayerSafe = false;
        }
    }

}
