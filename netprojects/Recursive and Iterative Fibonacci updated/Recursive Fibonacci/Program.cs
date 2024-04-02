using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Recursive_Fibonacci
{
    public class Program
    { 
        static void Main(string[] args)
        {
            IFibonacciMethod finnaly =new RecursiveFinal();
            finnaly.Fibonacci();
        }
    }
}
