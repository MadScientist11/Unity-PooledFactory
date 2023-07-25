using System;

namespace PooledFactory
{
    public interface IPoolable<T>
    {
        public Action<T> Release { get; set; }
    }
}