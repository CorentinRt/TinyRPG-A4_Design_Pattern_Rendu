public class Enemy_DeadState : Enemy_GenericState
{

    public override EnemieStates GetStateID()
    {
        return EnemieStates.Dead;
    }
    public override void StateEnter(EnemieStates previousState)
    {

    }

    public override void StateExit(EnemieStates nextState)
    {

    }
}
