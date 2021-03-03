using System;
using System.Runtime.ExceptionServices;

namespace Addition
{
    class Program
    {
        static void Main(string[] args)
        {
            int first = Convert.ToInt32(Console.ReadLine());
            int second = Convert.ToInt32(Console.ReadLine());

            var type = typeof(Adder<>).MakeGenericType(first.GetType());
            var adderObj = Activator.CreateInstance(type);
            var adder = adderObj as Adder<int>;
            
            Console.WriteLine(adder.Add(numbers: (first, second)));
        }
    }
}
