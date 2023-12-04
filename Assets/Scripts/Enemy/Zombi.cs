using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Zombi : EnemyController
{

    [SerializeField] private List<Transform> _positionsForPatrul = new List<Transform>();
    private int _indexMoveForPatrul = 0;

    private ContainerPositions _containerPositions;

    public void Awake()
    {
        _rb = GetComponent<Rigidbody>();
        _collider = GetComponent<Collider>();
        _playerPos = FindObjectOfType<PlayerController>().transform;
        _anim = GetComponent<Animator>();
        _agent = GetComponent<NavMeshAgent>();
        _containerPositions = FindObjectOfType<ContainerPositions>();
    }

    private void Start()
    {
        for (int i = 0; i < _containerPositions.ContainerWithPositionsForPatrul.Count; i++)
        {
            _positionsForPatrul.Add(_containerPositions.ContainerWithPositionsForPatrul[i]);
        }
        _agent.destination = _positionsForPatrul[Random.Range(0, _positionsForPatrul.Count)].position;
    }

    public void Update()
    {
        if (Vector3.Distance(transform.position, _playerPos.position) <= _radiusDetected
            && Vector3.Distance(transform.position, _playerPos.position) >= _radiusAttack 
                && _agent.isStopped == false)
        {
            _anim.SetBool("Walk", false);
            _anim.SetFloat("speed", _agent.velocity.magnitude);
            Move();
        }
        else if (Vector3.Distance(transform.position, _playerPos.position) > _radiusDetected 
            && _agent.isStopped == false)
        {
            _anim.SetBool("Walk", true);
            Patrul();
        }
        else if (Vector3.Distance(transform.position, _playerPos.position) < _radiusAttack
            && _agent.isStopped == false)
        {
            Attack();
            _anim.SetTrigger("Attack");
        }
    }

    public void Move()
    {
        _agent.destination = _playerPos.position;
    }

    void Patrul()
    {
        if (_agent.remainingDistance < 1f) 
        {
            _indexMoveForPatrul = Random.Range(0, _positionsForPatrul.Count);
            _agent.destination = _positionsForPatrul[_indexMoveForPatrul].position;
        }
    }

    public override void Attack()
    {
        StartCoroutine(Shoot());
    }

    private IEnumerator Shoot()
    {
        _isCooldownAttack = true;
        yield return new WaitForSeconds(_cooldownTime);
        _playerPos.GetComponent<IDamagable>().TakeDamage(_damage);
        _isCooldownAttack = false;
    }
}
