using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.SceneManagement;
public class DragAndDrop_ : MonoBehaviour
{
    public Sprite[] Levels;
    public List<piceseScript> pieces;

    public GameObject EndMenu;
    public GameObject SelectedPiece;
    int OIL = 1;
    public int PlacedPieces = 0;
    Vector3 startPoz; public LayerMask detectionLayer;
    [SerializeField] GameObject pieceBackground;
    int numberOfPieces;
    void Start()
    {
        numberOfPieces = pieces.Count;
        foreach (piceseScript item in pieces)
        {
            GameObject temp = Instantiate(pieceBackground);
            temp.transform.localScale = pieceBackground.transform.localScale;
            temp.transform.position = pieceBackground.transform.position;
            temp.transform.parent = item.transform;
            item.GetSprite();
            item.piecePic.sprite = Levels[Random.Range(0, Levels.Length)];
        }
        PuzzlePieceBelt.Instance.SetAllPieces(pieces);
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {


            RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero, 1, detectionLayer);
            if (hit)
                if (hit.transform.CompareTag("Puzzle"))
                {
                    if (!hit.transform.GetComponent<piceseScript>().InRightPosition)
                    {
                        SelectedPiece = hit.transform.gameObject;
                        startPoz = hit.transform.position;
                        SelectedPiece.GetComponent<piceseScript>().Selected = true;
                        SelectedPiece.GetComponent<SortingGroup>().sortingOrder = OIL;
                        OIL++;
                    }
                }
        }

        if (Input.GetMouseButtonUp(0))
        {

            if (SelectedPiece != null)
            {
                SelectedPiece.GetComponent<piceseScript>().Selected = false;
                if (!SelectedPiece.GetComponent<piceseScript>().Check())
                {
                    SelectedPiece.transform.position = startPoz;
                }
                SelectedPiece = null;
            }


        }
        if (SelectedPiece != null)
        {
            Vector3 MousePoint = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            SelectedPiece.transform.position = new Vector3(MousePoint.x, MousePoint.y, 0);
        }


        if (PlacedPieces == numberOfPieces)
        {
            EndMenu.SetActive(true);
            StartCoroutine(WaitAndLoadScene());
        }
    }
    
    IEnumerator WaitAndLoadScene()
    {
        yield return new WaitForSeconds(3f);
        SceneManager.LoadScene("GameMenuScene");
    }
    public void NextLevel()
    {
        PlayerPrefs.SetInt("Level", PlayerPrefs.GetInt("Level") + 1);
        SceneManager.LoadScene("Game");
    }

    public void BacktoMenu()
    {
        SceneManager.LoadScene("Menu");
    }
}