using UnityEngine;
using UnityEngine.EventSystems;

public class LetterSelectorUI : MonoBehaviour
{
    [SerializeField] char startingLetter;
    [SerializeField] LetterUI prefab;
    [SerializeField] Transform holder;

    void Start()
    {
        SetAllLetters();
    }

    void SetAllLetters()
    {
        for (var i = 0; i < 26; i++)
        {
            LetterUI temp = Instantiate(prefab, holder);
            temp.SetLetter(startingLetter);
            IncreaseLetter();
        }

    }

    void IncreaseLetter() => startingLetter++;



    public void Activate()
    {
        gameObject.SetActive(true);
    }

    public void Deactivate()
    {
        gameObject.SetActive(false);
    }

}


