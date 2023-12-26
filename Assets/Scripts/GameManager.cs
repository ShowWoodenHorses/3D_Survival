using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private Transform _playerTransform;
    [SerializeField] private ContainerPositions _containerPositionsForPatrul;
    [SerializeField] private ObjectPoolController _poolController;
    [SerializeField] private PoolEffectShoot _poolEffectShoot;
    [SerializeField] private ContainerHostage _containerHostage;
    [SerializeField] private ContainerBomb _containerBomb;

    //Enemy
    [SerializeField] private GameObject _enemyKnife;
    [SerializeField] private GameObject _enemyGun;

    //Positions
    public List<Transform> positionsEnemyKnife = new List<Transform>();
    public List<Transform> positionsEnemyGun = new List<Transform>();

    private void Start()
    {
        _playerTransform.GetComponent<CommandController>().Initialize(_containerHostage, _containerBomb);

        for (int i = 0; i < positionsEnemyKnife.Count; i++)
        {
            ManKnife manKnife = _enemyKnife.GetComponent<ManKnife>();
            manKnife?.Initialize(_playerTransform, _containerPositionsForPatrul);
            Instantiate(_enemyKnife, positionsEnemyKnife[i].position, Quaternion.identity);
        }

        for (int i = 0; i < positionsEnemyGun.Count; i++)
        {
            ManGun manGun = _enemyGun.GetComponent<ManGun>();
            manGun?.Initialize(_playerTransform, _poolController, _poolEffectShoot);
            Instantiate(_enemyGun, positionsEnemyGun[i].position, Quaternion.identity);
        }

        for (int i = 0; i < _containerHostage.listHostage.Count; i++)
        {
            _containerHostage.listHostage[i].GetComponent<Hostage>().Initialize(_playerTransform);
        }
    }
}
