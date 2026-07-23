using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PuzzlePieceBelt : MonoBehaviour
{
    public static PuzzlePieceBelt Instance { get; private set; }
    List<piceseScript> allpieces;

    [SerializeField] float offSetOnY;
    [SerializeField] int displayCount;

    private void Awake()
    {
        Instance = this;
    }
    // Start is called before the first frame update


    public void SetAllPieces(List<piceseScript> allpieces)
    {
        this.allpieces = allpieces;
        ReArrangeTheList();
        GetPiceseOntheBelt();
    }

    void ReArrangeTheList()
    {
        int length = allpieces.Count;
        List<piceseScript> allpiecesTemp = new();

        for (int i = 0; i < length; i++)
        {
            allpiecesTemp.Add(allpieces[Random.Range(0, allpieces.Count)]);
            allpieces.Remove(allpiecesTemp[^1]);
        }
        allpieces = allpiecesTemp;
    }
    void GetPiceseOntheBelt()
    {
        for (int i = 0; i < allpieces.Count; i++)
        {
            if (i >= displayCount)
            {
                allpieces[i].transform.position = transform.position * 20;

            }
            else
                allpieces[i].transform.position = transform.position + offSetOnY * i * Vector3.down;
        }
    }


    public void SortBelt(piceseScript pieceToRemove)
    {
        if (allpieces.Count > 0)
        {
            int index = allpieces.IndexOf(pieceToRemove);
            allpieces.Remove(pieceToRemove);
            for (int i = index, j = 0; i < allpieces.Count; j++, i++)
            {
                if (i >= displayCount)
                {
                    allpieces[i].transform.position = transform.position * 20;

                }
                else
                    allpieces[i].transform.position = transform.position + offSetOnY * i * Vector3.down;
            }

        }
    }

}
