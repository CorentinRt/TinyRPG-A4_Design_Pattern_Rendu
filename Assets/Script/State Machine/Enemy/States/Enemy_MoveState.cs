public class Enemy_MoveState : Enemy_GenericState
{


    public override void StateInit(StateMachine<EnemieStates> stateMachine)
    {
        base.StateInit(stateMachine);
    }
    public override void StateEnter(EnemieStates previousState)
    {
        base.StateEnter(previousState);
        _enemyStateMachine.MovementBehaviour.Move();
    }

    public override void StateUpdate(float deltaTime)
    {
        base.StateUpdate(deltaTime);
        if (_enemyStateMachine.Agent.velocity.magnitude >= 0)
            StateMachine.ChangeState(EnemieStates.Idle);
    }
}