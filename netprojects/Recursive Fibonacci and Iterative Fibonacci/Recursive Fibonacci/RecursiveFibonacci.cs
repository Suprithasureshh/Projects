using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Recursive_Fibonacci
{
    public class RecursiveFibonacci : IRecursive
    {
        public int Recursive(int i)
        {
            if (i <= 1)
            {
                return i;
            }
            else
                return Recursive(i - 1) + Recursive(i - 2);
            
        }
        
    }
}
