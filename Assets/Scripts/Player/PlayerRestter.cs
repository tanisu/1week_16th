using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerRestter : MonoBehaviour
{
    private GameObject playerObject;

    private void Start()
    {
        playerObject = GameObject.FindGameObjectWithTag("Player");
        playerObject.GetComponent<Rigidbody2D>().isKinematic = true;
        playerObject.transform.position = new Vector3(0, 0, 0);
        /*
        playerObject.transform.localRotation = new Vector3(0, 0, 0);
        */
        playerObject.transform.localScale = new Vector3(3, 3, 3);
    }

    public void ResetPlayer()
    {
        Destroy(playerObject);
    }

}
