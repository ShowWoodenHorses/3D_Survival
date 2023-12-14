using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class ManGun : EnemyController
{
    [SerializeField] private Transform _bulletPos;
    [SerializeField] private GameObject _bulletPrefab;

    [SerializeField] private ObjectPoolController _poolController;
    [SerializeField] private PoolEffectShoot _poolEffectShoot;
    private float _startSpeed;

    [SerializeField] private LayerMask _layerMask;
    public void Awake()
    {
        _rb = GetComponent<Rigidbody>();
        _collider = GetComponent<Collider>();
        _anim = GetComponent<Animator>();
        _agent = GetComponent<NavMeshAgent>();
        _startSpeed = _agent.speed;
    }

    public void Initialize(Transform playerPos, ObjectPoolController poolController, PoolEffectShoot poolEffectShoot)
    {
        _playerPos = playerPos;
        _poolController = poolController;
        _poolEffectShoot = poolEffectShoot;
    }

    public void Update()
    {
        _anim.SetFloat("speed", _agent.velocity.magnitude);
        float distanceToPlayer = Vector3.Distance(transform.position, _playerPos.position);
        bool isRange = distanceToPlayer > _radiusAttack;
        bool hit = Physics.SphereCast(transform.position, _radiusAttack,
            transform.TransformDirection(Vector3.forward),
            out RaycastHit hitinfo, _layerMask);

        if (isRange && !_agent.isStopped && Health > 0)
        {
            _anim.SetBool("isAttack", false);
            Move();
        }
        else if (hit && !_agent.isStopped && !_isCooldownAttack && Health > 0)
        {
            _agent.speed = 0f;
            Vector3 direction = _playerPos.position - transform.position;
            transform.rotation = Quaternion.LookRotation(direction);
            _anim.SetBool("isAttack", true);
            StartCoroutine(Shoot());
        }
    }

    public void Move()
    {
        _agent.destination = _playerPos.position;
    }

    private IEnumerator Shoot()
    {
        _isCooldownAttack = true;
        yield return new WaitForSeconds(_cooldownTime);
        GameObject bullet = _poolController.GetObjectFromPool();
        if (bullet != null)
        {
            BulletController bulletController = bullet.GetComponent<BulletController>();
            if (bulletController != null)
            {
                bulletController.Initialize(_poolController, _bulletPos.forward);
            }
            bullet.transform.position = _bulletPos.position;
            bullet.transform.rotation = transform.rotation;
            bullet.SetActive(true);
        }
        GameObject effectShoot = _poolEffectShoot.GetObjectFromPool();
        if (effectShoot != null)
        {
            EffectShoot effect = effectShoot.GetComponent<EffectShoot>();
            if (effect != null)
            {
                effect.Initialize(_poolEffectShoot);
            }
            effectShoot.transform.position = _bulletPos.position;
            effectShoot.transform.rotation = _bulletPos.rotation;
            effectShoot.SetActive(true);
        }
        _agent.speed = _startSpeed;
        _isCooldownAttack = false;
        _agent.isStopped = false;
    }
}
