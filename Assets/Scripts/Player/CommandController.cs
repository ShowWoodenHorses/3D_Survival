
using System.Collections.Generic;
using UnityEngine;

public class CommandController : MonoBehaviour
{
    [SerializeField] private float _commandDistanceHostage;
    [SerializeField] private float _commandDistanceBomb;
    [SerializeField] private LayerMask _layerMask;

    public List<GameObject> _hostagesObj = new List<GameObject>();
    public List<GameObject> _bombsObj = new List<GameObject>();
    private ContainerHostage _containerHostage;
    private ContainerBomb _containerBomb;

    public void Initialize(ContainerHostage containerHostage, ContainerBomb containerBomb)
    {
        _containerHostage = containerHostage;
        _containerBomb = containerBomb;
    }
    private void Start()
    {
        for (int i = 0; i < _containerHostage.listHostage.Count; i++)
        {
            _hostagesObj.Add(_containerHostage.listHostage[i]);
        }
        if (_containerBomb != null)
        {
            for (int i = 0; i < _containerBomb.listBomb.Count; i++)
            {
                _bombsObj.Add(_containerBomb.listBomb[i]);
            }
        }

    }

    public void AddBombToPlayer(GameObject obj)
    {
        _bombsObj.Add(obj);
    }
    public void AddHostageToPlayer(GameObject obj)
    {
        _hostagesObj.Add(obj);
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
        for (int i = 0; i < _bombsObj.Count; i++)
        {
            if (Vector3.Distance(transform.position, _bombsObj[i].transform.position) < _commandDistanceBomb)
            {
                _bombsObj[i].GetComponent<Bomb>().isActiveBomb = false;
                _bombsObj.Remove(_bombsObj[i]);
            }
        }
    }
}
