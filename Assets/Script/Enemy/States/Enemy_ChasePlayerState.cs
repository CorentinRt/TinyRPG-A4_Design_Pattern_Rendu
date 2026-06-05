using UnityEngine;

public class Enemy_ChasePlayerState : Enemy_GenericState
{
    public override EnemieStates GetStateID()
    {
        return EnemieStates.ChasePlayer;
    }

    public override void StateUpdate(float deltaTime)
    {
        base.StateUpdate(deltaTime);
        if (PlayerController.Instance == null) return;
        //Chase player
        _enemyStateMachine.MovementBehaviour.MoveTo(PlayerController.Instance.transform.position);

        //Calculate distance player enemy
        float distEnemyPlayer = Vector3.Distance(PlayerController.Instance.transform.position, StateMachine.transform.position);

        //Check if enemy can attack player
        if (distEnemyPlayer < _enemyStateMachine.Data.DistAttackPlayer)
            StateMachine.ChangeState(EnemieStates.Attack);

        //Check if player is too far to follow him
        if (distEnemyPlayer > _enemyStateMachine.Data.DistanceLooseSight)
            StateMachine.ChangeState(EnemieStates.Idle);

    }
}
