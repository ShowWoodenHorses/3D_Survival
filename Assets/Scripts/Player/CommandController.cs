
using System.Collections.Generic;
using UnityEngine;

public class CommandController : MonoBehaviour
{
    [SerializeField] private float _commandDistanceHostage;
    [SerializeField] private float _commandDistanceBomb;
    [SerializeField] private LayerMask _layerMask;

    [SerializeField] private List<GameObject> _hostagesObj = new List<GameObject>();
    [SerializeField] private List<GameObject> _bombObj = new List<GameObject>();
    private ContainerHostage _containerHostage;
    private ContainerBomb _containerBomb;

    public void Initialize(ContainerHostage containerHostage, ContainerBomb containerBomb)
    {
        _containerHostage = containerHostage;
        _containerBomb = containerBomb;
    }
    private void Start()
    {
        //for (int i = 0; i < _containerHostage.listHostage.Count; i++)
        //{
        //    _hostagesObj.Add(_containerHostage.listHostage[i]);
        //}

        for (int i = 0; i < _containerBomb.listBomb.Count; i++)
        {
            _bombObj.Add(_containerBomb.listBomb[i]);
        }

    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            CallHostage();
        }
        if (Input.GetKeyDown(KeyCode.F))
        {
            CheckBomb();
        }
    }

    public void CallHostage()
    {
        for (int i = 0; i < _hostagesObj.Count; i++)
        {
            if (Vector3.Distance(transform.position, _hostagesObj[i].transform.position) < _commandDistanceHostage)
            {
                _hostagesObj[i].GetComponent<Hostage>().isFollow = true;
            }
        }
    }

    public void CheckBomb()
    {
        for (int i = 0; i < _bombObj.Count; i++)
        {
            if (Vector3.Distance(transform.position, _bombObj[i].transform.position) < _commandDistanceBomb)
            {
                _bombObj[i].GetComponent<Bomb>().isActiveBomb = false;
            }
        }
    }
}
