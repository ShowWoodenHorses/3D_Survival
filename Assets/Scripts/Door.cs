using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Door : MonoBehaviour
{

    private Transform _doorTransform;
    private NavMeshObstacle _obstacleNavMesh;
    [SerializeField] private float _angle;
    [SerializeField] private float _startAngle;

    private void Awake()
    {
        _obstacleNavMesh = GetComponent<NavMeshObstacle>();
    }
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
        if (other.GetComponent<DoorTrigger>())
        {
            DoorOpen();
        }
    }
    private void OnTriggerStay(Collider other)
    {
        if (other.GetComponent<DoorTrigger>())
        {
            DoorOpen();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.GetComponent<DoorTrigger>())
        {
            DoorClose();
        }
    }

    void DoorOpen()
    {
        _doorTransform.rotation = Quaternion.Euler(transform.localRotation.x, _angle, transform.localRotation.z);
    }

    void DoorClose()
    {
        _doorTransform.rotation = Quaternion.Euler(transform.localRotation.x, _startAngle, transform.localRotation.z);
    }
}
