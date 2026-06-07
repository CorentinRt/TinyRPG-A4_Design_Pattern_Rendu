using UnityEngine;

public class Enemy_ChasePlayerState : Enemy_GenericState
{
    public override EnemieStates GetStateID()
    {
        return EnemieStates.ChasePlayer;
    }

    public override void StateEnter(EnemieStates previousState)
    {
        base.StateEnter(previousState);

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

        if (PlayerController.Instance == null)
            return;

        Vector3 enemyToPlayer = (PlayerController.Instance.transform.position - StateMachine.transform.position).normalized;

        //Chase player
        _enemyStateMachine.MovementBehaviour.MoveTo(PlayerController.Instance.transform.position - (enemyToPlayer * _enemyStateMachine.Data.DistAttackPlayer / 2f));

        //Calculate distance player enemy
        float distEnemyPlayer = Vector3.Distance(PlayerController.Instance.transform.position, StateMachine.transform.position);

        //Check if enemy can attack player
        if (distEnemyPlayer < _enemyStateMachine.Data.DistAttackPlayer)
            StateMachine.ChangeState(EnemieStates.Attack);

        //Check if player is too far to follow him
        if (distEnemyPlayer > _enemyStateMachine.Data.DistanceLooseSight)
            StateMachine.ChangeState(EnemieStates.Idle);

    }


    private void OnReceiveSetEnableRewind(bool enable)
    {
        if (!enable)
            return;

        StateMachine.ChangeState(EnemieStates.Rewind);
    }
}
