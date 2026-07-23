using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class piceseScript : MonoBehaviour
{
    private Vector3 RightPosition;
    public bool InRightPosition;
    public bool Selected;
    public SpriteRenderer piecePic;

    void Awake()
    {
        RightPosition = transform.position;
    }
    void Start()
    {
        // transform.position = new Vector3(Random.Range(5f, 11f), Random.Range(2.5f, -7));
    }

    public void GetSprite()
    {

        piecePic = transform.GetChild(0).GetComponent<SpriteRenderer>();
    }


    public bool Check()
    {
        if (Vector3.Distance(transform.position, RightPosition) < 0.5f)
        {
            if (!Selected)
            {
                if (InRightPosition == false)
                {
                    transform.position = RightPosition;
                    InRightPosition = true;
                    GetComponent<SortingGroup>().sortingOrder = 0;
                    Camera.main.GetComponent<DragAndDrop_>().PlacedPieces++;
                    PuzzlePieceBelt.Instance.SortBelt(this);
                    return true;
                }
            }

        }
        return false;
    }

}
