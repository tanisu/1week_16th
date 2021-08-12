using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClotheWrapperController : MonoBehaviour
{
    [SerializeField] GameObject[] getClothes;
    float diff = 23.53f;
    int count = 0;
    public void ViewCloth(string clothTag)
    {
        foreach(GameObject cloth in getClothes)
        {
            if (cloth.CompareTag(clothTag))
            {
                GameObject tmp = Instantiate(cloth);
                tmp.transform.SetParent(transform,false);
                float y = 1f;
                if(clothTag == "Suit")
                {
                    y = 11f;
                } 
                if(clothTag == "Spacesuit")
                {
                    y = 8f;
                }
                tmp.transform.localPosition = new Vector3(count * diff, y, 1f);
                tmp.GetComponent<SpriteRenderer>().sortingOrder = count;
                count++;
            }
        }
    }
}
