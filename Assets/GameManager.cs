using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }
    void Awake()
    {
        Instance = this;
    }
    public void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        ResumeGame();
    }
    public void SetLevel(int level) => PlayerPrefs.SetInt("Level", level);
    public void NextScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        ResumeGame();
    }
    public void PreviousScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 1);
        ResumeGame();
    }
    public void PauseGame()
    {
        Time.timeScale = 0; Debug.Log("Game is paused");
    }
    public void ResumeGame() { Debug.Log("Resume Game"); Time.timeScale = 1; }

    public void Exit() => Application.Quit();
}
