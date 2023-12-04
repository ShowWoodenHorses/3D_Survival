
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    public void TimeScaleOne()
    {
        Time.timeScale = 1.0f;
    }
    public void RestartGame()
    {
        SceneManager.LoadScene("GameScene");
    }
}
