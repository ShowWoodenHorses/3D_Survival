using System.Collections.Generic;
using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class RandomManager : MonoBehaviour
{
    [SerializeField] Transform Player;
    [SerializeField] private GameObject _bombObj;
    [SerializeField] private GameObject _Hostage;
    [SerializeField] private List<Transform> spawnsPointBomb = new List<Transform>();
    [SerializeField] private List<Transform> spawnsPointHostage = new List<Transform>();
    private GameObject bombObj;
    private GameObject HostageObj;

    public float timer = 10f;
    private float Timer 
    {  get 
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
        Timer -= Time.deltaTime;
    }

    void SpawnBomb()
    {
        _bombObj.GetComponent<ArrowDirection>().Initialize(Player);
        Instantiate(_bombObj, spawnsPointBomb[Random.Range(0, spawnsPointBomb.Count)].position,
            _bombObj.transform.rotation);
        GameObject bomb = FindObjectOfType<Bomb>().gameObject;
        Player.GetComponent<CommandController>().AddBombToPlayer(bomb);
    }

    void SpawnHostage()
    {
        _Hostage.GetComponent<ArrowDirection>().Initialize(Player);
        _Hostage.GetComponent<Hostage>().Initialize(Player);
        Instantiate(_Hostage, spawnsPointHostage[Random.Range(0, spawnsPointHostage.Count)].position,
            Quaternion.identity);
        GameObject hostage = FindObjectOfType<Hostage>().gameObject;
        Player.GetComponent<CommandController>().AddHostageToPlayer(hostage);
    }
}