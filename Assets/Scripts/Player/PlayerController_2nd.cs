using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController_2nd : MonoBehaviour
{
    public float aceleration = 10f;
    public float limitSpeed = 10f;
    public int direction = 1;
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

        if(Input.GetKeyDown(KeyCode.Space))
        {
            Flip();
        }

    }

    void OnTriggerEnter2D(Collider2D other)
    {

        //タニス追記：太陽フェーズの当たり判定
        if (other.CompareTag("PhaseBlock"))
        {
            return;
        }

        //服をゲットするとスコアプラス
        if (other.gameObject.tag == "Clothes")
        {
            GameManager.I.GetCloth();
        }

        //TODO:↓風弾に触れるとスコアマイナス
        if (other.gameObject.tag == "Wind")
        {
            GameManager.I.DelCloth();
        }
        //触れたオブジェクトの破壊
        Destroy(other.gameObject);
    }

    //左右移動
    void Move()
    {
        float x = aceleration * direction;
        Vector2 force = new Vector2(x, 0);
        rigid2D.AddForce(force);

        if (rigid2D.velocity.magnitude > limitSpeed)
        {
            rigid2D.velocity = rigid2D.velocity.normalized * limitSpeed;
        }

        //進行方向を向く
        transform.localScale = new Vector3(direction, 1, 1);

        /*
        Vector2 direction = new Vector2(x, 0);
        rigid2D.velocity = direction * playerSpeed;
        */

    }

    //左右反転
    void Flip()
    {
        direction = direction * -1;
    }

}