using NaughtyAttributes;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class RewindCommandManager : GenericSingleton<RewindCommandManager>
{
    #region Fields

    [Header("Datas")]
    [SerializeField] private SO_PlayerDatas _datas;

    private bool _isRewinding;

    private float _currentTimeAccumulated;

    private Coroutine _rewindCoroutine;
    private Coroutine _rewindWithDelayCoroutine;

    private List<RewindCommandEntity> _rewindCommandEntities = new(10);


    #endregion

    public event Action onStartRewind;
    public event Action onStopRewind;


    protected override void Awake()
    {
        base.Awake();

        InitCommandRewindManager();
    }

    public void InitCommandRewindManager()  // public cause may be called through the GameManager in a bigger project
    {
        _rewindCommandEntities = FindObjectsByType<RewindCommandEntity>(FindObjectsSortMode.None).ToList();
    }

    private void Update()
    {
        UpdateTimeAccumulated(Time.deltaTime);

    }

    private void UpdateTimeAccumulated(float deltaTime)
    {
        if (_isRewinding)
            return;
            
        _currentTimeAccumulated += deltaTime;

        if (_currentTimeAccumulated > _datas.MaxFreeTimeBeforeRewind)
        {
            SetRewindState(true);  // auto rewind
            Debug.Log("Auto rewind Triggered !", this);
        }
    }

    public float GetCurrentTimeAccumulated()
    {
        return _currentTimeAccumulated;
    }

    [Button]
    public void StartRewind() => SetRewindState(true);
    [Button]
    public void StopRewind() => SetRewindState(false);
    private void SetRewindState(bool enable)
    {
        if (_isRewinding == enable)
            return;

        if (enable && _currentTimeAccumulated < _datas.MinAccumulatedTimeToRewind)
            return;

        _isRewinding = enable;

        for (int i = 0; i < _rewindCommandEntities.Count; ++i)
        {
            RewindCommandEntity entity = _rewindCommandEntities[i];

            if (entity == null)
                continue;

            entity.SetEnableRewindEntity(enable);
        }

        if (_isRewinding)
        {
            StartRewindCoroutine();

            onStartRewind?.Invoke();
        }
    }

    public bool IsRewindEnabled()
    {
        return _isRewinding;
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
        while (_isRewinding && _currentTimeAccumulated > 0f)
        {
            for (int i = 0; i < _rewindCommandEntities.Count; ++i)
            {
                RewindCommandEntity rewindEntity = _rewindCommandEntities[i];

                if (rewindEntity == null)
                    continue;

                rewindEntity.RewindAllCommandsAfterGivenTime(_currentTimeAccumulated);
            }

            _currentTimeAccumulated -= Time.deltaTime * _datas.RewindSpeed;

            yield return null;
        }

        if (_currentTimeAccumulated < 0f)
        {
            _currentTimeAccumulated = 0f;
        }

        SetRewindState(false);

        onStopRewind?.Invoke();
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

        SetRewindState(true);
    }
}
