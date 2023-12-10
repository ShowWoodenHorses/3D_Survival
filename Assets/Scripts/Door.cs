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
        _obstacleNavMesh.enabled = true;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<PlayerController>())
        {
            DoorOpen();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.GetComponent<PlayerController>())
        {
            DoorClose();
        }
    }

    void DoorOpen()
    {
        _doorTransform.rotation = Quaternion.Euler(transform.localRotation.x, _angle, transform.localRotation.z);
        _obstacleNavMesh.enabled = false;
    }

    void DoorClose()
    {
        _doorTransform.rotation = Quaternion.Euler(transform.localRotation.x, _startAngle, transform.localRotation.z);
        _obstacleNavMesh.enabled = true;
    }
}
