using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    public void RestartGame()
    {
        string nameScene = SceneManager.GetActiveScene().name;
        SceneManager.LoadScene(nameScene);
        Time.timeScale = 1.0f;
    }

    public void PauseGame()
    {
        Time.timeScale = 0f;
    }

    public void ContinueGame()
    {
        Time.timeScale = 1.0f;
    }

    public void MainMenuClick()
    {
        SceneManager.LoadScene("MainMenu");
        Time.timeScale = 1.0f;
    }

    public void StartLevelOne()
    {
        SceneManager.LoadScene("Level_1");
        Time.timeScale = 1.0f;
    }
}
