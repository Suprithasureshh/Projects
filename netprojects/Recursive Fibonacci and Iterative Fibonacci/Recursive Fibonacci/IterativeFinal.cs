using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Recursive_Fibonacci
{
    public class IterativeFinal: IIterativeMethod
    {
        public void Iterative()
        {
            while (true)
            {
                Console.Write("\n Enter the number of terms to perform Fibonacci: ");
                int n;
                try
                {
                    n = Convert.ToInt32(Console.ReadLine());
                    if (n <=0)
                    {
                        Console.WriteLine("\n Number of terms cannot be Negative and it should be greater than 0.");
                        continue;
                    }
                }
                catch (FormatException)
                {
                    Console.WriteLine("\n Input should be in number only.");
                    continue;
                }
                Console.WriteLine("\n Fibonacci sequence up to: {0}", n + "\n");
                Console.WriteLine("\n ***Using Iterative Method***");
                IIterative iter = new IterationFibonacci();
                for (int i=0; i<=n; i++)
                {
                    Console.Write("\n Iterative Fibonacci of {0}", i + " " + "is:" + " ");
                    Console.WriteLine(iter.Iterative(i));
                }
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
