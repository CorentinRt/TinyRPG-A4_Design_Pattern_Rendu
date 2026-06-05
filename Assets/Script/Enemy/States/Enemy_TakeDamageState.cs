public class Enemy_TakeDamageState : Enemy_GenericState
{

    public override EnemieStates GetStateID()
    {
        return EnemieStates.TakeDamage;
    }
    public override void StateEnter(EnemieStates previousState)
    {
        base.StateEnter(previousState);
        StateMachine.ChangeState(previousState);
    }
}
