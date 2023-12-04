using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public GameObject GameOverObj;
    public GameObject GameWinObj;
    [SerializeField] private int _countHostageForWin;
    [SerializeField] private int _count;
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

    private void Update()
    {
        if (_count == _countHostageForWin)
        {
            GameWin();
        }
    }

    private void Add()
    {
        _count++;
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
}
