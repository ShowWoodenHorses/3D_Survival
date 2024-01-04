using UnityEngine;
using System.Collections;
public class MeleeAttackStateEnemy : StateEnemy
{
    private readonly ManKnife _manKnife;
    private readonly Animator _anim;
    private readonly Transform _playerPos;
    private readonly float _cooldownTime;
    private readonly int _damage;
    private bool _isCooldownAttack = false;

    public MeleeAttackStateEnemy(ManKnife manKnife, Animator anim, Transform playerPos, 
        float colldownTime, int damage)
    {
        _manKnife = manKnife;
        _anim = anim;
        _playerPos = playerPos;
        _cooldownTime = colldownTime;
        _damage = damage;
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
        if (!_isCooldownAttack)
        {
            _anim.SetTrigger("Attack");
            _manKnife.StartCoroutine(Shoot());
        }
    }

    private IEnumerator Shoot()
    {
        _isCooldownAttack = true;
        _playerPos.GetComponent<IDamagable>().TakeDamage(_damage);
        yield return new WaitForSeconds(_cooldownTime);
        _isCooldownAttack = false;
    }
}
