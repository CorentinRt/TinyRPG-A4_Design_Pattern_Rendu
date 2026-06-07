using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : GenericSingleton<PlayerController>
{
    #region Fields

    [Header("Datas")]
    [SerializeField] private SO_PlayerDatas _datas;

    [Header("Inputs")]
    [SerializeField] private InputActionReference _moveInput;
    [SerializeField] private InputActionReference _attackInput;
    [SerializeField] private InputActionReference _rewindInput;

    [Header("Behaviours")]
    [SerializeField] private PlayerMovementsBehaviour _movements;
    [SerializeField] private PlayerAttackBehaviour _attack;
    [SerializeField] private RewindCommandEntity _rewind;

    [Header("Health")]
    [SerializeField] private HealthBehaviour _health;

    [Header("RigidBody")]
    [SerializeField] private Rigidbody _rb;

    #endregion

    #region Properties
    public HealthBehaviour Health => _health;
    public RewindCommandEntity Rewind => _rewind;

    #endregion

    public event Action<Vector2> onMoveInput;

    protected override void Awake()
    {
        // Move
        _moveInput.action.started += OnReceiveMoveInput;
        _moveInput.action.performed += OnReceiveMoveInput;
        _moveInput.action.canceled += OnReceiveMoveInput;
        _movements.onPlayerApplyMove += OnReceivePlayerApplyMove;

        // Attack
        _attackInput.action.started += OnReceiveAttackInput;
        _attack.onPlayerAttack += OnReceivePlayerAttack;

        // Rewind
        _rewindInput.action.started += OnReceiveRewindInput;
        _rewind.onSetEnableRewindEntity += OnReceiveSetEnableRewind;

        // Heath
        _health.onDie += OnReceivePlayerDie;
        _health.onRevive += OnReceivePlayerRevive;

    }

    private void OnDestroy()
    {
        // Move
        _moveInput.action.started -= OnReceiveMoveInput;
        _moveInput.action.performed -= OnReceiveMoveInput;
        _moveInput.action.canceled -= OnReceiveMoveInput;
        _movements.onPlayerApplyMove -= OnReceivePlayerApplyMove;

        // Attack
        _attackInput.action.started -= OnReceiveAttackInput;
        _attack.onPlayerAttack -= OnReceivePlayerAttack;

        // Rewind
        _rewindInput.action.started -= OnReceiveRewindInput;
        _rewind.onSetEnableRewindEntity -= OnReceiveSetEnableRewind;

        // Heath
        _health.onDie -= OnReceivePlayerDie;
        _health.onRevive -= OnReceivePlayerRevive;

    }

    private void Start()
    {
        InitPlayerController();

    }

    private void InitPlayerController()
    {
        InitMovements();

    }

    private void InitMovements()
    {
        if (_movements != null)
        {
            _movements.InitPlayerMovements(_datas);
        }
        else
        {
            Debug.LogError("Error : No movements behaviour linked to Player Controller ! Movements of player won't work !", this);
        }
    }

    private void OnReceiveMoveInput(InputAction.CallbackContext ctx)
    {
        if (_movements != null)
        {
            Vector2 dir = Vector2.ClampMagnitude(ctx.ReadValue<Vector2>(), 1f);

            _movements.SetMoveDirection(dir);
        }
        else
        {
            Debug.LogError("Error : Try to move but no movements behaviour linked to Player Controller ! Movements of player won't work !", this);
        }
    }

    private void OnReceiveAttackInput(InputAction.CallbackContext ctx)
    {
        if(_attack != null)
        {
            if (CanTriggerAttack())
            {
                _attack.TriggerNewAttack(_datas.Attacks[0], 0);
            }
        }
        else
        {
            Debug.LogError("Error : Try to attack but no attack behaviour linked to Player Controller ! Attack of player won't work !", this);
        }
    }

    private void OnReceiveRewindInput(InputAction.CallbackContext ctx)
    {
        if (_rewind != null)
        {
            if (!_health.IsDead())
            {
                if (RewindCommandManager.Exist)
                {
                    if (RewindCommandManager.Instance.IsRewindEnabled())
                    {
                        RewindCommandManager.Instance.StopRewind();
                    }
                    else
                    {
                        RewindCommandManager.Instance.StartRewind();
                    }
                }
                else
                {
                    Debug.LogError("Error : Try to enable rewind manager but singleton of rewind command manager not found ! Nothing will happen !", this);
                }
            }
        }
        else
        {
            Debug.LogError("Error : Try to rewind but no Player rewind behaviour linked to Player Controller ! rewind of player won't work !", this);
        }
    }

    private void OnReceiveSetEnableRewind(bool enabled)
    {
        UpdateMovementsEnabledState();
        UpdateAttackEnabledState();

        _health.SetInvincible(enabled);
    }

    private void OnReceivePlayerDie()
    {
        UpdateMovementsEnabledState();
        UpdateAttackEnabledState();

        RewindCommandManager.Instance.StartRewindWithDelay(_datas.OnDeathRewindDelay);
    }

    private void OnReceivePlayerRevive()
    {
        UpdateMovementsEnabledState();
        UpdateAttackEnabledState();
    }

    private void UpdateMovementsEnabledState()
    {
        if (_rewind.IsRewindEnabled() || _health.IsDead())
        {
            _movements.SetMoveDirection(Vector2.zero);
            _movements.SetMovementsEnable(false);
            _movements.SetLinearVelocity(Vector3.zero);
            return;
        }

        _movements.SetMovementsEnable(true);
    }

    private void UpdateAttackEnabledState()
    {
        if (_rewind.IsRewindEnabled() || _health.IsDead())
        {
            _attack.SetAttackEnable(false);
            return;
        }

        _attack.SetAttackEnable(true);
    }

    private bool CanTriggerAttack()
    {
        if (_rewind == null)
            return true;

        return !_rewind.IsRewindEnabled();
    }


    private void OnReceivePlayerAttack(AttackParams attackParams, int index, Vector3 forward)
    {
        Command_Attack commandAttack = new Command_Attack(_attack, _rb, attackParams, index, forward);

        _rewind.RegisterCommand(commandAttack);
    }

    private void OnReceivePlayerApplyMove(Command_MoveRigidbody.MoveRigidbody_Params beforeMoveParams, Command_MoveRigidbody.MoveRigidbody_Params afterMoveParams, Transform anchorRotation)
    {
        Command_MoveRigidbody commandMove = new Command_MoveRigidbody(_rb, anchorRotation, beforeMoveParams, afterMoveParams);

        _rewind.RegisterCommand(commandMove);
    }
}
