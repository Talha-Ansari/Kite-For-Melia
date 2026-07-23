using System.Collections;
using UnityEngine;

public class BirdClueFinder : MonoBehaviour
{
    LetterHolder target;
    bool findClue;

    void Awake()
    {
        findClue = true;
    }

    public void ShowHint()
    {
        if (!findClue) return;
        findClue = false;

        LetterHolder temp = LetterHolder.GetRandomHolder();
        if (temp != null)
        {
            target = temp;
            StartCoroutine(ToTarget());
        }
    }

    IEnumerator ToTarget()
    {

        while (Vector2.Distance(target.transform.position, transform.position) > 2)
        {
            transform.position += (target.transform.position - transform.position).normalized * Time.deltaTime * 2;
            yield return null;


        }
        if (!target.HasLetters())
            target.ShowLetter();
        findClue = true;
    }
}
