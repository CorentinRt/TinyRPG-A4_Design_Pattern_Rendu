public class Enemy_GenericState : GenericState<EnemieStates>
{
    protected Enemy_StateMachine _enemyStateMachine;
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
        TryAttack();
    }

    protected void TryAttack()
    {
        //float distPlayerEnemy = Vector3.Distance(_enemyStateMachine.Player.transform.position, _enemyStateMachine.transform.position);
        //if (_enemyStateMachine.Data.MinDistAttackPlayer > distPlayerEnemy)
        //StateMachine.ChangeState(EnemieStates.Attack);
    }

    protected void OnTakeDamage()
    {
        StateMachine.ChangeState(EnemieStates.TakeDamage);
    }

    protected void OnDie()
    {
        StateMachine.ChangeState(EnemieStates.Dead);
    }
}
