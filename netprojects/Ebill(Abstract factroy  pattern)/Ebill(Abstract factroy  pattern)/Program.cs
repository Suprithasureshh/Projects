using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ebill_Abstract_factroy__pattern_
{
    internal class Program
    {
        static void Main(string[] args)
        {
            GetDetails get=new GetDetails();
            get.Final();
        }
    }
}
