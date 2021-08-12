using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerRestter : MonoBehaviour
{
    private GameObject playerObject;

    private void Start()
    {
        playerObject = GameObject.FindGameObjectWithTag("Player");
        if(playerObject != null)
        {
            playerObject.GetComponent<Rigidbody2D>().isKinematic = true;
            playerObject.transform.position = new Vector3(-0.36f, 1.71f, 0);
            playerObject.transform.localScale = new Vector3(4.5f, 4.5f, 4.5f);
        }
    }

    public void ResetPlayer()
    {
        Destroy(playerObject);
    }

}
