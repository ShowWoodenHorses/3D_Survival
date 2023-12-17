using UnityEngine;
using UnityEngine.AI;

public class IdleStateEnemy : StateEnemy
{
    private ManGun _manGun;
    private Animator _anim;
    private NavMeshAgent _agent;
    private Transform _playerPos;
    private float _radiusAttack;
    private bool _isRange;
    private bool _hit;
    private LayerMask _layerMask;

    public IdleStateEnemy(ManGun manGun, Animator anim, NavMeshAgent agent, Transform playerPos, float radiusAttack,
        bool isRange, bool hit, LayerMask layerMask)
    {
        _manGun = manGun;
        _anim = anim;
        _agent = agent;
        _playerPos = playerPos;
        _radiusAttack = radiusAttack;
        _isRange = isRange;
        _hit = hit;
        _layerMask = layerMask;

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
        Debug.Log("Update Idle");
        _anim.SetFloat("speed", _agent.velocity.magnitude);
    }
}
