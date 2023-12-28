using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class ManKnife : EnemyController
{
    [SerializeField] private protected ContainerPositions _containerPositions;

    private protected StateMachineEnemy _SM;
    private protected RunStateEnemy _runStateEnemy;
    private protected PatrulStateEnemy _patrulStateEnemy;
    private protected MeleeAttackStateEnemy _meleeAttackStateEnemy;

    public override void Awake()
    {
        base.Awake();
    }

    public void Initialize(Transform transformPlayer, ContainerPositions containerPositions)
    {
        _containerPositions = containerPositions;
        _playerPos = transformPlayer;
    }

    private void Start()
    {
        _SM = new StateMachineEnemy();
        _runStateEnemy = new RunStateEnemy(_playerPos, _anim, _agent);
        _patrulStateEnemy = new PatrulStateEnemy(_agent,_anim, _containerPositions);
        _meleeAttackStateEnemy = new MeleeAttackStateEnemy(this, _anim, _playerPos,
            _cooldownTime, _damage);
        _SM.Initialize(_patrulStateEnemy);
        _patrulStateEnemy.Init();
    }

    public virtual void Update()
    {
        _SM.CurreantStateEnemy.Update();
        float distanceToPlayer = Vector3.Distance(transform.position, _playerPos.position);
        if (distanceToPlayer <= _radiusDetected && distanceToPlayer >= _radiusAttack 
                && _agent.isStopped == false)
        {
            _SM.ChangeState(_runStateEnemy);
        }
        else if (distanceToPlayer > _radiusDetected && _agent.isStopped == false)
        {
            _SM.ChangeState(_patrulStateEnemy);
        }
        else if (distanceToPlayer < _radiusAttack && !_isCooldownAttack && _agent.isStopped == false)
        {
            _SM.ChangeState(_meleeAttackStateEnemy);
        }
    }
}
