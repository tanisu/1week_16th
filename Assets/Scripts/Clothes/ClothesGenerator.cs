using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClothesGenerator : MonoBehaviour
{
    public GameObject fallClothesPrefab;
    public GameObject boundeClothesPrefab;
    float span = 1.0f;
    float delta = 0;


    void Update()
    {
        GameObject clothes;

        this.delta += Time.deltaTime;
        if(this.delta > this.span)
        {
            //generateDiceが5以上の時に服を生成（出現頻度をバラけさせるため）
            int generateDice;
            generateDice = Random.Range(0, 11);
            if(generateDice >= 5)
            {
                //服を生成
                this.delta = 0;
                clothes = Instantiate(fallClothesPrefab) as GameObject;

                //ランダムな場所に生成
                float x = Random.Range(-7.7f, 7.7f);
                clothes.transform.position = new Vector2(x, 6f);
            }
            delta = 0;
        }

    }
}
