public class StateMachineEnemy
{
    public StateEnemy CurreantStateEnemy {  get; set; }
    
    public void Initialize(StateEnemy curreantStateEnemy)
    {
        CurreantStateEnemy = curreantStateEnemy;
        CurreantStateEnemy.Enter();
    }

    public void ChangeState(StateEnemy newState)
    {
        CurreantStateEnemy.Exit();
        CurreantStateEnemy = newState;
        newState.Enter();
    }
}
