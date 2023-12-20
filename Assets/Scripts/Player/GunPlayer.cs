using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunPlayer : MonoBehaviour
{
    [SerializeField] private List<GameObject> guns = new List<GameObject>();
    [SerializeField] private Transform _bulletPos;
    [SerializeField] private GameObject _bulletPrefab;
    [SerializeField] private float _speedBullet;

    [SerializeField] private float _cooldown;
    [SerializeField] private bool _isCooldown = false;

    [SerializeField] private float _offset;

    [SerializeField] private ObjectPoolController _poolController;
    [SerializeField] private PoolEffectShoot _poolEffectShoot;

    [SerializeField] private bool _isCheckTriggerWeaponInWall;

    private Animator _anim;
    private void Awake()
    {
        for (int i = 0; i < guns.Count; i++)
        {
            guns[i].gameObject.SetActive(false);
        }
    }
    void Start()
    {
        _anim = GetComponent<Animator>();
    }

    void Update()
    {
        //костыль, чтоб если оружие в стене, то нельзя стрелять
        _isCheckTriggerWeaponInWall = _bulletPos.GetComponent<CheckTriggerWeaponAndObstacle>().isWeaponInObstacle;
        if (Input.GetMouseButtonDown(0) && _isCooldown == false && _isCheckTriggerWeaponInWall)
        {
            Shoot();
        }
    }

    private void Shoot()
    {
        float v = Input.GetAxis("Vertical");
        float h = Input.GetAxis("Horizontal");
        Vector3 direction = new Vector3(-v, 0, h);
        if (direction.magnitude > Mathf.Abs(0.05f))
        {
            _anim.SetTrigger("ShootRun");
        }
        else
        {
            _anim.SetTrigger("ShootStay");
        }
        StartCoroutine(ShootCooldown());
        StartCoroutine(SpawnBullet());
    }

    private IEnumerator ShootCooldown()
    {
        _isCooldown = true;
        yield return new WaitForSeconds(_cooldown);
        _isCooldown = false;
    }

    private IEnumerator SpawnBullet()
    {
        yield return new WaitForSeconds(0.1f);

        GameObject bullet = _poolController.GetObjectFromPool();
        if (bullet != null)
        {
            BulletController bulletController = bullet.GetComponent<BulletController>();
            if (bulletController != null)
            {
                bulletController.Initialize(_poolController, _bulletPos.forward);
            }
            bullet.transform.position = _bulletPos.position;
            bullet.transform.rotation = _bulletPos.rotation;
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
    }
}
