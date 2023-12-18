using UnityEngine;
using UnityEngine.AI;
using System.Collections.Generic;
public class PatrulStateEnemy : StateEnemy
{
    private readonly NavMeshAgent _agent;
    private readonly Animator _anim;
    int _indexMoveForPatrul = 0;

    [SerializeField] private List<Transform> _positionsForPatrul = new List<Transform>();
    [SerializeField] private ContainerPositions _containerPositions;
    public PatrulStateEnemy(NavMeshAgent agent, Animator anim, ContainerPositions containerPositions)
    {
        _agent = agent;
        _anim = anim;
        _containerPositions = containerPositions;
    }

    public void Init()
    {
        for (int i = 0; i < _containerPositions.ContainerWithPositionsForPatrul.Count; i++)
        {
            Debug.Log(_containerPositions.ContainerWithPositionsForPatrul[i]);
            _positionsForPatrul.Add(_containerPositions.ContainerWithPositionsForPatrul[i]);
        }
        _agent.destination = _positionsForPatrul[Random.Range(0, _positionsForPatrul.Count)].position;
    }


    public override void Enter()
    {
        Debug.Log("Enter ManKnife");
        _anim.SetBool("Walk", true);
    }
    public override void Exit()
    {
        base.Exit();
        _anim.SetBool("Walk", false);
    }

    public override void Update()
    {
        if (_agent.remainingDistance < 1f)
        {
            _indexMoveForPatrul = Random.Range(0, _positionsForPatrul.Count);
            _agent.destination = _positionsForPatrul[_indexMoveForPatrul].position;
        }
    }
}
