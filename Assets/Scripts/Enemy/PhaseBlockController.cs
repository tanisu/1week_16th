using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhaseBlockController : MonoBehaviour
{
    public static bool isPlayerIn = false;

    private void OnTriggerStay2D(Collider2D collision)
    {
        
        if (collision.CompareTag("Player") && !isPlayerIn)
        {
            isPlayerIn = true;
            GameManager.I.UpdateOndo();
        }
    }


}
