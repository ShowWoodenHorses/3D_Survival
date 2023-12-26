using System;
using UnityEngine;

public class Bomb : MonoBehaviour
{
    [SerializeField] private float _timerBomb;
    [SerializeField] private GameObject _effectExplosion;
    [SerializeField] private GameObject _flickerObj;
    private bool isDestroyBomb = false;
    public bool isActiveBomb;

    public delegate void BombCallback();
    public static BombCallback bombActive;


    private void Start()
    {
        isActiveBomb = true;
        _flickerObj.SetActive(true);
    }
    private void Update()
    {
        if (isActiveBomb && !isDestroyBomb)
        {
            _timerBomb -= Time.deltaTime;
            if (_timerBomb < 0)
            {
                _timerBomb = 0;
                isDestroyBomb = true;
                Explosion();
                bombActive();
            }
        }
        else if (!isActiveBomb)
            _flickerObj.SetActive(false);
    }

    private void Explosion()
    {
        GameObject explosion = Instantiate(_effectExplosion, transform.position, Quaternion.identity) as GameObject;
        Destroy(explosion, 2f);
        Destroy(this.gameObject, 0.1f);
    }
}
