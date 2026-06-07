using UnityEngine;
using UnityEngine.AI;

public enum EnemieStates
{
    None,
    Idle,
    Move,
    ChasePlayer,
    Attack,
    TakeDamage,
    Dead,
    Rewind
}

public class Enemy_StateMachine : StateMachine<EnemieStates>
{
    #region Fields
    [Header("Datas")]
    [SerializeField] private SO_EnemyData _data;

    [Header("Navigation")]
    [SerializeField] private NavMeshAgent _agent;

    [Header("Behaviours")]
    [SerializeField] private HealthBehaviour _healthBehaviour;
    [SerializeField] private Enemy_MovementBehaviour _movementBehaviour;
    [SerializeField] private Enemy_AttackBehaviour _attackBehaviour;
    [SerializeField] private RewindCommandEntity _rewind;


    #endregion

    #region Properties
    public SO_EnemyData Data { get => _data; }
    public NavMeshAgent Agent { get => _agent; }
    public HealthBehaviour HealthBehaviour { get => _healthBehaviour; }
    public Enemy_MovementBehaviour MovementBehaviour { get => _movementBehaviour; }
    public Enemy_AttackBehaviour AttackBehaviour { get => _attackBehaviour; }
    public RewindCommandEntity Rewind => _rewind;

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
            case (EnemieStates.ChasePlayer):
                state = new Enemy_ChasePlayerState();
                break;
            case (EnemieStates.Rewind):
                state = new Enemy_RewindState();
                break;
            default:
                Debug.LogError("ERROR : Missing state class in Enemy state machine");
                break;
        }

        if (state == null)
        {
            Debug.LogError(" ERROR : State doesn't exist");
            return;
        }

        AddState(state);
    }
}