using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
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
    [SerializeField] private PlayerCommandRewind _rewind;

    #endregion

    public event Action<Vector2> onMoveInput;

    private void Awake()
    {
        // Move
        _moveInput.action.started += OnReceiveMoveInput;
        _moveInput.action.performed += OnReceiveMoveInput;
        _moveInput.action.canceled += OnReceiveMoveInput;

        // Attack
        _attackInput.action.started += OnReceiveAttackInput;

        // Rewind
        _rewindInput.action.started += OnReceiveRewindInput;
        _rewind.onSetEnableRewind += OnReceiveSetEnableRewind;
    }

    private void OnDestroy()
    {
        // Move
        _moveInput.action.started -= OnReceiveMoveInput;
        _moveInput.action.performed -= OnReceiveMoveInput;
        _moveInput.action.canceled -= OnReceiveMoveInput;

        // Attack
        _attackInput.action.started -= OnReceiveAttackInput;

        // Rewind
        _rewindInput.action.started -= OnReceiveRewindInput;
        _rewind.onSetEnableRewind -= OnReceiveSetEnableRewind;

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
            _rewind.SetEnableRewind(!_rewind.IsRewindEnabled());
        }
        else
        {
            Debug.LogError("Error : Try to rewind but no Player rewind behaviour linked to Player Controller ! rewind of player won't work !", this);
        }
    }

    private void OnReceiveSetEnableRewind(bool enabled)
    {
        _movements.SetMovementsEnable(!enabled);
    }

    private bool CanTriggerMove()
    {
        if (_rewind == null)
            return true;

        return !_rewind.IsRewindEnabled();
    }

    private bool CanTriggerAttack()
    {
        if (_rewind == null)
            return true;

        return !_rewind.IsRewindEnabled();
    }
}
