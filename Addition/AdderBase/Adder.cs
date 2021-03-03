using System;
using System.Collections.Generic;
using System.Text;

namespace Addition.AdderBase
{
    public abstract class Adder<T> : IAdder<T> where T : struct, IComparable<T>
    {
        public abstract T Add((T, T) numbers);
    }
}
