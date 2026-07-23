using TMPro;
using UnityEngine;

public class Letter : MonoBehaviour
{

    public static Letter Instance { get; private set; }

    public bool canBePicked = true;
    [SerializeField] TMP_Text letterTXT;
    [SerializeField] char letter;
    Vector2 startPosition;
    void Awake()
    {
        Instance = this;
        canBePicked = true;
        startPosition = transform.position;
    }
    public char GetLetter() => letter;
    public void ChangeLetter(char letter)
    {
        this.letter = letter;
        letterTXT.text = letter.ToString();
    }

    public void ResetLetterPosition()
    {
        transform.SetParent(null);
        transform.position = startPosition;
        transform.rotation = Quaternion.identity;
        transform.localScale = Vector3.one * 2;
        canBePicked = true;
    }

}
