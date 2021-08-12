using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class ClotheWrapperController : MonoBehaviour
{
    [SerializeField] GameObject[] getClothes;
    float diff = 23.53f;
    int count = 0;
    private List<GameObject> currentCloth;

    private void Start()
    {
        currentCloth = new List<GameObject>();
    }

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
                
                currentCloth.Add(tmp);
                count++;
            }
        }
    }

    public void HideCloth()
    {
        
        if(currentCloth.Count > 0)
        {
            GameObject tmp = currentCloth.Last();
            currentCloth.Remove(currentCloth.Last());
            Destroy(tmp);
            count--;
        }
        
        
    }
}
