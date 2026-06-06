public class Enemy_MoveState : Enemy_GenericState
{
    public override EnemieStates GetStateID()
    {
        return EnemieStates.Move;
    }

    public override void StateInit(StateMachine<EnemieStates> stateMachine)
    {
        base.StateInit(stateMachine);
    }
    public override void StateEnter(EnemieStates previousState)
    {
        base.StateEnter(previousState);
        _enemyStateMachine.MovementBehaviour.Move();

        _enemyStateMachine.Rewind.onSetEnableRewindEntity += OnReceiveSetEnableRewind;
    }

    public override void StateExit(EnemieStates nextState)
    {
        base.StateExit(nextState);

        _enemyStateMachine.Rewind.onSetEnableRewindEntity -= OnReceiveSetEnableRewind;
    }

    public override void StateUpdate(float deltaTime)
    {
        base.StateUpdate(deltaTime);
        if (_enemyStateMachine.Agent.velocity.magnitude >= 0)
            StateMachine.ChangeState(EnemieStates.Idle);

        SearchPlayer();
    }


    private void OnReceiveSetEnableRewind(bool enable)
    {
        if (!enable)
            return;

        StateMachine.ChangeState(EnemieStates.Rewind);
    }
}