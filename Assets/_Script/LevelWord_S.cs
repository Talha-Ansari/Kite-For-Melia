using UnityEngine;

[CreateAssetMenu(fileName = "LevelWord_S", menuName = "")]
public class LevelWord_S : ScriptableObject
{
    public string[] currentLevelWords;
    public int maxWordSelected = 3;
}