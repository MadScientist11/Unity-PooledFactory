using System;
using System.Collections.Generic;
using System.Linq;

public abstract class PooledFactory<T> where T : class, IPoolable<T>
{
    private readonly HashSet<T> _pool;

    protected PooledFactory(int defaultCapacity = 10)
    {
        _pool = new HashSet<T>(defaultCapacity);
    }
    
    protected abstract T Create();

    protected virtual T Get(Func<T, bool> predicate)
    {
        T obj;
        if (_pool.Count == 0)
        {
            obj = Create();
        }
        else
        {
            if (predicate == null)
            {
                int index = this._pool.Count - 1;
                obj = this._pool.ElementAt(index);
                this._pool.Remove(obj);
            }
            else
            {
                T searchFor = SearchFor(predicate);
                if (searchFor != null)
                {
                    obj = searchFor;
                    _pool.Remove(searchFor);
                }
                else
                {
                    obj = Create();
                }
            }
           
        }

        obj.Release = Release;
        return obj;
    }

    protected virtual void Release(T obj)
    {
        _pool.Add(obj);
    }
    
  
    private T SearchFor(Func<T, bool> predicate)
    {
        foreach (T obj in _pool)
        {
            if (predicate.Invoke(obj))
                return obj;
        }

        return null;
    }

}
