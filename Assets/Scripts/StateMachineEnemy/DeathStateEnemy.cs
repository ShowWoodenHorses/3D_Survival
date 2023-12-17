using UnityEngine;
using System.Collections;
using UnityEngine.AI;
public class DeathStateEnemy : StateEnemy
{
    private ManGun _manGun;
    private NavMeshAgent _agent;
    private Animator _anim;
    private Collider _collider;
    private float _radiusDetected;
    private bool _isCooldownAttack;


    public DeathStateEnemy(ManGun manGun, NavMeshAgent agent, Animator anim, Collider collider, float radiusDetected, bool isCooldownAttack)
    {
        _manGun = manGun;
        _agent = agent;
        _anim = anim;
        _collider = collider;
        _radiusDetected = radiusDetected;
        _isCooldownAttack = isCooldownAttack;
    }

    public override void Enter()
    {
    }
    public override void Exit() 
    {
        base.Exit();
    }

    public override void Update()
    {
        _agent.isStopped = true;
        _agent.speed = 0f;
        _radiusDetected = 0f;
        _isCooldownAttack = true;
        _anim.SetTrigger("Death");
        _collider.enabled = false;
    }
}
