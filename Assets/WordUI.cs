using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

public class LetterUI : MonoBehaviour, IPointerDownHandler
{

    char letter;
    [SerializeField] TMP_Text letterTXT;
    public void OnPointerDown(PointerEventData eventData)
    {
        if (Letter.Instance != null)
        {
            Letter.Instance.ChangeLetter(letter);
        }
    }

    public void SetLetter(char letter)
    {
        this.letter = letter;
        letterTXT.text = letter.ToString();
    }
}
