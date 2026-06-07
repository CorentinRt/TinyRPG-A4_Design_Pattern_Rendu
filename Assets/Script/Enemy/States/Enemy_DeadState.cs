public class Enemy_DeadState : Enemy_GenericState
{

    public override EnemieStates GetStateID()
    {
        return EnemieStates.Dead;
    }
    public override void StateEnter(EnemieStates previousState)
    {
        _enemyStateMachine.Rewind.onSetEnableRewindEntity += OnReceiveSetEnableRewind;
    }

    public override void StateExit(EnemieStates nextState)
    {
        _enemyStateMachine.Rewind.onSetEnableRewindEntity -= OnReceiveSetEnableRewind;
    }


    private void OnReceiveSetEnableRewind(bool enable)
    {
        if (!enable)
            return;

        StateMachine.ChangeState(EnemieStates.Rewind);
    }
}
