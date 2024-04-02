using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Recursive_Fibonacci 
{
    public class RecursiveFinal
    {
        public void  Recursive()
        {
            while (true)
            {
                Console.Write("\n Enter the number of terms to perform Fibonacci: ");
                int n = Convert.ToInt32(Console.ReadLine());
                RecursiveFibonacci fibonacci = new RecursiveFibonacci();
                Console.WriteLine("Fibonacci sequence up to: {0}", n + "\n");
                for (int i = 0; i < n; i++)
                {
                    Console.Write("Fibonacci of {0}",i + " " + "is:" + " ");
                    Console.WriteLine(fibonacci.Fibonacci(i));
                }
                Console.WriteLine("");
                Console.Write("\n Do you want to continue? ");
                Console.WriteLine("\n If Yes press Y then If No press N: ");
                string input = Console.ReadLine();
                Console.WriteLine("");
                if (input.ToUpper() != "Y")
                {
                    break;
             
                }
               
            }
           
        }

       
    }
}
