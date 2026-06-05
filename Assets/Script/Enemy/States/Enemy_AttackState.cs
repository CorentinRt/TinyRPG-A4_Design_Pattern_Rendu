public class Enemy_AttackState : Enemy_GenericState
{
    public override EnemieStates GetStateID()
    {
        return EnemieStates.Attack;
    }


    public override void StateEnter(EnemieStates previousState)
    {
        base.StateEnter(previousState);
        if (_enemyStateMachine.AttackBehaviour.IsInCoolDown() && _enemyStateMachine.AttackBehaviour.IsAttacking)
        {
            StateMachine.ChangeState(previousState);
            return;
        }
        _enemyStateMachine.AttackBehaviour.TriggerNewAttack(_enemyStateMachine.Data.AttackParams[0], 0);
        StateMachine.ChangeState(previousState);
    }
}
