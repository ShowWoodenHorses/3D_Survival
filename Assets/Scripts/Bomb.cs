using System;
using UnityEngine;
using UnityEngine.UI;

public class Bomb : MonoBehaviour
{
    [SerializeField] private double _timerBomb;
    [SerializeField] private Text _timerText;
    [SerializeField] private GameObject _effectExplosion;
    [SerializeField] private GameObject _flickerObj;
    private bool isDestroyBomb = false;
    public bool isActiveBomb;

    public delegate void BombCallback();
    public static BombCallback bombActive;

    private DateTime _timer;
    private void Start()
    {
        _timer = DateTime.Now.AddSeconds(_timerBomb);
        isActiveBomb = true;
        _flickerObj.SetActive(true);
    }
    private void Update()
    {
        if (isActiveBomb && !isDestroyBomb)
        {
            TimeSpan delta = _timer - DateTime.Now;
            _timerText.text = str(delta);
            if (delta.TotalSeconds < 0)
            {
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

    private string str(TimeSpan t)
    {
        return t.Minutes + ":" + t.Seconds;
    }
}
