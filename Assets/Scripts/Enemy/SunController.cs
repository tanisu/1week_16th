using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SunController : MonoBehaviour
{
    public static bool isPlayerInTheSun = false;
    private void OnTriggerStay2D(Collider2D collision)
    {
        /*PlayerÇ™âeÇÃíÜÇ»ÇÁreturn*/
        if (ShadowController.isPlayerSafe || GameManager.I.phaseState == PhaseState.KITAKAZE)
        {
            GameManager.I.UpdateOndo(false);
            return;
        }
        /*ÇªÇ§Ç≈Ç»ÇØÇÍÇŒâ∑ìxè„Ç∞ÇÈ*/
        if (collision.CompareTag("Player") && !isPlayerInTheSun)
        {
            isPlayerInTheSun = true;
            GameManager.I.UpdateOndo(true);
        }
    }
}
