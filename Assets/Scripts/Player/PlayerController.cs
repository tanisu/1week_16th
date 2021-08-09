using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float playerSpeed = 10f;
    Rigidbody2D rigid2D;

    // Start is called before the first frame update
    void Start()
    {
        rigid2D = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        Move();
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        //服をゲットするとスコアプラス
        if(other.gameObject.tag == "Clothes")
        {
            GameManager.I.GetCloth();
        }

        //TODO:↓風弾に触れるとスコアマイナス


        //触れたオブジェクトの破壊
        Destroy(other.gameObject);
    }

    //左右移動
    void Move()
    {
        float x = Input.GetAxisRaw("Horizontal");
        Vector2 direction = new Vector2(x, 0);
        rigid2D.velocity = direction * playerSpeed;

        //移動方向を向く
        if(x != 0)
        {
            transform.localScale = new Vector3(x, 1, 1);
        }
    }

}
