
public abstract class GenericState<T> where T : System.Enum
{
    #region Fields
    private StateMachine<T> _stateMachine;
    #endregion
    #region Properties
    public StateMachine<T> StateMachine { get => _stateMachine; }
    #endregion
    public abstract T GetStateID();

    public virtual void StateInit(StateMachine<T> stateMachine)
    {
        _stateMachine = stateMachine;
    }
    public virtual void StateEnter(T previousState)
    {

    }

    public virtual void StateUpdate(float deltaTime)
    {

    }
    public virtual void StateExit(T nextState)
    {

    }
}
