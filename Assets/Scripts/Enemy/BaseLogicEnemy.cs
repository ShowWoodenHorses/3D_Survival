using UnityEngine;

public class BaseLogicEnemy : MonoBehaviour
{
    private StateMachineEnemy _SM;
    private IdleStateEnemy _idleStateEnemy;
    private RunStateEnemy _runStateEnemy;
    private AttackStateEnemy _attackStateEnemy;

    //private void Start()
    //{
    //    _SM = new StateMachineEnemy();
    //    _idleStateEnemy = new IdleStateEnemy();
    //    _runStateEnemy = new RunStateEnemy();
    //    _attackStateEnemy = new AttackStateEnemy();
    //    _SM.Initialize(_idleStateEnemy);
    //}

    //private void Update()
    //{
    //    if (Input.GetKeyDown(KeyCode.I))
    //    {
    //        _SM.ChangeState(_idleStateEnemy);
    //    }
    //    else if (Input.GetKeyDown(KeyCode.R))
    //    {
    //        _SM.ChangeState(_runStateEnemy);
    //    }
    //    else if (Input.GetKeyDown(KeyCode.A))
    //    {
    //        _SM.ChangeState(_attackStateEnemy);
    //    }
    //}
}
