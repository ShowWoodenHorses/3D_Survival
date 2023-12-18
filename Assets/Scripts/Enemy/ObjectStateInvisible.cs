
using UnityEngine;

public class ObjectStateInvisible : MonoBehaviour
{
    private GameObject _roofObject;

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
            Destroy(this.gameObject, 3f);
        }
    }
}
