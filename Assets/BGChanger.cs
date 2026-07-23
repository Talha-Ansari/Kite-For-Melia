using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BGChanger : MonoBehaviour
{
    [SerializeField] List<GameObject> bgImages;

    int currentIndex = 0;

    private void Start()
    {
        StartCoroutine(ChangeBG());
        // ChangeRandomly();
    }
    IEnumerator ChangeBG()
    {

        while (true)
        {
            ChangeRandomly();
            yield return new WaitForSeconds(Random.Range(30, 50));
        }
    }

    void ChangeRandomly()
    {
        bgImages[currentIndex].SetActive(false);
        currentIndex = Random.Range(0, bgImages.Count);
        bgImages[currentIndex].SetActive(true);
    }
}
