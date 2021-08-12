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
    public GameObject player;
    private Animator playerAnimator;

    //一番上の服。
    public static GameObject topCloth;
    

    [Header("効果音")]
    public AudioClip getSE;
    public AudioClip dropSE;

    [Header("服")]
    public GameObject spacesuit;
    public GameObject coat;
    public GameObject suit;
    public GameObject parker;
    public GameObject scarf;

    [Header("飛び出る服")]
    public GameObject spacesuitSprite;
    public GameObject coatSprite;
    public GameObject suitSprite;
    public GameObject parkerSprite;
    public GameObject scarfSprite;

    [Header("一番上の服(力技)")]
    public GameObject spacesuitTopSprite;
    public GameObject coatTopSprite;
    public GameObject suitTopSprite;
    public GameObject parkerTopSprite;
    public GameObject scarfTopSprite;

    public GameObject sweatSprite;

    private GameObject touchedCloth;
    //タニス追記：服とポイントの辞書型宣言
    private Dictionary<string, int> clothPoint;

    /*リスト宣言*/
    private List<GameObject> getClothes;

    void Start()
    {
        playerAnimator = player.GetComponent<Animator>();

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
        if(GameManager.I.gameState == GameState.GAMEOVER)
        {
            playerAnimator.SetBool("stopAnimation", !playerAnimator.GetBool("stopAnimation"));
            return;
        }

        Move();

        if (Input.GetKeyDown(KeyCode.Space))
        {
            Flip();
        }

        UpdateClothInf();


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
            if (topCloth != null)
            {
                InActiveCloth(topCloth);
                DropCloth(topCloth);
            }
            else
            {
                this.aud.PlayOneShot(this.dropSE);

                //汗がでる
                GameObject sp = Instantiate(sweatSprite) as GameObject;
                sp.transform.position = gameObject.transform.position;
            }
            UIController.isMaxOndo = false;
        }

    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (GameManager.I.gameState == GameState.GAMEOVER)
        {
            return;
        }

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
            //最後に来た服から順番に脱げる
            if (topCloth != null)
            {
                InActiveCloth(topCloth);
                DropCloth(topCloth);
            }
            else //2連続の場合は汗が出て１点マイナス
            {
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

        //リストから1つ減らす。
        getClothes.RemoveAt(getClothes.Count - 1);

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

    public void UpdateClothInf()
    {
        //リストの一番最後の服を代入。未取得であればnull
        if (1 <= getClothes.Count)
        {
            topCloth = getClothes[getClothes.Count - 1];
        }
        else
        {
            topCloth = null;
        }
        UpDateTopClothSprite();
    }

    //力技で一番上の服の表示を調整
    void UpDateTopClothSprite()
    {
        coatTopSprite.SetActive(false);
        scarfTopSprite.SetActive(false);
        parkerTopSprite.SetActive(false);
        suitTopSprite.SetActive(false);
        spacesuitTopSprite.SetActive(false);

        if (topCloth == null)
        {
            return;
        }

        if (coatTopSprite.tag == topCloth.tag)
        {
            coatTopSprite.SetActive(true);
        }
        else if (scarfTopSprite.tag == topCloth.tag)
        {
            scarfTopSprite.SetActive(true);
        }
        else if (parkerTopSprite.tag == topCloth.tag)
        {
            parkerTopSprite.SetActive(true);
        }
        else if (suitTopSprite.tag == topCloth.tag)
        {
            suitTopSprite.SetActive(true);
        }
        else if (spacesuitTopSprite.tag == topCloth.tag)
        {
            spacesuitTopSprite.SetActive(true);
        }
    }

    public void InActiveCloth(GameObject other)
    {
        GameObject inActiveCloth = null;

        if (other.gameObject.tag == "Coat")
        {
            inActiveCloth = coat;
        }

        if (other.gameObject.tag == "Scarf")
        {
            inActiveCloth = scarf;
        }

        if (other.gameObject.tag == "Parker")
        {
            inActiveCloth = parker;
        }

        if (other.gameObject.tag == "Suit")
        {
            inActiveCloth = suit;
        }

        if (other.gameObject.tag == "Spacesuit")
        {
            inActiveCloth = spacesuit;
        }

        inActiveCloth.SetActive(false);
    }


    }
