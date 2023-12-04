
using UnityEngine;

public class BulletController : MonoBehaviour
{
    private float _startPosoitionY;

    [SerializeField] private int _damage;
    void Start()
    {
        _startPosoitionY = transform.position.y;
    }

    void Update()
    {
        transform.position = new Vector3(transform.position.x, _startPosoitionY, transform.position.z);
        Destroy(gameObject, 5f);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Enemy")
            || other.gameObject.CompareTag("Player"))
        {
            other.GetComponent<IDamagable>().TakeDamage(_damage);
            Destroy(this.gameObject);
        }
        if (other.gameObject.CompareTag("Obstacle"))
        {
            Destroy(this.gameObject);
        }
    }
}
