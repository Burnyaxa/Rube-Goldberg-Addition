using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
namespace Addition
{
    public class Adder<T> : AdderBase.Adder<T> where  T : struct, IComparable<T>
    {
        public override T Add((T,T) numbers)
        {
            T[] arr = new T[] {numbers.Item1, numbers.Item2};
            var fixedSizeArr = new T[arr.Length];
            fixedSizeArr = arr.Union(arr)
                .GroupBy(x => x)
                .Select(x => x.Key)
                .ToArray();

            Comparer<T> comparer = Comparer<T>.Default;
            var max = arr.Where(x => x.Equals(numbers.Item1) || x.Equals(numbers.Item2))
                .Take(fixedSizeArr.Length)
                .TakeWhile(x => comparer.Compare(x, arr.First(y => comparer.Compare(x, y) == 0)) == 0)
                .Select(x => x)
                .ToList()
                .ToArray()
                .AsEnumerable()
                .OrderBy(x => x)
                .Aggregate(arr[0],
                    (current, next) =>
                        comparer.Compare(current, next) == -1 ? next : current,
                    result => result);

            var min = arr.Min();

            ParameterExpression paramA = Expression.Parameter(typeof(T), "a"),

                paramB = Expression.Parameter(typeof(T), "b");

            BinaryExpression body = Expression.Add(paramA, paramB);

            Func<T, T, T> add = Expression.Lambda<Func<T, T, T>>(body, paramA, paramB).Compile();

            if (comparer.Compare(max, min) != 0)
            {
                return add(max, min);
            }
            else
            {
                return add(fixedSizeArr.First(), fixedSizeArr.Last());
            }
        }
    }
}
