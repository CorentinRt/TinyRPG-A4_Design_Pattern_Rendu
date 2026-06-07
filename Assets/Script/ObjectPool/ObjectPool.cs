using System;
using System.Collections.Generic;

public class ObjectPool<T> where T : class
{
    #region Fields
    private Stack<T> _pool = new Stack<T>(100);

    private Func<T> _createFunc;
    private Action<T> _actionOnGet;
    private Action<T> _actionOnRelease;
    #endregion
    public ObjectPool(Func<T> createFunc, Action<T> actionOnGet = null, Action<T> actionOnRelease = null)
    {
        _createFunc = createFunc;
        _actionOnGet = actionOnGet;
        _actionOnRelease = actionOnRelease;
    }

    public T Get()
    {
        T item;

        if (_pool.Count > 0)
            item = _pool.Pop();
        else
            item = _createFunc();

        _actionOnGet?.Invoke(item);

        return item;
    }

    public void Release(T item)
    {
        _actionOnRelease?.Invoke(item);
        _pool.Push(item);
    }
}
