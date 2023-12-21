
using UnityEngine;

public class ObjectStateInvisible : MonoBehaviour
{
    private GameObject _roofObject;

    [SerializeField] private bool _destroyRoof;

    void Start()
    {
        _roofObject = transform.GetChild(0).gameObject;
        _roofObject.SetActive(true);
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<PlayerController>())
        {
            _roofObject.SetActive(false);
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.GetComponent<PlayerController>())
        {
            if (_destroyRoof)
                _roofObject.SetActive(true);
            else if (!_destroyRoof)
                Destroy(this.gameObject, 3f);
        }
    }
}
