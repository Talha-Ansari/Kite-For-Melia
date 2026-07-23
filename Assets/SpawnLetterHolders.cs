using System.Collections.Generic;
using UnityEngine;

public class SpawnLetterHolders : MonoBehaviour
{
    [SerializeField] LetterHolder word;
    [SerializeField] Transform holderPosition;
    [SerializeField] float offset;
    float tempOffset;
    [SerializeField] int length;
    List<LetterHolder> letterHolders = new();
    void Awake()
    {
        tempOffset = offset;
        for (var i = 0; i < length; i++)
        {
            Spawn();
        }

    }
    void Start()
    {
        LevelManager.Instance.StartGame(letterHolders);
    }
    public void Spawn()
    {
        Vector2 newPoz = holderPosition.position;
        newPoz.x += tempOffset;
        letterHolders.Add(Instantiate(word, newPoz, Quaternion.identity));

        tempOffset += offset;

    }

    public void Reset()
    {
        tempOffset = offset;
    }
}
