using System;
using UnityEngine;
using UnityEngine.UI;

public class Bomb : MonoBehaviour
{
    [SerializeField] private double _timerBomb;
    [SerializeField] private Text _timerText;
    [SerializeField] private GameObject _effectExplosion;
    [SerializeField] private GameObject _flickerObj;
    [SerializeField] private GameObject _timerObj;
    private bool isDestroyBomb = false;
    private bool _bombNeutralize = false;
    public bool isActiveBomb;

    public delegate void BombCallback();
    public static BombCallback bombActive;
    public static BombCallback bombNeutralize;

    private DateTime _timer;
    private void Start()
    {
        _timerObj.SetActive(true);
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
        {
            _timerObj.SetActive(false);
            _flickerObj.SetActive(false);
            if (!_bombNeutralize)
            {
                bombNeutralize();
                _bombNeutralize = true;
            }
        }
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
