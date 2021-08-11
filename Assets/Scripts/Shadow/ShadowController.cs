using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShadowController : MonoBehaviour
{
    [SerializeField] Transform[] tfs;
    public float maxScaleX = 1.76f;
    public static bool isPlayerSafe = false;
    SpriteRenderer sp;
    float alpha = 0.6f;
    private void Start()
    {
        sp = GetComponent<SpriteRenderer>();
    }

    private void Update()
    {        
        
        if(GameManager.I.phaseState == PhaseState.SUN)
        {
            sp.color = new Color(1,1,1,alpha) ;
        }
        if(GameManager.I.phaseState == PhaseState.BOTH)
        {
            gameObject.SetActive(false);
        }

        float x = tfs[0].position.x - tfs[1].position.x;
        
        if(x < 0)
        {
            transform.localPosition = new Vector3(x, transform.localPosition.y, transform.localPosition.z);
            if(Mathf.Abs(x) > 1 && transform.localScale.x < maxScaleX)
            {
                transform.localScale += new Vector3(0.005f, 0f, 0f);
            }
        }
        
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
