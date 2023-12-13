using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;

public class Hostage : MonoBehaviour, IDamagable
{
    public bool isFollow = false;

    [SerializeField] private protected Rigidbody _rb;
    [SerializeField] private protected Collider _collider;
    [SerializeField] private protected Transform _playerPos;
    [SerializeField] private protected Animator _anim;
    [SerializeField] private protected NavMeshAgent _agent;
    [SerializeField] private protected GameObject _bloodPrefab;

    public delegate void hostageActionZone();
    public delegate void hostageActionDie();
    public static hostageActionZone hostageZone;
    public static hostageActionDie hostageDie;

    [SerializeField] private int _startHealth;
    private protected int _health;
    public int Health
    {
        get
        {
            return _health;
        }
        set
        {
            _health = value;
            if (_health <= 0)
            {
                _health = 0;
                Die();
            }
        }
    }

    private void Awake()
    {
        _rb = GetComponent<Rigidbody>();
        _collider = GetComponent<Collider>();
        _anim = GetComponent<Animator>();
        _agent = GetComponent<NavMeshAgent>();
    }

    public void Initialize(Transform playerTransform)
    {
        _playerPos = playerTransform;
    }

    private void Start()
    {
        _agent.isStopped = true;
    }

    private void Update()
    {
        _anim.SetFloat("speed", _agent.velocity.magnitude);
        if (isFollow == true)
        {
            _agent.isStopped = false;
            _agent.destination = _playerPos.position;
        }
    }

    public void EnemyInHostageZone(Collider other)
    {
        if (other.GetComponent<HostageZoneTrigger>())
        {
            _agent.isStopped = true;
            isFollow = false;
            hostageZone();
            _collider.enabled = false;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        EnemyInHostageZone(other);
    }

    public void TakeDamage(int damage)
    {
        Health -= damage;
        GameObject blood = Instantiate(_bloodPrefab, transform.position, transform.rotation) as GameObject;
        Destroy(blood?.gameObject, 2f);
    }

    public void Die()
    {
        _agent.isStopped = true;
        _agent.speed = 0f;
        _anim.SetTrigger("Death");
        _collider.enabled = false;
        Destroy(gameObject, 2f);
        hostageDie();
    }
}
