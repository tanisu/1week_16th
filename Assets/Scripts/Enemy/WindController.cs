using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class WindController : MonoBehaviour
{
    private GameObject player;
    public float movementTime = 5f;
    public float vanishTime = 4f;
    public float scaleValue = 2f;
    public float scaleChangeTime = 2f;

    private float delta;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        MoveToPlayer();
    }

    void Update()
    {
        delta += Time.deltaTime;

        if (delta > vanishTime)
        {
            Destroy(gameObject);
        }

        if(gameObject.transform.position.y < -4f)
        {
            Destroy(gameObject);
        }
    }

    // プレイヤーに向かって拡大しながら移動
    void MoveToPlayer()
    {
        float x = player.transform.position.x;

        if(gameObject.transform.position.x - x < 0)
        {
            gameObject.transform.localScale = new Vector3(0.5f, 0.5f, 0.5f);
            transform.DOScale(new Vector3(scaleValue, scaleValue, scaleValue), scaleChangeTime).SetLink(gameObject);
        }
        else
        {
            gameObject.transform.localScale = new Vector3(-0.5f, 0.5f, 0.5f);
            transform.DOScale(new Vector3(-scaleValue, scaleValue, scaleValue), scaleChangeTime).SetLink(gameObject);
        }

        float i = Random.Range(-1.5f, 1.5f);


        transform.DOLocalMove(new Vector3(x + i, -4.2f, 0), movementTime).SetLink(gameObject);
    }
}
