using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    #region Fields
    [Header("Inputs")]
    [SerializeField] private InputActionReference _move;


    #endregion

    public event Action<Vector2> onMoveInput;

    private void Awake()
    {
        InitPlayerController();
    }

    private void OnDestroy()
    {
        _move.action.started -= OnReceiveMoveInput;
        _move.action.performed -= OnReceiveMoveInput;
        _move.action.canceled -= OnReceiveMoveInput;
    }

    private void InitPlayerController()
    {
        _move.action.started += OnReceiveMoveInput;
        _move.action.performed += OnReceiveMoveInput;
        _move.action.canceled += OnReceiveMoveInput;
    }

    private void OnReceiveMoveInput(InputAction.CallbackContext ctx)
    {
        onMoveInput?.Invoke(ctx.ReadValue<Vector2>());
    }

}
