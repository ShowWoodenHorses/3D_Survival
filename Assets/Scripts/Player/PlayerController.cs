
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour, IDamagable
{
    private Rigidbody _rb;
    private Collider _collider;
    private Animator _anim;
    [SerializeField] private float _speedMove;
    [SerializeField] private float _speedRotate;
    [SerializeField] private float _jumpForce;

    [SerializeField] private GameObject _bloodPrefab;

    [SerializeField] private Transform _groundCheckerTransform;
    [SerializeField] private LayerMask _layerMask;

    [SerializeField] private Transform _bulletPos;
    [SerializeField] private float _offset;

    private bool _isPlayerDeath = false;

    private Camera _mainCamera;

    public delegate void PlayerAction();
    public static PlayerAction playerDie;

    [SerializeField] private int _startHealth;
    private int _health;

    public int Health
    {
        get
        {
            return _health;
        }
        set
        {
            _health = value;
            if (_health <= 0)
            {
                _health = 0;
                Die();
            }
        }
    }

    void Start()
    {
        _health = _startHealth;
        _collider = GetComponent<Collider>();
        _rb = GetComponent<Rigidbody>();
        _anim = GetComponent<Animator>();
        _mainCamera = Camera.main;
    }

    private void Update()
    {
        //if (Input.GetKeyDown(KeyCode.Space))
        //{
        //    JumpPlayer();
        //}
    }

    void FixedUpdate()
    {
        if (_isPlayerDeath == false)
        {
            MovementPlayer();
        }
        if (Physics.CheckSphere(_groundCheckerTransform.position,
            0.2f, _layerMask))
        {
            _anim.SetBool("isInAir", false);
        }
        else
        {
            _anim.SetBool("isInAir", true);
        }
    }

    private void RotatePlayer(Transform t, float offset)
    {
        Vector3 mousePosition = Input.mousePosition;
        Vector3 objectPosition = _mainCamera.WorldToScreenPoint(t.position);
        Vector3 direction = mousePosition - objectPosition;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        if (Vector3.Distance(_bulletPos.position, direction) > 75f)
        {
            transform.rotation = Quaternion.Euler(0f, -angle + offset, 0f);
        }
    }

    void MovementPlayer()
    {
        float v = Input.GetAxis("Vertical");
        float h = Input.GetAxis("Horizontal");
        Vector3 direction = new Vector3(-v, 0, h);
        _anim.SetFloat("speed", Vector3.ClampMagnitude(direction, 1).magnitude);
        Vector3 moveDir = Vector3.ClampMagnitude(direction, 1) * _speedMove;
        _rb.velocity = new Vector3(moveDir.x, _rb.velocity.y, moveDir.z);
        _rb.angularVelocity = Vector3.zero;
        if (moveDir.magnitude > 0.02f)
        {
            RotatePlayer(this.transform, 30f);
        }
        else
        {
            RotatePlayer(_bulletPos, 51.5f);
        }
        
    }

    void JumpPlayer()
    {
        if (Physics.Raycast(_groundCheckerTransform.position, Vector3.down,
            0.2f, _layerMask))
        {
            _anim.SetTrigger("Jump");
            _rb.AddForce(Vector3.up.normalized * _jumpForce, ForceMode.Impulse);
        }
    }

    public void TakeDamage(int damage)
    {
        Health -= damage;
        GameObject blood = Instantiate(_bloodPrefab, transform.position, transform.rotation) as GameObject;
        Destroy(blood?.gameObject, 2f);
    }

    public void Die()
    {
        _anim.Play("Death");
        _isPlayerDeath = true;
        playerDie();

    }
}
