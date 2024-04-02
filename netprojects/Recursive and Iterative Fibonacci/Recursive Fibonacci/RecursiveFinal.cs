using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Recursive_Fibonacci 
{
    public class RecursiveFinal: IFibonacciMethod
    {
        public void Fibonacci()
        {
            Console.Write("----Recursive Method----");
            do
            {
               Console.Write("\n Enter the number of terms to perform Fibonacci: ");
                double n;
                try
                {
                    n = Convert.ToDouble(Console.ReadLine());
                    if (n <= 0)
                    {
                        Console.WriteLine("\n Number of terms cannot be Negative and it should be greater than 0.");
                        continue;
                    }
                    else if (Math.Round(n) == 0)
                    {
                        Console.WriteLine("\n Number should be greater than 0.");
                        continue;
                    }
                    else if (n % 1 != 0)
                    {
                        n = Math.Round(n);
                    }
                   
                }
                catch (FormatException)
                {
                    Console.WriteLine("\n Input should be in number only.");
                    continue;
                }

                IFibonacci recur = new RecursiveFibonacci();

                Console.WriteLine("\n Fibonacci sequence up to: {0}", n + "\n");
                Console.WriteLine("\n ***Using Recursive Method***");
                for (int i = 1; i <= n; i++)
                {
                    Console.Write("\n Recursive Fibonacci of {0}", i + " " + "is:" + " ");
                    Console.WriteLine(recur.Fibonacci(i));
                }
                Console.WriteLine("");
                do
                {
                    Console.Write("\n Do you want to continue? ");
                    Console.WriteLine("\n If Yes press Y then If No press N: ");
                    string input;
                    try { 
                        input= Console.ReadLine();
                        if(input.ToUpper() == "Y")
                        {
                          Fibonacci();
                        }
                        else if (input.ToUpper() == "N")
                        {
                       
                          break;
                        }
                        else
                        {
                            Console.WriteLine("\n Input should be either Y nor N.");
                            continue;
                        }
                    }
                    catch (FormatException)
                    {
                    Console.WriteLine("\n Input should be either Y nor N.");
                    continue;
                    }
                } while (true);
                    IFibonacciMethod iter1 = new IterativeFinal();
                    iter1.Fibonacci();
                    break;
            } while (true);
           

        }

       
    }
}
