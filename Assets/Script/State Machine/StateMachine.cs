using System.Collections.Generic;
using UnityEngine;


public abstract class StateMachine<T> : MonoBehaviour where T : System.Enum
{
    private GenericState<T> _currentState;
    private List<GenericState<T>> _statesList = new();
    [SerializeField] private List<T> _allStates;

    public GenericState<T> CurrentState { get => _currentState; }

    protected void AddState(GenericState<T> state)
    {
        _statesList.Add(state);
        Debug.Log(state.GetStateID());
    }

    public virtual void InitStateMachine()
    {
        CreateAllStates();

        foreach (GenericState<T> state in _statesList)
        {
            state.StateInit(this);
        }
    }

    private GenericState<T> GetStateById(T id)
    {
        for (int i = 0; i < _statesList.Count; i++)
        {
            if (EqualityComparer<T>.Default.Equals(_statesList[i].GetStateID(), id))
            {
                return _statesList[i];
            }
        }

        Debug.LogError("State doesn't exist");
        return null;
    }

    public void ChangeState(T idNextState)
    {
        GenericState<T> nextStates = GetStateById(idNextState);
        if (nextStates == null)
            return;


        if (_currentState != null)
        {
            Debug.Log(_currentState.GetStateID());
            _currentState.StateExit(idNextState);
        }


        T lastState = _currentState != null ? _currentState.GetStateID() : default;
        _currentState = nextStates;
        _currentState.StateEnter(lastState);
    }

    public void UpdateStateMachine()
    {
        if (_currentState != null)
            _currentState.StateUpdate(Time.deltaTime);
    }

    protected void CreateAllStates()
    {
        foreach (T state in _allStates)
        {

            Debug.Log(state);
        }
        foreach (T state in _allStates)
        {
            CreateStateById(state);
        }
    }

    protected abstract void CreateStateById(T id);

}
