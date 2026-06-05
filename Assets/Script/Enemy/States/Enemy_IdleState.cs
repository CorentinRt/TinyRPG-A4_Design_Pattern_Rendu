public class Enemy_IdleState : Enemy_GenericState
{
    public override EnemieStates GetStateID()
    {
        return EnemieStates.Idle;
    }
    public override void StateEnter(EnemieStates previousState)
    {
        base.StateEnter(previousState);
        _enemyStateMachine.MovementBehaviour.Idle();
        _enemyStateMachine.MovementBehaviour.onMove += StartMove;
    }

    public override void StateUpdate(float deltaTime)
    {
        base.StateUpdate(deltaTime);
        SearchPlayer();
    }

    public override void StateExit(EnemieStates nextState)
    {
        base.StateExit(nextState);
        _enemyStateMachine.MovementBehaviour.onMove -= StartMove;
    }

    public void StartMove()
    {
        StateMachine.ChangeState(EnemieStates.Move);
    }
}
