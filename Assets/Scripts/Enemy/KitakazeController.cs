using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KitakazeController : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
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
