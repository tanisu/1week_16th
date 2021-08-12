using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloneShadowController : MonoBehaviour
{
    /*Playerの安全フラグ*/
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            ShadowController.isPlayerSafe = true;
        }
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            ShadowController.isPlayerSafe = true;
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            ShadowController.isPlayerSafe = false;
        }
    }
}
