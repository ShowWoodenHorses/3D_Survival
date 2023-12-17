
using UnityEngine;
using UnityEngine.AI;

public abstract class EnemyController : MonoBehaviour, IDamagable
{
    [SerializeField] private protected float _speedMove;
    [SerializeField] private protected float _radiusDetected;
    [SerializeField] private protected float _radiusAttack;
    [SerializeField] private protected bool _isCooldownAttack = false;
    [SerializeField] private protected float _cooldownTime;
    [SerializeField] private protected int _damage;

    [SerializeField] private protected GameObject _bloodPrefab;

    [SerializeField] private protected Rigidbody _rb;
    [SerializeField] private protected Collider _collider;
    [SerializeField] private protected Transform _playerPos;
    [SerializeField] private protected Animator _anim;
    [SerializeField] private protected NavMeshAgent _agent;

    public delegate void EnemyAction();
    public static EnemyAction enemyDie;

    [SerializeField] private int _startHealth;
    [SerializeField] private protected int _health;
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

    void Start()
    {
        _health = _startHealth;
    }

    public void Die()
    {
        _agent.isStopped = true;
        _agent.speed = 0f;
        _radiusDetected = 0f;
        _isCooldownAttack = true;
        _anim.SetTrigger("Death");
        _collider.enabled = false;
        Destroy(gameObject, 2f);
        enemyDie();
    }

    public void TakeDamage(int damage)
    {
        Debug.Log("Damage");
        Health -= damage;
        GameObject blood = Instantiate(_bloodPrefab, transform.position, transform.rotation) as GameObject;
        Destroy(blood?.gameObject, 2f);
    }

}
