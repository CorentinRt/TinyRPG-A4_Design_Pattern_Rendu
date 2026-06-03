using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    #region Fields

    [Header("Datas")]
    [SerializeField] private SO_PlayerDatas _datas;

    [Header("Inputs")]
    [SerializeField] private InputActionReference _move;

    [Header("Behaviours")]
    [SerializeField] private PlayerMovementsBehaviour _movements;

    #endregion

    public event Action<Vector2> onMoveInput;

    private void Awake()
    {
        _move.action.started += OnReceiveMoveInput;
        _move.action.performed += OnReceiveMoveInput;
        _move.action.canceled += OnReceiveMoveInput;
    }

    private void OnDestroy()
    {
        _move.action.started -= OnReceiveMoveInput;
        _move.action.performed -= OnReceiveMoveInput;
        _move.action.canceled -= OnReceiveMoveInput;
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

}
