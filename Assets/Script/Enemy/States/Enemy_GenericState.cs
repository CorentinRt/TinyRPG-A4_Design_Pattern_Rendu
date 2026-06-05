using UnityEngine;

public class Enemy_GenericState : GenericState<EnemieStates>
{
    #region Fields
    protected Enemy_StateMachine _enemyStateMachine;
    #endregion
    public override EnemieStates GetStateID()
    {
        return EnemieStates.None;
    }


    public override void StateInit(StateMachine<EnemieStates> stateMachine)
    {
        _enemyStateMachine = (Enemy_StateMachine)stateMachine;
        base.StateInit(stateMachine);
    }

    public override void StateEnter(EnemieStates previousState)
    {
        base.StateEnter(previousState);
        _enemyStateMachine.HealthBehaviour.onTakeDamage += OnTakeDamage;
        _enemyStateMachine.HealthBehaviour.onDie += OnDie;
    }

    public override void StateExit(EnemieStates nextState)
    {
        base.StateExit(nextState);
        _enemyStateMachine.HealthBehaviour.onTakeDamage -= OnTakeDamage;
        _enemyStateMachine.HealthBehaviour.onDie -= OnDie;
    }

    public override void StateUpdate(float deltaTime)
    {
        base.StateUpdate(deltaTime);
    }

    protected void OnTakeDamage()
    {
        StateMachine.ChangeState(EnemieStates.TakeDamage);
    }

    protected void OnDie()
    {
        StateMachine.ChangeState(EnemieStates.Dead);
    }

    protected void SearchPlayer()
    {
        if (Vector3.Distance(PlayerController.Instance.transform.position, StateMachine.transform.position) < _enemyStateMachine.Data.DistanceSight)
            StateMachine.ChangeState(EnemieStates.ChasePlayer);
    }
}
