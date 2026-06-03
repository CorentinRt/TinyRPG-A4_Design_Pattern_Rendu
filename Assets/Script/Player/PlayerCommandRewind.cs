using NaughtyAttributes;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCommandRewind : GenericSingleton<PlayerCommandRewind>
{
    #region Fields

    [Header("Rewind")]
    [SerializeField] private int _minCommandToStartRewind = 20;

    private List<Command> _commands = new();

    private bool _isEnabled;

    private Coroutine _rewindCoroutine;


    #endregion

    public event Action<bool> onSetEnableRewind;

    

    public void RegisterCommand(Command command)
    {
        if (command == null || _isEnabled)
            return;

        _commands.Add(command);
    }

    public void ClearCommands()
    {
        _commands.Clear();
    }

    [Button]
    private void DebugEnableRewind() => SetEnableRewind(true);
    [Button]
    private void DebugDisableRewind() => SetEnableRewind(false);
    public void SetEnableRewind(bool enable)
    {
        if (_isEnabled == enable)
            return;

        if (enable && _minCommandToStartRewind > _commands.Count)
            return;

        _isEnabled = enable;

        if (_isEnabled)
        {
            StartRewindCoroutine();
        }

        onSetEnableRewind?.Invoke(_isEnabled);
    }

    public bool IsRewindEnabled()
    {
        return _isEnabled;
    }

    private void StartRewindCoroutine()
    {
        StopRewindCoroutine();

        _rewindCoroutine = StartCoroutine(RewindCoroutine());
    }
    private void StopRewindCoroutine()
    {
        if (_rewindCoroutine != null)
        {
            StopCoroutine(_rewindCoroutine);
            _rewindCoroutine = null;
        }
    }
    private IEnumerator RewindCoroutine()
    {
        while (_isEnabled && _commands.Count > 0)
        {
            Command command = _commands[_commands.Count - 1];

            if (command != null)
            {
                command.Undo();
            }

            _commands.RemoveAt(_commands.Count - 1);

            yield return null;
        }

        SetEnableRewind(false);
    }
}
