using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using System.Linq;

public class PlayerController : MonoBehaviour
{
    public float aceleration = 10f;
    public float limitSpeed = 10f;
    public static int direction = 1;
    Rigidbody2D rigid2D;
    AudioSource aud;

    [Header("効果音")]
    public AudioClip getSE;
    public AudioClip dropSE;

    [Header("服")]
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

    public GameObject sweatSprite;

    private GameObject touchedCloth;
    //タニス追記：服とポイントの辞書型宣言
    private Dictionary<string, int> clothPoint;

    /*リスト宣言*/
    private List<GameObject> getClothes;

    void Start()
    {
        this.aud = GetComponent<AudioSource>();
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

        /*リスト初期化*/
        getClothes = new List<GameObject>();

        DontDestroyOnLoad(gameObject);

    }

    void Update()
    {
        if(GameManager.I.gameState == GameState.PLAY)
        {

            Move();

            if (Input.GetKeyDown(KeyCode.Space))
            {
                Flip();
            }

            /*
            //タニス追記：温度MAXの処理
            //服が脱げる(最後に着た服が脱げる。服が脱げた後に次の服を切る前にもう一度ゲージがマックスになれば、次に服をとった瞬間その服が脱げる）
            if (UIController.isMaxOndo && touchedCloth != null && touchedCloth.activeSelf == true)
            {
                UIController.isMaxOndo = false;
                touchedCloth.SetActive(false);
                DropCloth(touchedCloth);
            }
            */

            //服が脱げる（最後に着た服が脱げる。服が脱げた後に次の服を切る前にもう一度ゲージがマックスになれば、服が脱げるエフェクトは発生せず−１点）
            if (UIController.isMaxOndo)
            {
                if (touchedCloth != null && touchedCloth.activeSelf == true)
                {
                    touchedCloth.SetActive(false);
                    DropCloth(touchedCloth);
                }
                else
                {
                    //1点マイナス
                    GameManager.I.DelCloth(1);
                    this.aud.PlayOneShot(this.dropSE);

                    //汗がでる
                    GameObject sp = Instantiate(sweatSprite) as GameObject;
                    sp.transform.position = gameObject.transform.position;
                }
                UIController.isMaxOndo = false;
            }
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
            //最後に来た服が脱げる服が脱げる
            if (touchedCloth != null && touchedCloth.activeSelf == true)
            {
                touchedCloth.SetActive(false);
                DropCloth(touchedCloth);
            }
            else //2連続の場合は汗が出て１点マイナス
            {
                //1点マイナス
                GameManager.I.DelCloth(1);
                this.aud.PlayOneShot(this.dropSE);

                //汗がでる
                GameObject sp = Instantiate(sweatSprite) as GameObject;
                sp.transform.position = gameObject.transform.position;
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
        //タニス追記：UIの服削除
        GameManager.I.DelClothObj();
        sp.transform.position = gameObject.transform.position;

        this.aud.PlayOneShot(this.dropSE);
    }

    //左右反転
    void Flip()
    {
        direction = direction * -1;
    }

    public void DestroyPlayer()
    {
        Destroy(gameObject);
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

        getClothes.Add(touchedCloth);

        Debug.Log(getClothes.Count);






        this.aud.PlayOneShot(this.getSE);






        //タニス追記：服による加点処理
        GameManager.I.GetCloth(clothPoint[other.gameObject.tag]);
        //タニス追記：UIに服追加
        GameManager.I.GetClothObj(other.gameObject);

        if (touchedCloth != null && touchedCloth.activeSelf == false)
        {
            touchedCloth.SetActive(true);
        }
    }
}
