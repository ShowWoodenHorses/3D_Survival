using UnityEngine;
using System.Collections;
using UnityEngine.AI;
public class AttackStateEnemy : StateEnemy
{
    private ManGun _manGun;
    private Transform _playerPos;
    private Transform _bulletPos;
    private ObjectPoolController _poolController;
    private PoolEffectShoot _poolEffectShoot;
    private NavMeshAgent _agent;
    private bool _isCooldownAttack;
    private float _cooldownTime;
    private float _startSpeed;
    private Animator _anim;

    public AttackStateEnemy(ManGun manGun, Transform playerPos, Transform bulletPos, ObjectPoolController poolController,
        PoolEffectShoot poolEffectShoot, NavMeshAgent agent, bool isCooldown, float cooldownTime, float startSpeed, Animator anim)
    {
        _manGun = manGun;
        _playerPos = playerPos;
        _bulletPos = bulletPos;
        _poolController = poolController;
        _poolEffectShoot = poolEffectShoot;
        _agent = agent;
        _isCooldownAttack = isCooldown;
        _cooldownTime = cooldownTime;
        _startSpeed = startSpeed;
        _anim = anim;
    }
    public override void Enter()
    {
        base.Enter();
        _anim.SetBool("isAttack", true);
    }
    public override void Exit()
    {
        base.Exit();
    }

    public override void Update()
    {
        if (!_isCooldownAttack)
        {
            _agent.speed = 0f;
            Vector3 direction = _playerPos.position - _manGun.transform.position;
            _manGun.transform.rotation = Quaternion.LookRotation(direction);
            if (_manGun.Health >= 0)
            {
                _manGun.StartCoroutine(Shoot());
            }
        }
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
            bullet.transform.rotation = _manGun.transform.rotation;
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
