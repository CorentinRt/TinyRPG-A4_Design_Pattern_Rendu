public class Enemy_AttackState : Enemy_GenericState
{
    public override EnemieStates GetStateID()
    {
        return EnemieStates.Attack;
    }


    public override void StateEnter(EnemieStates previousState)
    {
        base.StateEnter(previousState);

        if (_enemyStateMachine.AttackBehaviour.IsInCoolDown() || _enemyStateMachine.AttackBehaviour.IsAttacking)
        {
            StateMachine.ChangeState(previousState);

            return;
        }

        AttackParams attackParam = _enemyStateMachine.Data.AttackParams[0];
        _enemyStateMachine.AttackBehaviour.TriggerNewAttack(attackParam, 0);

        Command_Attack commandAttack = new Command_Attack(_enemyStateMachine.AttackBehaviour, null, attackParam, 0, UnityEngine.Vector3.zero);

        _enemyStateMachine.Rewind.RegisterCommand(commandAttack);

        StateMachine.ChangeState(previousState);
    }
}
