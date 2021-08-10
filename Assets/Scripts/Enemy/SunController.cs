using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SunController : MonoBehaviour
{
    public static bool isPlayerIn = false;
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (ShadowController.isPlayerSafe)
        {
            GameManager.I.UpdateOndo(false);
            return;
        }

        if (collision.CompareTag("Player") && !isPlayerIn)
        {
            isPlayerIn = true;
            GameManager.I.UpdateOndo(true);
        }
    }
}
