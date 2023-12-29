using UnityEngine;

public class SeatEnemyState : StateEnemy
{
    private Animator _anim;
    private bool _isSeat;

    public SeatEnemyState(Animator anim, bool isSeat)
    {
        _anim = anim;
        _isSeat = isSeat;
    }

    public override void Enter()
    {
        base.Enter();
        _anim.SetBool("Seat", true);
        _isSeat = true;
    }
    public override void Exit()
    {
        base.Exit();
        _anim.SetBool("Seat", false);
        _isSeat = false;
    }

    public override void Update()
    {
    }
}
