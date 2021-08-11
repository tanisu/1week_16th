using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClothesGenerator : MonoBehaviour
{
    public GameObject coatPrefab;
    public GameObject skarfPrefab;
    public GameObject parkerPrefab;
    public GameObject suitPrefab;
    public GameObject spaceSuitPrefab;
    public float span = 1.0f;

    private float delta = 0.8f;
    private GameObject clothes;
    private float second;

    void Update()
    {
        second = GameManager.I.Seconds;

        this.delta += Time.deltaTime;
        if(this.delta > this.span)
        {
            int generateDice;
            generateDice = Random.Range(0, 11);

            if(second < 40) // ゲーム開始40秒までは、80%：落ちてくる服、20％：横からの服
            {
                if (generateDice < 9)
                {
                    GenerateFallCloth();
                }
                else if (9 <= generateDice && generateDice < 11)
                {
                    GenerateSideCloth();
                }
            }
            else if(40 <= second)// ゲーム開始後40秒をすぎれば、60%：落ちてくる服、40%：横からの服
            {
                if (generateDice < 7)
                {
                    GenerateFallCloth();
                }
                else if (7 <= generateDice && generateDice < 11)
                {
                    GenerateSideCloth();
                }
            }
            delta = 0;
        }

    }

    //上部に生成
    public void GenerateFallCloth()
    {
        int fallDice;
        fallDice = Random.Range(0, 11);

        // 50% コート, 30% スカーフ, 20%　パーカー
        if(fallDice < 6)
        {
            clothes = Instantiate(coatPrefab) as GameObject;
        }
        else if(6 <= fallDice && fallDice < 9)
        {
            clothes = Instantiate(skarfPrefab) as GameObject;
        }
        else if(9 <= fallDice)
        {
            clothes = Instantiate(parkerPrefab) as GameObject;
        }

        float x = Random.Range(-7.7f, 7.7f);
        clothes.transform.position = new Vector2(x, 6f);
    }

    //左右に生成
    public void GenerateSideCloth()
    {
        int sideDice;
        sideDice = Random.Range(0, 11);

        //70% スーツ, 30% 宇宙服
        if (sideDice < 8)
        {
            clothes = Instantiate(suitPrefab) as GameObject;
        }
        else if (8 <= sideDice)
        {
            clothes = Instantiate(spaceSuitPrefab) as GameObject;
        }

        int i = Random.Range(0, 2);
        float x = Random.Range(10f, 16f);

        if (i == 0)
        {
            clothes.transform.position = new Vector2(x, -3.5f);
        }
        else if (i == 1)
        {
            clothes.transform.position = new Vector2(-x, -3.5f);
        }
    }
}
