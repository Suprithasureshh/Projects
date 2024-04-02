using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Recursive_Fibonacci
{
    public class RecursiveFibonacci : IFibonacci
    {
        public int Fibonacci(int n)
        {
            if (n <= 1)
            {
                return n;
            }
            else
                return Fibonacci(n - 1) + Fibonacci(n - 2);
            
        }
        
    }
}
