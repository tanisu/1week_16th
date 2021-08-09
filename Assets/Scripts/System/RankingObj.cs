using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RankingObj : MonoBehaviour
{
    
    void Start()
    {
        naichilab.RankingLoader.Instance.SendScoreAndShowRanking(SceneController.I.score);
    }



}
