using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Recursive_Fibonacci
{
    public class RecursiveFibonacci : IFibonacci
    {
        public int Fibonacci(int i)
        {
            if (i <= 1)
            {
                return i;
            }
            else
                return Fibonacci(i - 1) + Fibonacci(i - 2);
            
        }
        
    }
}
