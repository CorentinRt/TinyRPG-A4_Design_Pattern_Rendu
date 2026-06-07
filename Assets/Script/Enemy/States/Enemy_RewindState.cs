using System;
using UnityEngine;

public class Enemy_RewindState : Enemy_GenericState
{
    public override EnemieStates GetStateID()
    {
        return EnemieStates.Rewind;
    }

    public override void StateInit(StateMachine<EnemieStates> stateMachine)
    {
        base.StateInit(stateMachine);

    }

    public override void StateEnter(EnemieStates previousState)
    {
        base.StateEnter(previousState);

        _enemyStateMachine.Agent.enabled = false;

        _enemyStateMachine.Rewind.onSetEnableRewindEntity += OnReceiveSetEnableRewind;

        _enemyStateMachine.HealthBehaviour.SetInvincible(true);
    }

    public override void StateExit(EnemieStates nextState)
    {
        base.StateExit(nextState);

        _enemyStateMachine.Agent.enabled = true;

        _enemyStateMachine.MovementBehaviour.MoveTo(StateMachine.transform.position);

        _enemyStateMachine.Rewind.onSetEnableRewindEntity -= OnReceiveSetEnableRewind;

        _enemyStateMachine.HealthBehaviour.SetInvincible(false);
    }

    public override void StateUpdate(float deltaTime)
    {
        base.StateUpdate(deltaTime);

    }


    private void OnReceiveSetEnableRewind(bool enable)
    {
        if (enable)
            return;

        StateMachine.ChangeState(EnemieStates.Idle);
    }
}
