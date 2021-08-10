using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SunController : MonoBehaviour
{
    public static bool isPlayerInTheSun = false;
    private void OnTriggerStay2D(Collider2D collision)
    {
        /*Player���e�̒��Ȃ�return*/
        if (ShadowController.isPlayerSafe || GameManager.I.phaseState == PhaseState.KITAKAZE)
        {
            GameManager.I.UpdateOndo(false);
            return;
        }
        /*�����łȂ���Ή��x�グ��*/
        if (collision.CompareTag("Player") && !isPlayerInTheSun)
        {
            isPlayerInTheSun = true;
            GameManager.I.UpdateOndo(true);
        }
    }
}
