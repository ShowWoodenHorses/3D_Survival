
using UnityEngine;
using UnityEngine.AI;

public class ManGun : EnemyController
{
    [SerializeField] private protected Transform _bulletPos;
    [SerializeField] private protected GameObject _bulletPrefab;

    [SerializeField] private protected ObjectPoolController _poolController;
    [SerializeField] private protected PoolEffectShoot _poolEffectShoot;
    [SerializeField] private protected float _startSpeed;

    [SerializeField] private protected LayerMask _layerMask;

    [SerializeField] private StateMachineEnemy _SM;
    private IdleStateEnemy _idleStateEnemy;
    private RunStateEnemy _runStateEnemy;
    private AttackStateEnemy _attackStateEnemy;
    private DeathStateEnemy _deathStateEnemy;

    [SerializeField] private protected bool _isRange;
    [SerializeField] private protected bool _hit;


    public void Awake()
    {
        _rb = GetComponent<Rigidbody>();
        _collider = GetComponent<Collider>();
        _anim = GetComponent<Animator>();
        _agent = GetComponent<NavMeshAgent>();
        _startSpeed = _agent.speed;
    }

    private void Start()
    {
        _SM = new StateMachineEnemy();
        _idleStateEnemy = new IdleStateEnemy(this, _anim,_agent,_playerPos,_radiusAttack,
      _isRange, _hit, _layerMask);
        _runStateEnemy = new RunStateEnemy(_playerPos, _anim, _agent);
        _attackStateEnemy = new AttackStateEnemy(this, _playerPos,_bulletPos, _poolController,
            _poolEffectShoot, _agent,_isCooldownAttack,_cooldownTime, _startSpeed, _anim);
        _deathStateEnemy = new DeathStateEnemy(this,_agent,_anim,_collider,
            _radiusDetected,_isCooldownAttack);
        {

        };
        _SM.Initialize(_idleStateEnemy);
    }

    public void Initialize(Transform playerPos, ObjectPoolController poolController, PoolEffectShoot poolEffectShoot)
    {
        _playerPos = playerPos;
        _poolController = poolController;
        _poolEffectShoot = poolEffectShoot;
    }

    public void Update()
    {
        _SM.CurreantStateEnemy.Update();
        float distanceToPlayer = Vector3.Distance(transform.position, _playerPos.position);
        _isRange = distanceToPlayer > _radiusAttack;
        _hit = Physics.SphereCast(transform.position, _radiusAttack,
            transform.TransformDirection(Vector3.forward),
            out RaycastHit hitinfo, _layerMask);

        if (_isRange)
        {
            _SM.ChangeState(_runStateEnemy);
        }
        else if (_hit)
        {
            _SM.ChangeState(_attackStateEnemy);
        }
    }
}
