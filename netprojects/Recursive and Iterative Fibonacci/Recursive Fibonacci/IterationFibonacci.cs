using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Recursive_Fibonacci
{
    public class IterationFibonacci: IFibonacci
    {
        public int Fibonacci(int n)
        {
            if (n == 0)
            {
                return n;
            }
            else if (n == 1)
            {
                return n;
            }
            int first = 0;
            int second = 1;
            int next = 0;
            for (int i = 2; i <= n; i++)
            {
               next = first + second;
               first = second;
               second = next;
            }
            return next;
        }
    }
}
