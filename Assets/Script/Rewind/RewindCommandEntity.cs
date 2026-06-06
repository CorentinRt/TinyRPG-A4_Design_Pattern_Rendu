using NaughtyAttributes;
using System;
using System.Collections.Generic;
using System.Windows.Input;
using UnityEngine;

public class RewindCommandEntity : MonoBehaviour
{
    #region Fields
    private List<Command> _commands = new(200); // 200 to set a init length and optimize a bit the resize of the list

    private bool _isEnabled;

    #endregion

    public event Action<bool> onSetEnableRewindEntity;


    public void RegisterCommand(Command command)
    {
        if (command == null || _isEnabled)
            return;

        if (RewindCommandManager.Exist)
        {
            command.TimeRegistered = RewindCommandManager.Instance.GetCurrentTimeAccumulated();
        }
        else
        {
            command.TimeRegistered = Time.time;
            Debug.LogWarning("Warning : Command Rewind Manager singleton not found ! Registered command with time Time.time but may cause some issues !", this);
        }

        _commands.Add(command);
    }

    public void ClearCommands()
    {
        _commands.Clear();
    }

    [Button]
    private void DebugEnableRewindEntity() => SetEnableRewindEntity(true);
    [Button]
    private void DebugDisableRewindEntity() => SetEnableRewindEntity(false);
    public void SetEnableRewindEntity(bool enable)
    {
        if (_isEnabled == enable)
            return;

        _isEnabled = enable;

        onSetEnableRewindEntity?.Invoke(_isEnabled);
    }

    public bool IsRewindEnabled()
    {
        return _isEnabled;
    }

    public void RewindAllCommandsAfterGivenTime(float time)
    {
        for (int i = 0; i < _commands.Count; ++i)
        {
            int currentIndex = _commands.Count - 1 - i;

            if (currentIndex >= _commands.Count)
                continue;

            Command command = _commands[currentIndex];

            if (command == null)
            {
                _commands.RemoveAt(currentIndex);
                continue;
            }

            if (time <= command.TimeRegistered)
            {
                command.Undo();
                _commands.RemoveAt(currentIndex);
                continue;
            }
        }
    }
}
