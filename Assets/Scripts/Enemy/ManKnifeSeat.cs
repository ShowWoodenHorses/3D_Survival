using UnityEngine;

public class ManKnifeSeat : ManKnife
{
    private SeatEnemyState _seatEnemyState;
    private void Start()
    {
        _SM = new StateMachineEnemy();
        _seatEnemyState = new SeatEnemyState(_anim);
        _runStateEnemy = new RunStateEnemy(_playerPos, _anim, _agent);
        _meleeAttackStateEnemy = new MeleeAttackStateEnemy(this, _anim, _playerPos,
            _cooldownTime, _damage);
        _SM.Initialize(_seatEnemyState);
    }
    public override void Update()
    {
        _SM.CurreantStateEnemy.Update();
        float distanceToPlayer = Vector3.Distance(transform.position, _playerPos.position);
        if (distanceToPlayer <= _radiusDetected && distanceToPlayer >= _radiusAttack
                && _agent.isStopped == false)
        {
            _SM.ChangeState(_runStateEnemy);
        }
        else if (distanceToPlayer < _radiusAttack && !_isCooldownAttack && _agent.isStopped == false)
        {
            _SM.ChangeState(_meleeAttackStateEnemy);
        }
    }
}
