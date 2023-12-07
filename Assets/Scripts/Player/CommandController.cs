
using System.Collections.Generic;
using UnityEngine;

public class CommandController : MonoBehaviour
{
    [SerializeField] private float _commandDistance;
    [SerializeField] private LayerMask _layerMask;

    [SerializeField] private List<GameObject> _hostagesObj = new List<GameObject>();
    private ContainerHostage _containerHostage;

    public void Initialize(ContainerHostage containerHostage)
    {
        _containerHostage = containerHostage;
    }
    private void Start()
    {
        for (int i = 0; i < _containerHostage.listHostage.Count; i++)
        {
            _hostagesObj.Add(_containerHostage.listHostage[i]);
        }
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            CallHostage();
        }
    }

    public void CallHostage()
    {
        for (int i = 0; i < _hostagesObj.Count; i++)
        {
            if (Vector3.Distance(transform.position, _hostagesObj[i].transform.position) < _commandDistance)
            {
                _hostagesObj[i].GetComponent<Hostage>().isFollow = true;
            }
        }
    }
}
