using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class ManGun : EnemyController
{
    [SerializeField] private Transform _bulletPos;
    [SerializeField] private GameObject _bulletPrefab;

    private float _startSpeed;

    [SerializeField] private LayerMask _layerMask;
    public void Awake()
    {
        _rb = GetComponent<Rigidbody>();
        _collider = GetComponent<Collider>();
        _playerPos = FindObjectOfType<PlayerController>().transform;
        _anim = GetComponent<Animator>();
        _agent = GetComponent<NavMeshAgent>();
        _startSpeed = _agent.speed;
    }

    public void FixedUpdate()
    {
        _anim.SetFloat("speed", _agent.velocity.magnitude);
        if (Vector3.Distance(transform.position, _playerPos.position) <= _radiusDetected
            && Vector3.Distance(transform.position, _playerPos.position) >= _radiusAttack
                && _agent.isStopped == false)
        {
            Move();
            _anim.SetBool("isAttack", false);
        }
        else if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward),
                _radiusAttack, _layerMask)
            && _agent.isStopped == false
            && _isCooldownAttack == false)
        {
            _agent.speed = 0f;
            _anim.SetBool("isAttack", true);
            StartCoroutine(Shoot());
        }
    }

    public void Move()
    {
        _agent.destination = _playerPos.position;
    }

    public override void Attack()
    {
        StartCoroutine(Shoot());
    }

    private IEnumerator Shoot()
    {
        _isCooldownAttack = true;
        yield return new WaitForSeconds(_cooldownTime);
        if (Health > 0)
        {
            Instantiate(_bulletPrefab, _bulletPos.position,
                transform.rotation).GetComponent<Rigidbody>().AddForce(_bulletPos.forward * 1000f);
        }
        _agent.speed = _startSpeed;
        _isCooldownAttack = false;
        _agent.isStopped = false;
    }
}
