using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using DG.Tweening;
public class LevelManager : MonoBehaviour
{

    public static LevelManager Instance { private set; get; }
    [SerializeField] List<LevelWord_S> allLevels;
    [SerializeField] List<LetterHolder> allLetterHolders;
    int currentLevel = 0;
    [SerializeField] GameObject wordDisplayerHolder;
    [SerializeField] TMP_Text wordDisplayerTXT;
    [SerializeField] TMP_Text currentWordCount;
    public UnityEvent onWin;
    List<string> allWords = new List<string>();
    [SerializeField] GameObject celebritation;
    [SerializeField] List<Transform> dogs;
    int allWordsCount;
    string currentWord;
    int wordCount = 0;
    void Awake()
    {
        Instance = this;
    }
    public void StartGame(List<LetterHolder> letterHolders)
    {
        allLetterHolders = letterHolders;
        currentLevel = PlayerPrefs.GetInt("Level", 0);
        SetNextLevel();
    }
    public void IncreaseLevel()
    {
        wordCount = 0;
        celebritation.SetActive(false);
        currentLevel++;
        Debug.Log(currentLevel + " : " + allLevels.Count);
        if (currentLevel >= allLevels.Count)
        {
            GameManager.Instance.PreviousScene();
            return;
        }
        SetNextLevel();
    }
    void SetNextLevel()
    {
        allWords = allLevels[currentLevel].currentLevelWords.ToList();
        if (allLevels[currentLevel].currentLevelWords.Length > allLevels[currentLevel].maxWordSelected)
        {
            List<string> newList = new();
            allWordsCount = allLevels[currentLevel].maxWordSelected;
            for (var i = 0; i < allWordsCount; i++)
            {
                string word = allWords[Random.Range(0, allWords.Count)];
                allWords.Remove(word);
                newList.Add(word);
            }
            allWords = newList;
        }
        else
        {
            allWordsCount = allWords.Count;
        }
        SetCurrentWord();

    }
    public void Celebrate()
    {
        celebritation.transform.position = Player.Instance.transform.position;
        celebritation.SetActive(true);
        AnimateDogs();

    }

    public async void SetCurrentWord()
    {
        currentWordCount.text = "Words Completed : " + wordCount + " / " + allWordsCount;
        if (wordCount == allWordsCount)
        {
            AudioPlayer.instance.Play("Win");
            Celebrate();
            await Task.Delay(2000);
            // GameManager.Instance.PreviousScene();
            QuizManager.instance.ShowQuestion(currentLevel);
            return;
        }
        Player.Instance.SetCanMove(false);
        currentWord = allWords[Random.Range(0, allWords.Count)];
        allWords.Remove(currentWord);
        wordDisplayerTXT.text = currentWord;
        wordDisplayerHolder.SetActive(true);
        await Task.Delay(1000);
        wordCount++;
        SetupLetterHolder();
    }
    void SetupLetterHolder()
    {
        foreach (var item in allLetterHolders)
        {
            item.Reset();
        }

        for (var i = 0; i < currentWord.Length; i++)
        {
            allLetterHolders[i].gameObject.SetActive(true);
            allLetterHolders[i].SetLetter(currentWord[i]);
        }
        // for (var i = 0; i < currentWord.Length; i++)
        // {
        //     while (true)
        //     {
        //         int a = Random.Range(0, allLetterHolders.Count);
        //         if (!allLetterHolders[a].gameObject.activeInHierarchy)
        //         {
        //             allLetterHolders[a].gameObject.SetActive(true);
        //             break;
        //         }

        //     }
        // }

        // int j = 0;
        // for (var i = 0; i < allLetterHolders.Count; i++)
        // {

        //     if (allLetterHolders[i].gameObject.activeInHierarchy)
        //     {
        //         allLetterHolders[i].SetLetter(currentWord[j]);
        //         j++;
        //     }


        // }

        List<char> temp = GetShuffledLetters(currentWord);
        Debug.Log(temp);
        foreach (var item in temp)
        {
            LetterSpawner.Instance.Spawn(item);
        }
        LetterSpawner.Instance.Reset();
        Invoke(nameof(ActivateNextLevel), 3);

    }

    List<char> GetShuffledLetters(string word)
    {
        List<char> letters = new List<char>(word.ToCharArray());

        // Fisher–Yates shuffle
        for (int i = 0; i < letters.Count; i++)
        {
            int randomIndex = UnityEngine.Random.Range(i, letters.Count);
            char temp = letters[i];
            letters[i] = letters[randomIndex];
            letters[randomIndex] = temp;
        }

        return letters;
    }

    void ActivateNextLevel()
    {
        celebritation.SetActive(false);

        Player.Instance.SetCanMove(true);
        wordDisplayerHolder.SetActive(false);


    }

    void AnimateDogs()
    {

        foreach (Transform item in dogs)
        {
            item.localScale = Vector3.one;
            item.DOPunchScale(Vector3.one, 1);
        }

    }
    public int GetLevelSpeed() => currentLevel;
}
