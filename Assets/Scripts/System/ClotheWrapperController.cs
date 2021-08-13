using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class ClotheWrapperController : MonoBehaviour
{
    [SerializeField] GameObject[] getClothes;
    float diff = 23.53f;
    int count = 0;
    int rowCount = 0;
    public int maxItem;
    float rowPos = -20f;
    private List<GameObject> currentCloth;

    private void Start()
    {
        currentCloth = new List<GameObject>();
    }

    public void ViewCloth(string clothTag)
    {
        foreach (GameObject cloth in getClothes)
        {
            if (cloth.CompareTag(clothTag))
            {
                GameObject tmp = Instantiate(cloth);
                tmp.transform.SetParent(transform, false);


                if (count > 0 && count % maxItem == 0)
                {
                    rowCount++;

                }
                float y = 1f + (rowPos * rowCount);
                if (clothTag == "Suit")
                {
                    y = 11f + (rowPos * rowCount); ;
                }
                if (clothTag == "Spacesuit")
                {
                    y = 8f + (rowPos * rowCount); ;
                }
                tmp.transform.localPosition = new Vector3(count % maxItem * diff, y, 1f);
                count++;
                tmp.GetComponent<SpriteRenderer>().sortingOrder = count;

                currentCloth.Add(tmp);
            }
        }
    }

    public void HideCloth()
    {

        if (currentCloth.Count > 0)
        {
            GameObject tmp = currentCloth.Last();
            currentCloth.Remove(currentCloth.Last());
            Destroy(tmp);
            count--;
            if (rowCount > 0 && count % maxItem == 0)
            {
                rowCount--;
            }
        }

    }
}
