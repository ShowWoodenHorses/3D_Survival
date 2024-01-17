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

    public void StartLevelTwo()
    {
        SceneManager.LoadScene("Level_2");
        Time.timeScale = 1.0f;
    }

    public void StartLevelThree()
    {
        SceneManager.LoadScene("Level_3");
        Time.timeScale = 1.0f;
    }

    public void StartLevelSurvival()
    {
        SceneManager.LoadScene("Level_Survival");
        Time.timeScale = 1.0f;
    }
}
