using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClothesGenerator : MonoBehaviour
{
    public GameObject fallClothesPrefab;
    public GameObject boundeClothesPrefab;
    public float span = 1.0f;
    float delta = 0;

    void Update()
    {
        GameObject clothes;

        this.delta += Time.deltaTime;
        if(this.delta > this.span)
        {

            int generateDice;
            generateDice = Random.Range(0, 11);

            //generateDiceが1 ~ 5の時に落ちる服を生成
            if (generateDice < 7)
            {
                clothes = Instantiate(fallClothesPrefab) as GameObject;

                //ランダムな場所に生成
                float x = Random.Range(-7.7f, 7.7f);
                clothes.transform.position = new Vector2(x, 6f);
            }
            else if(generateDice >= 8 && generateDice < 11)　//generateDiceが6 ~ 10の時に跳ねる服を生成
            {
                //跳ねる服を生成
                clothes = Instantiate(boundeClothesPrefab) as GameObject;

                //左右ランダムな場所に生成
                int i = Random.Range(0, 2);
                float x = Random.Range(10f, 16f);
                if(i == 0)
                {
                    clothes.transform.position = new Vector2(x, -3.5f);
                }
                else if(i == 1)
                {
                    clothes.transform.position = new Vector2(-x, -3.5f);
                }
            }
            delta = 0;
        }

    }
}
