using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SunController : MonoBehaviour
{
    public static bool isPlayerInTheSun = false;
    private void OnTriggerStay2D(Collider2D collision)
    {
        /*Playerが影の中ならreturn*/
        if (ShadowController.isPlayerSafe || GameManager.I.phaseState == PhaseState.KITAKAZE)
        {
            GameManager.I.UpdateOndo(false);
            return;
        }
        /*そうでなければ温度上げる*/
        if (collision.CompareTag("Player") && !isPlayerInTheSun)
        {
            isPlayerInTheSun = true;
            GameManager.I.UpdateOndo(true);
        }
    }
}
