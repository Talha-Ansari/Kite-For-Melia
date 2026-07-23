using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Threading.Tasks;
using TMPro;

[System.Serializable]
public class Question
{
    public string questionText;
    public string[] options;
    public int correctOptionIndex;
}

public class QuizManager : MonoBehaviour
{
    public static QuizManager instance { get; private set; }

    [Header("Questions")]
    [SerializeField]
    private Question[] questions;
    [Header("UI References")]
    [SerializeField] private TMP_Text questionDisplay;
    [SerializeField] private Button[] optionButtons;
    [SerializeField] private TMP_Text feedbackText;
    bool isSelected;
    private int currentQuestionIndex = 0;

    void Awake()
    {
        instance = this;
    }
    private void Start()
    {
        questions = new Question[]
    {
        new Question
        {
            questionText = "Will the older kids let Melia play with their kites?",
            options = new string[]
            {
                "Yes, they are friendly to her.",
                "No, they won't let her play with their kites.",
                "Only if she gives them her own kite.",
                "Yes, but only for a short time."
            },
            correctOptionIndex = 1
        },
        new Question
        {
            questionText = "Where did Melia learn how to make her kite?",
            options = new string[]
            {
                "From her best friend.",
                "At the park.",
                "At the library.",
                "By watching the older kids."
            },
            correctOptionIndex = 2
        },
        new Question
        {
            questionText = "Why does Melia want to make a kite?",
            options = new string[]
            {
                "To win a kite-flying competition.",
                "To play with her friends.",
                "To decorate her room.",
                "To send a note or letter to Ginger."
            },
            correctOptionIndex = 3
        }
    };
        feedbackText.text = string.Empty;
        feedbackText.gameObject.SetActive(false);
        for (var i = 0; i < optionButtons.Length; i++)
        {
            int option = i;
            optionButtons[i].onClick.AddListener(() => { OnOptionSelected(option); });
        }
        gameObject.SetActive(false);
    }

    public void ShowQuestion(int index)
    {
        currentQuestionIndex = index;
        gameObject.SetActive(true);
        // if (index < 0 || index >= questions.Length)
        // {
        //     questionDisplay.text = "Quiz Complete!";
        //     foreach (var btn in optionButtons)
        //         btn.gameObject.SetActive(false);
        //     return;
        // }

        Question q = questions[index];
        questionDisplay.text = q.questionText;
        feedbackText.text = string.Empty;

        for (int i = 0; i < optionButtons.Length; i++)
        {
            if (i < q.options.Length)
            {
                optionButtons[i].gameObject.SetActive(true);
                optionButtons[i].GetComponentInChildren<TMP_Text>().text = q.options[i];
                int captured = i;
                optionButtons[i].onClick.RemoveAllListeners();
                optionButtons[i].onClick.AddListener(() => OnOptionSelected(captured));
            }
            else
            {
                optionButtons[i].gameObject.SetActive(false);
            }
        }
    }

    async void OnOptionSelected(int selectedIndex)
    {
        if (isSelected) return;
        isSelected = true;
        Question q = questions[currentQuestionIndex];
        feedbackText.gameObject.SetActive(false);

        if (selectedIndex == q.correctOptionIndex)
        {
            feedbackText.text = "Next Level";

        }
        else
        {

            feedbackText.text = "Next Level";
            optionButtons[selectedIndex].GetComponent<Image>().color = Color.red;

        }
        if (currentQuestionIndex + 1 == questions.Length)
            feedbackText.text = " Read A Kite for Melia. ";
        optionButtons[q.correctOptionIndex].GetComponent<Image>().color = Color.green;
        await Task.Delay(1000);
        foreach (var item in optionButtons)
        {
            item.gameObject.SetActive(false);
        }
        await Task.Delay(500);

        feedbackText.gameObject.SetActive(true);
        await Task.Delay(3000);
        Reset();
        gameObject.SetActive(false);
        LevelManager.Instance.IncreaseLevel();
    }

    void Reset()
    {
        foreach (var item in optionButtons)
        {
            item.GetComponent<Image>().color = Color.white;
        }
        isSelected = false;
        feedbackText.text = "";

    }
}
