using UnityEngine;
using UnityEngine.AI;

public enum EnemieStates
{
    None,
    Idle,
    Move,
    Attack,
    TakeDamage,
    Dead
}

public class Enemy_StateMachine : StateMachine<EnemieStates>
{
    #region Fields
    [SerializeField] private SO_EnemyData _data;
    [SerializeField] private HealthBehaviour _healthBehaviour;
    [SerializeField] private NavMeshAgent _agent;
    [SerializeField] private Enemy_MovementBehaviour _movementBehaviour;
    [SerializeField] private Enemy_AttackBehaviour _attackBehaviour;


    #endregion
    #region Properties
    public SO_EnemyData Data { get => _data; }
    public NavMeshAgent Agent { get => _agent; }
    public HealthBehaviour HealthBehaviour { get => _healthBehaviour; }
    public Enemy_MovementBehaviour MovementBehaviour { get => _movementBehaviour; }
    public Enemy_AttackBehaviour AttackBehaviour { get => _attackBehaviour; }

    #endregion

    public override void InitStateMachine()
    {
        base.InitStateMachine();
        ChangeState(EnemieStates.Idle);
    }


    protected override void CreateStateById(EnemieStates id)
    {
        GenericState<EnemieStates> state = null;
        switch (id)
        {
            case (EnemieStates.None):
                state = new Enemy_NoneState();
                break;
            case (EnemieStates.Idle):
                state = new Enemy_IdleState();
                break;
            case (EnemieStates.Move):
                state = new Enemy_MoveState();
                break;
            case (EnemieStates.Attack):
                state = new Enemy_AttackState();
                break;
            case (EnemieStates.TakeDamage):
                state = new Enemy_TakeDamageState();
                break;
            case (EnemieStates.Dead):
                state = new Enemy_DeadState();
                break;
        }

        if (state == null)
        {
            Debug.LogError("State doesn't exist");
            return;
        }
        AddState(state);
    }
}