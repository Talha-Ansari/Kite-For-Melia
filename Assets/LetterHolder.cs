using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;

public class LetterHolder : MonoBehaviour
{

    public static List<LetterHolder> allLetterHolders = new();

    [SerializeField] char letter;
    [SerializeField] TMP_Text letterTxt;
    Letter currentLetter;
    bool hasLetter = false;
    void Awake()
    {
        allLetterHolders.Add(this);
    }
    void OnEnable()
    {
        if (!allLetterHolders.Contains(this))
            allLetterHolders.Add(this);

    }
    void OnDisable()
    {
        allLetterHolders.Remove(this);
    }
    public void ShowLetter()
    {
        letterTxt.text = letter.ToString();
    }
    public void SetLetter(char letter)
    {
        this.letter = letter;
    }
    public bool SetUp(Letter letter)
    {
        if (letter.GetLetter() != this.letter || hasLetter) return false;
        hasLetter = true;
        letter.transform.SetParent(transform);
        letterTxt.text = "";
        if (CheckAllLetters())
        {
            LevelManager.Instance.SetCurrentWord();
            LevelManager.Instance.Celebrate();
        }
        return true;
    }


    public static bool CheckAllLetters()
    {
        foreach (var item in allLetterHolders)
        {
            if (item.gameObject.activeInHierarchy)
                if (!item.hasLetter) return false;
        }
        return true;
    }

    public static LetterHolder GetRandomHolder()
    {
        List<LetterHolder> temp = allLetterHolders.Where(x => x.hasLetter == false && x.gameObject.activeInHierarchy).ToList();
        if (temp.Count > 0) return temp[Random.Range(0, temp.Count)];


        return null;
    }
    public void Reset()
    {
        if (currentLetter)
        {
            Destroy(currentLetter.gameObject);
            currentLetter = null;
        }
        hasLetter = false;
        gameObject.SetActive(false);
    }

    public void SetLetter(Letter currentLetter)
    {
        this.currentLetter = currentLetter;
        currentLetter.transform.position = transform.position;
        currentLetter.transform.rotation = Quaternion.identity;
        currentLetter.transform.localScale = Vector3.one * 2;
        hasLetter = true;
    }
    public bool HasLetters() => hasLetter;

}
