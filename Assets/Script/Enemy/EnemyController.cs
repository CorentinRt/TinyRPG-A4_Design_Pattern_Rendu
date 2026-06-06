using System;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    #region Fields

    [Header("State machine")]
    [SerializeField] private Enemy_StateMachine _stateMachine;

    [Header("Components")]
    [SerializeField] private Transform _mainTransform;


    #endregion



    void Start()
    {
        _stateMachine.InitStateMachine();
    }
    void Update()
    {

        Command_MoveTransform.MoveTransform_Params beforeMoveParam = new();
        beforeMoveParam.Position = _mainTransform.position;
        beforeMoveParam.Rotation = _mainTransform.rotation;

        _stateMachine.UpdateStateMachine();

        Command_MoveTransform.MoveTransform_Params afterMoveParam = new();
        afterMoveParam.Position = _mainTransform.position;
        afterMoveParam.Rotation = _mainTransform.rotation;

        RegisterMoveCommand(beforeMoveParam, afterMoveParam);
    }


    private void RegisterMoveCommand(Command_MoveTransform.MoveTransform_Params beforeMoveParam, Command_MoveTransform.MoveTransform_Params afterMoveParam)
    {
        if (_stateMachine == null)
        {
            Debug.LogError("ERROR : State machine in Enemy controller is null ! Rewind Command move of enemies will not work !", this);
            return;
        }

        Command_MoveTransform commandMoveTransform = new Command_MoveTransform(_mainTransform, beforeMoveParam, afterMoveParam);

        if (_stateMachine.Rewind != null)
        {
            _stateMachine.Rewind.RegisterCommand(commandMoveTransform);
        }
        else
        {
            Debug.LogError("ERROR : _stateMachine.Rewind in Enemy controller is null ! Rewind Command move of enemies will not work !", this);
        }
    }


    private void ReceiveOnDie()
    {
        
    }

    private void ReceiveOnHealthChanged(float currentHealth, float amountChanged)
    {
        
    }

}
