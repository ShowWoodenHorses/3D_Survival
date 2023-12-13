using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    public GameObject GameOverObj;
    public GameObject GameWinObj;

    public ContainerHostage containerHostage;
    public int countHostageForWin;
    public int countFreeHostage;


    private void OnEnable()
    {
        Hostage.hostageZone += Add;
        Hostage.hostageDie += GameOver;
        PlayerController.playerDie += GameOver;
    }
    private void OnDisable()
    {
        Hostage.hostageZone -= Add;
        Hostage.hostageDie -= GameOver;
        PlayerController.playerDie -= GameOver;
    }
    private void Awake()
    {
        countHostageForWin = containerHostage.listHostage.Count;
    }

    private void Update()
    {
        if (countFreeHostage == countHostageForWin)
        {
            GameWin();
        }
    }

    private void Add()
    {
        countFreeHostage++;
    }

    private void GameOver()
    {
        StartCoroutine(EndGameCoroutine(GameOverObj));
    }

    private void GameWin()
    {
        StartCoroutine(EndGameCoroutine(GameWinObj));
    }

    private IEnumerator EndGameCoroutine(GameObject obj)
    {
        yield return new WaitForSeconds(2f);
        obj.SetActive(true);
        Time.timeScale = 0f;
    }
    public void RestartGame()
    {
        string nameScene = SceneManager.GetActiveScene().name;
        SceneManager.LoadScene(nameScene);
        Time.timeScale = 1.0f;
    }
}
