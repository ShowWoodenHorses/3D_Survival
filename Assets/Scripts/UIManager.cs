using UnityEditor;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour 
{
    public MenuManager menuManager;
    public GameManager gameManager;
    public ContainerHostage containerHostage;
    public int countHostageForWin;
    public int countHostageFree;
    public int countStartEnemy;
    public int countKillEnemy;

    //Text
    [SerializeField] Text countHostageText;
    [SerializeField] Text countHostageFreeText;
    [SerializeField] Text countStartEnemyText;
    [SerializeField] Text countKillEnemyText;

    public GameObject GameOverObj;
    public GameObject GameWinObj;

    private void OnEnable()
    {
        Hostage.hostageZone += AddHostage;
        EnemyController.enemyDie += AddEnemy;
        Hostage.hostageDie += GameOver;
        PlayerController.playerDie += GameOver;
    }

    private void OnDisable()
    {
        Hostage.hostageZone -= AddHostage;
        EnemyController.enemyDie -= AddEnemy;
        Hostage.hostageDie -= GameOver;
        PlayerController.playerDie -= GameOver;
    }
    private void Start()
    {
        countHostageForWin = containerHostage.listHostage.Count;
        countStartEnemy = gameManager.positionsEnemyGun.Count + gameManager.positionsEnemyKnife.Count;
        countHostageText.text = countHostageForWin.ToString();
        countStartEnemyText.text = countStartEnemy.ToString();
    }

    private void Update()
    {
        if (countHostageFree == countHostageForWin && countKillEnemy == countStartEnemy)
        {
            GameWin();
        }
        countHostageFreeText.text = countHostageFree.ToString();
        countKillEnemyText.text = countKillEnemy.ToString();
    }

    private void AddHostage()
    {
        countHostageFree++;
    }
    private void AddEnemy()
    {
        countKillEnemy++;
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
        obj?.SetActive(true);
        Time.timeScale = 0f;
    }

}
