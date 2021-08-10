using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WindGenerator : MonoBehaviour
{
    public GameObject windPrefab;
    public float span = 6.0f;

    private float delta = 4;
    private float second;

    void Update()
    {
        second = GameManager.I.Seconds;
        //北風の口元が以下の範囲内であれば、風弾を放出
        if (gameObject.transform.position.x > -6.5f && gameObject.transform.position.x < 6.5f)
        {
            this.delta += Time.deltaTime;
            if (this.delta > this.span)
            {
                Debug.Log(second);
                //40秒以降は3連ブレス。
                if(second < 40)
                {
                    Breath();
                }
                else
                {
                    TripleBreath();
                }
            }
        }
    }

    void Breath()
    {
        GameObject wind = Instantiate(windPrefab) as GameObject;
        wind.transform.position = this.gameObject.transform.position;
        delta = 2.5f;
    }

    void TripleBreath()
    {
        StartCoroutine(DelayBreath());
    }


    private IEnumerator DelayBreath()
    {
        Breath();
        yield return new WaitForSeconds(0.75f);
        Breath();
        yield return new WaitForSeconds(0.75f);
        Breath();
        //3連ブレスの後は気持ち間隔を長めに。
        delta = 2f;
    }
}
