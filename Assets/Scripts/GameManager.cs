using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private Transform _playerTransform;
    [SerializeField] private ContainerPositions _containerPositionsForPatrul;
    [SerializeField] private ObjectPoolController _poolController;

    //Enemy
    [SerializeField] private GameObject _enemyKnife;
    [SerializeField] private GameObject _enemyGun;

    //Positions
    [SerializeField] private List<Transform> positionsEnemyKnife = new List<Transform>();
    [SerializeField] private List<Transform> positionsEnemyGun = new List<Transform>();

    private void Start()
    {
        for (int i = 0; i < positionsEnemyKnife.Count; i++)
        {
            ManKnife manKnife = _enemyKnife.GetComponent<ManKnife>();
            manKnife?.Initialize(_playerTransform, _containerPositionsForPatrul);
            Instantiate(_enemyKnife, positionsEnemyKnife[i].position, Quaternion.identity);
        }

        for (int i = 0; i < positionsEnemyGun.Count; i++)
        {
            ManGun manGun = _enemyGun.GetComponent<ManGun>();
            manGun?.Initialize(_playerTransform, _poolController);
            Instantiate(_enemyGun, positionsEnemyGun[i].position, Quaternion.identity);
        }
    }
}
