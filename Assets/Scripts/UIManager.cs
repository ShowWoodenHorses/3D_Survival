using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour 
{
    public MenuManager menuManager;
    public GameManager gameManager;
    public int countHostageForWin;
    public int countHostageFree;
    public int countStartEnemy;
    public int countKillEnemy;

    //Text
    [SerializeField] Text countHostageText;
    [SerializeField] Text countHostageFreeText;
    [SerializeField] Text countStartEnemyText;
    [SerializeField] Text countKillEnemyText;


    private void OnEnable()
    {
        Hostage.hostageZone += AddHostage;
        EnemyController.enemyDie += AddEnemy;
    }

    private void OnDisable()
    {
        Hostage.hostageZone -= AddHostage;
        EnemyController.enemyDie -= AddEnemy;
    }
    private void Start()
    {
        countHostageForWin = menuManager.countHostageForWin;
        countStartEnemy = gameManager.positionsEnemyGun.Count + gameManager.positionsEnemyKnife.Count;
        countHostageText.text = countHostageForWin.ToString();
        countStartEnemyText.text = countStartEnemy.ToString();
    }

    private void Update()
    {
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
}
