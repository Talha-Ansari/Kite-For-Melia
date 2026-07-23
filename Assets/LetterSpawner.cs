using System.Collections.Generic;
using UnityEngine;

public class LetterSpawner : MonoBehaviour
{
    public static LetterSpawner Instance { get; private set; }
    [SerializeField] Letter word;
    [SerializeField] Transform holderPosition;
    [SerializeField] float offset;
    float tempOffset;


    void Awake()
    {
        Instance = this;
        tempOffset = offset;
    }

    public void Spawn(char character)
    {
        Vector2 newPoz = holderPosition.position;
        newPoz.x += tempOffset;
        Letter temp = Instantiate(word, newPoz, Quaternion.identity);
        temp.ChangeLetter(character);
        tempOffset += offset;

    }

    public void Reset()
    {
        tempOffset = offset;
    }


}
