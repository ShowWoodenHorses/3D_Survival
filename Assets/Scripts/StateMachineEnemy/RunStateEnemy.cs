using UnityEngine;
using UnityEngine.AI;
public class RunStateEnemy : StateEnemy
{
    private Transform _playerPos;
    private Animator _anim;
    private NavMeshAgent _agent;

    public RunStateEnemy(Transform playerPos, Animator anim, NavMeshAgent agent)
    {
        _playerPos = playerPos;
        _anim = anim;
        _agent = agent;
    }

    public override void Enter()
    {
        base.Enter();
    }
    public override void Exit()
    {
        base.Exit();
    }

    public override void Update()
    {
        _anim.SetFloat("speed", _agent.velocity.magnitude);
        _agent.destination = _playerPos.position;
    }
}
