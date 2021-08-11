using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class PlayerController : MonoBehaviour
{
    public float aceleration = 10f;
    public float limitSpeed = 10f;
    public static int direction = 1;
    Rigidbody2D rigid2D;

    public GameObject spacesuit;
    public GameObject coat;
    public GameObject suit;
    public GameObject parker;
    public GameObject scarf;

    [Header("画像")]
    public GameObject spacesuitSprite;
    public GameObject coatSprite;
    public GameObject suitSprite;
    public GameObject parkerSprite;
    public GameObject scarfSprite;

    private GameObject touchedCloth;
    //タニス追記：服とポイントの辞書型宣言
    private Dictionary<string, int> clothPoint;
    void Start()
    {
        rigid2D = GetComponent<Rigidbody2D>();
        //タニス追記：服とポイントの辞書型定義
        clothPoint = new Dictionary<string, int>()
        {
            {"Coat",1 },
            {"Scarf",2 },
            {"Parker",3 },
            {"Suit",4 },
            {"Spacesuit",5 }
        };
    }

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
        if (other.CompareTag("PhaseBlock") || other.CompareTag("Shadow"))
        {
            return;
        }

        //触れた服を反映
        //タニス追記：風の当たり判定除外
        if (!other.CompareTag("Wind"))
        {
            TouchedClothes(other);
        }

        //風弾に触れるとスコアマイナス
        if (other.gameObject.tag == "Wind")
        {

            //服が脱げる
            if (touchedCloth != null && touchedCloth.activeSelf == true)
            {
                touchedCloth.SetActive(false);
                DropCloth(touchedCloth);
            }
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
    }

    public void DropCloth(GameObject other)
    {
        GameObject sp = null;

        if(other.gameObject.tag == "Coat")
        {
            sp = Instantiate(coatSprite) as GameObject;
        }
        if (other.gameObject.tag == "Scarf")
        {
            sp = Instantiate(scarfSprite) as GameObject;
        }
        if (other.gameObject.tag == "Parker")
        {
            sp = Instantiate(parkerSprite) as GameObject;
        }
        if (other.gameObject.tag == "Suit")
        {
            sp = Instantiate(suitSprite) as GameObject;
        }
        if (other.gameObject.tag == "Spacesuit")
        {
            sp = Instantiate(spacesuitSprite) as GameObject;
        }
        //タニス追記：服による減点処理
        GameManager.I.DelCloth(clothPoint[other.gameObject.tag]);
        sp.transform.position = gameObject.transform.position;
    }

    //左右反転
    void Flip()
    {
        direction = direction * -1;
    }

    //触れた服の種類を返す
    public void TouchedClothes(Collider2D other)
    {
        if (other.gameObject.tag == "Coat")
        {
            touchedCloth = coat;
        }

        if (other.gameObject.tag == "Scarf")
        {
            touchedCloth = scarf;
        }

        if (other.gameObject.tag == "Parker")
        {
            touchedCloth = parker;
        }

        if (other.gameObject.tag == "Suit")
        {
            touchedCloth = suit;
        }

        if (other.gameObject.tag == "Spacesuit")
        {
            touchedCloth = spacesuit;
        }

        //タニス追記：服による加点処理
        GameManager.I.GetCloth(clothPoint[other.gameObject.tag]);
        if (touchedCloth != null && touchedCloth.activeSelf == false)
        {
            touchedCloth.SetActive(true);
        }
    }
}
