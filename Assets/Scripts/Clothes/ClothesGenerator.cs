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

            if(second < 30) // ゲーム開始30秒までは、落ちてくる服:90%、横からの服:10%
            {
                if (generateDice < 10)
                {
                    GenerateFallCloth();
                }
                else if (10 <= generateDice && generateDice < 11)
                {
                    GenerateSideCloth();
                }
            }
            else if(30 <= second && second < 60)// ゲーム開始後30秒をすぎれば、落ちてくる服:80%、横からの服:20%
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
            else if (60 <= second)// ゲーム開始後60秒をすぎれば、落ちてくる服:60%、横からの服:40%
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

        // 40% コート, 30% スカーフ, 30%　パーカー
        if(fallDice < 5)
        {
            clothes = Instantiate(coatPrefab) as GameObject;
        }
        else if(5 <= fallDice && fallDice < 8)
        {
            clothes = Instantiate(skarfPrefab) as GameObject;
        }
        else if(8 <= fallDice)
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
