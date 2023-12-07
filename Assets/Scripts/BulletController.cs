using System.Collections;
using UnityEngine;

public class BulletController : MonoBehaviour
{
    private ObjectPoolController _poolController;
    private Rigidbody _rb;
    private float _startPosoitionY;
    private Vector3 _position;


    [SerializeField] private int _damage;
    [SerializeField] private float _speedMove;
    public void Initialize(ObjectPoolController objectPoolController, Vector3 v)
    {
        _poolController = objectPoolController;
        _position = v;
    }
    private void Awake()
    {
        _rb = GetComponent<Rigidbody>();
    }

    void Start()
    {
        _startPosoitionY = transform.position.y;
    }

    void Update()
    {
        StartCoroutine(ReturnBulletToPool(this.gameObject));
        transform.position = new Vector3(transform.position.x, _startPosoitionY, transform.position.z);
        _rb.velocity = _position * _speedMove;
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Enemy")
            || other.gameObject.CompareTag("Player"))
        {
            other.GetComponent<IDamagable>().TakeDamage(_damage);
            _poolController.ReturnObjectToPool(this.gameObject);
        }
        if (other.gameObject.CompareTag("Obstacle"))
        {
            _poolController.ReturnObjectToPool(this.gameObject);
        }
    }

    private IEnumerator ReturnBulletToPool(GameObject bullet)
    {
        yield return new WaitForSeconds(5);
        if (bullet != null)
        {
            _poolController.ReturnObjectToPool(bullet);
        }
    }
}
