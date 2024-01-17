using System.Collections.Generic;
using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class RandomManager : MonoBehaviour
{
    [SerializeField] Transform Player;
    [SerializeField] private GameObject _BombObj;
    [SerializeField] private GameObject _Hostage;
    [SerializeField] private ContainerPositions _containerPositionsForPatrul;
    [SerializeField] private List<GameObject> enemies = new List<GameObject>();
    [SerializeField] private List<Transform> spawnsPointBomb = new List<Transform>();
    [SerializeField] private List<Transform> spawnsPointHostage = new List<Transform>();
    [SerializeField] private List<Transform> spawnsPointEnemy = new List<Transform>();
    private GameObject bombObj;
    private GameObject HostageObj;


    [SerializeField] private ObjectPoolController _poolController;
    [SerializeField] private PoolEffectShoot _poolEffectShoot;

    public float timer = 10f;
    public float timerEnemy = 2f;
    private float TimerSpawnBombAndHostage
    {
        get
        {
            return timer;
        }
        set
        {
            timer = value;
            if (timer < 0)
            {
                timer = 10f;
                if (bombObj == null)
                {
                    SpawnBomb();
                    bombObj = FindObjectOfType<Bomb>().gameObject;
                }
                if (HostageObj == null)
                {
                    SpawnHostage();
                    HostageObj = FindObjectOfType<Hostage>().gameObject;
                }
            }
        }
    }
    private void Start()
    {
        SpawnBomb();
        SpawnHostage();
        bombObj = FindObjectOfType<Bomb>().gameObject;
        HostageObj = FindObjectOfType<Hostage>().gameObject;
    }

    private void Update()
    {
        TimerSpawnBombAndHostage -= Time.deltaTime;
        timerEnemy -= Time.deltaTime;
        if (timerEnemy < 0)
        {
            timerEnemy = 2f;
            SpawnEnemy();
        }
    }

    void SpawnBomb()
    {
        _BombObj.GetComponent<ArrowDirection>().Initialize(Player);
        int index = Random.Range(0, spawnsPointBomb.Count);
        Instantiate(_BombObj, spawnsPointBomb[index].position,
            _BombObj.transform.rotation);
        GameObject bomb = FindObjectOfType<Bomb>().gameObject;
        Player.GetComponent<CommandController>().AddBombToPlayer(bomb);
    }

    void SpawnHostage()
    {
        _Hostage.GetComponent<ArrowDirection>().Initialize(Player);
        _Hostage.GetComponent<Hostage>().Initialize(Player);
        int index = Random.Range(0, spawnsPointHostage.Count);
        Instantiate(_Hostage, spawnsPointHostage[index].position,
            Quaternion.identity);
        GameObject hostage = FindObjectOfType<Hostage>().gameObject;
        Player.GetComponent<CommandController>().AddHostageToPlayer(hostage);
    }

    void SpawnEnemy()
    {
        int indexEnemy = Random.Range(0, enemies.Count);
        int indexTransform = Random.Range(0, spawnsPointEnemy.Count);
        switch (indexEnemy)
        {
            case 0:
                enemies[indexEnemy].GetComponent<ManKnife>().Initialize(Player,
                    _containerPositionsForPatrul);
                enemies[indexEnemy].GetComponent<EnemyController>().ChangeRadiusDetected(100f);
                break;
            case 1:
                enemies[indexEnemy].GetComponent<ManGun>().Initialize(Player,
                    _poolController, _poolEffectShoot);
                enemies[indexEnemy].GetComponent<EnemyController>().ChangeRadiusDetected(100f);
                break;
            default:
                break;
        }
        Instantiate(enemies[indexEnemy], spawnsPointEnemy[indexTransform].position,
            Quaternion.identity);
    }
}