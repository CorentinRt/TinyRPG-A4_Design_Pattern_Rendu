using NaughtyAttributes;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCommandRewind : GenericSingleton<PlayerCommandRewind>
{
    #region Fields

    [Header("Datas")]
    [SerializeField] private SO_PlayerDatas _datas;

    private List<Command> _commands = new(200); // 200 to set a init length and optimize a bit the resize of the list

    private bool _isEnabled;

    private float _currentTimeAccumulated;

    private Coroutine _rewindCoroutine;
    private Coroutine _rewindWithDelayCoroutine;


    #endregion

    public event Action<bool> onSetEnableRewind;



    private void Update()
    {
        UpdateTimeAccumulated(Time.deltaTime);

    }

    private void UpdateTimeAccumulated(float deltaTime)
    {
        if (_isEnabled)
            return;
            
        _currentTimeAccumulated += deltaTime;

        if (_currentTimeAccumulated > _datas.MaxFreeTimeBeforeRewind)
        {
            SetEnableRewind(true);  // auto rewind
            Debug.Log("Auto rewind Triggered !", this);
        }
    }

    public void RegisterCommand(Command command)
    {
        if (command == null || _isEnabled)
            return;

        command.TimeRegistered = _currentTimeAccumulated;

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

        if (enable && _currentTimeAccumulated < _datas.MinAccumulatedTimeToRewind)
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
            int currentIndex = _commands.Count - 1;

            while (currentIndex >= 0 && currentIndex < _commands.Count)
            {
                Command command = _commands[currentIndex];

                if (command == null)
                {
                    _commands.RemoveAt(currentIndex);
                    --currentIndex;
                    continue;
                }

                if (_currentTimeAccumulated <= command.TimeRegistered)
                {
                    command.Undo();
                    _commands.RemoveAt(currentIndex);
                    --currentIndex;
                    continue;
                }

                break;
            }

            _currentTimeAccumulated -= Time.deltaTime * _datas.RewindSpeed;

            yield return null;
        }

        SetEnableRewind(false);
    }

    public void StartRewindWithDelay(float delay)
    {
        if (IsRewindEnabled())
            return;

        StartRewindWithDelayCoroutine(delay);
    }
    private void StartRewindWithDelayCoroutine(float delay)
    {
        StopRewindWithDelayCoroutine();

        _rewindWithDelayCoroutine = StartCoroutine(RewindWithDelayCoroutine(delay));
    }
    private void StopRewindWithDelayCoroutine()
    {
        if (_rewindWithDelayCoroutine != null)
        {
            StopCoroutine(_rewindWithDelayCoroutine);
            _rewindWithDelayCoroutine = null;
        }
    }
    private IEnumerator RewindWithDelayCoroutine(float delay)
    {
        yield return new WaitForSeconds(delay);

        SetEnableRewind(true);
    }
}
