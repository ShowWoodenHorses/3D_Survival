using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{

    private Transform _doorTransform;
    [SerializeField] private float _angle;
    [SerializeField] private float _startAngle;

    // Start is called before the first frame update
    void Start()
    {
        _doorTransform = gameObject.transform.GetChild(0);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            _doorTransform.rotation = Quaternion.Euler(transform.localRotation.x, _angle, transform.localRotation.z);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            _doorTransform.rotation = Quaternion.Euler(transform.localRotation.x, _startAngle, transform.localRotation.z);
        }
    }
}
