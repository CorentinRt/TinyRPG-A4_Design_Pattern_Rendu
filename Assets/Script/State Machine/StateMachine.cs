using System.Collections.Generic;
using UnityEngine;


public abstract class StateMachine<T> : MonoBehaviour where T : System.Enum
{
    private GenericState<T> _currentState;
    private GenericState<T> _previousState;
    private List<GenericState<T>> _statesList = new();

    public GenericState<T> PreviousState { get => _previousState; }
    public GenericState<T> CurrentState { get => _currentState; }

    protected void AddState(GenericState<T> state)
    {
        _statesList.Add(state);
    }

    protected void StateAtStart(T id)
    {
        _currentState = GetStateById(id);
    }

    private void InitStateMachine()
    {
        foreach (GenericState<T> state in _statesList)
        {
            state.StateInit(this);
        }
    }

    private GenericState<T> GetStateById(T id)
    {
        for (int i = 0; i < _statesList.Count; i++)
        {
            if (EqualityComparer<T>.Default.Equals(_statesList[i].EnumState, id))
            {
                return _statesList[i];
            }
        }

        Debug.LogError("State doesn't exist");
        return null;
    }

    public void ChangeState(T nextState)
    {
        CurrentState.StateExit(nextState);
        _previousState = _currentState;
        _currentState = GetStateById(nextState);
        _currentState.StateEnter(_previousState.EnumState);
    }

    private void Update()
    {
        _currentState.StateUpdate(Time.deltaTime);
    }

    protected abstract void CreateStateById(T id);

}
