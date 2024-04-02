using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ebill
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Electricbill bill = new Electricbill();
            bill.GetDetails();
         
            
        }
    }
}
