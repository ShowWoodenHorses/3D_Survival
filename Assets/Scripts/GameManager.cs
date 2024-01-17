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

    [SerializeField] private int _countBomb;

    //Enemy
    [SerializeField] private GameObject _enemyKnife;
    [SerializeField] private GameObject _enemyGun;

    //Positions
    public List<Transform> positionsEnemyKnife = new List<Transform>();
    public List<Transform> positionsEnemyGun = new List<Transform>();
    public List<Transform> positionsBomb = new List<Transform>();

    private void Awake()
    {
        if (_containerBomb != null)
            _countBomb = _containerBomb.listBomb.Count;
        _playerTransform.GetComponent<CommandController>().Initialize(_containerHostage, _containerBomb);

        for (int i = 0; i < positionsEnemyKnife.Count; i++)
        {
            ManKnife manKnife = _enemyKnife.GetComponent<ManKnife>();
            manKnife.GetComponent<EnemyController>().ChangeRadiusDetected(3f);
            manKnife?.Initialize(_playerTransform, _containerPositionsForPatrul);
            Instantiate(_enemyKnife, positionsEnemyKnife[i].position, positionsEnemyKnife[i].transform.rotation);
        }

        for (int i = 0; i < positionsEnemyGun.Count; i++)
        {
            ManGun manGun = _enemyGun.GetComponent<ManGun>();
            manGun.GetComponent<EnemyController>().ChangeRadiusDetected(5f);
            manGun?.Initialize(_playerTransform, _poolController, _poolEffectShoot);
            Instantiate(_enemyGun, positionsEnemyGun[i].position, Quaternion.identity);
        }
        if (_countBomb != 0)
        {
            for (int i = 0; i < _countBomb; i++)
            {
                int randomIndex = Random.Range(0, positionsBomb.Count);
                _containerBomb.listBomb[i].transform.position = positionsBomb[randomIndex].position;
                positionsBomb.Remove(positionsBomb[randomIndex]);
            }
        }

        for (int i = 0; i < _containerHostage.listHostage.Count; i++)
        {
            _containerHostage.listHostage[i].GetComponent<Hostage>().Initialize(_playerTransform);
        }
    }
}
