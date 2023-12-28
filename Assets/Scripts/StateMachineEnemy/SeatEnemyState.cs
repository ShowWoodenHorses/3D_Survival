using UnityEngine;

public class SeatEnemyState : StateEnemy
{
    private Animator _anim;

    public SeatEnemyState(Animator anim)
    {
        _anim = anim;
    }

    public override void Enter()
    {
        base.Enter();
    }
    public override void Exit()
    {
        base.Exit();
    }

    public override void Update()
    {
        _anim.Play("Seat");
    }
}
