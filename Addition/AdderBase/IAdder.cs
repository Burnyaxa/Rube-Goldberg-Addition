using System;

namespace Addition
{
    public interface IAdder<T> where T : struct, IComparable<T>
    {
        public T Add((T,T) numbers);
    }
}
