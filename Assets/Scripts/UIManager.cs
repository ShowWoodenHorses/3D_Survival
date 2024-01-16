using UnityEditor;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour 
{
    public MenuManager menuManager;
    public GameManager gameManager;
    public ContainerHostage containerHostage;
    public ContainerBomb containerBomb;
    public int countHostageForWin;
    public int countHostageFree;
    public int countStartEnemy;
    public int countKillEnemy;
    public int countStartBomb;
    public int countNeutralizeBomb;

    //Text
    [SerializeField] Text countHostageText;
    [SerializeField] Text countHostageFreeText;
    [SerializeField] Text countStartEnemyText;
    [SerializeField] Text countKillEnemyText;
    [SerializeField] Text countStartBombText;
    [SerializeField] Text countNeutralizeBombText;

    public GameObject GameOverObj;
    public GameObject GameWinObj;

    private void OnEnable()
    {
        Hostage.hostageZone += AddHostage;
        EnemyController.enemyDie += AddEnemy;
        Bomb.bombNeutralize += AddBomb;
        Hostage.hostageDie += GameOver;
        PlayerController.playerDie += GameOver;
        Bomb.bombActive += GameOver;
    }

    private void OnDisable()
    {
        Hostage.hostageZone -= AddHostage;
        EnemyController.enemyDie -= AddEnemy;
        Bomb.bombNeutralize -= AddBomb;
        Hostage.hostageDie -= GameOver;
        PlayerController.playerDie -= GameOver;
        Bomb.bombActive -= GameOver;
    }
    private void Start()
    {
        if (containerHostage != null)
            countHostageForWin = containerHostage.listHostage.Count;
        if (gameManager != null)
            countStartEnemy = gameManager.positionsEnemyGun.Count + gameManager.positionsEnemyKnife.Count;
        if (containerBomb != null)
        {
            countStartBomb = containerBomb.listBomb.Count;
            countStartBombText.text = countStartBomb.ToString();
        }
        countHostageText.text = countHostageForWin.ToString();
        countStartEnemyText.text = countStartEnemy.ToString();
    }

    private void Update()
    {
        //if (countHostageFree == countHostageForWin && countKillEnemy == countStartEnemy)
        //{
        //    GameWin();
        //}
    }

    private void AddHostage()
    {
        countHostageFree++;
        countHostageFreeText.text = countHostageFree.ToString();
    }
    private void AddEnemy()
    {
        countKillEnemy++;
        countKillEnemyText.text = countKillEnemy.ToString();
    }
    private void AddBomb()
    {
        countNeutralizeBomb++;
        countNeutralizeBombText.text = countNeutralizeBomb.ToString();
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
