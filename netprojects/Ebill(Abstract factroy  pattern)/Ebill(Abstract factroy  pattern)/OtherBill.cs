using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ebill_Abstract_factroy__pattern_
{
    public class OtherBill
    { public void Bill()
        {
            Console.WriteLine("Do you want to generate another bill");
            Console.WriteLine("If Yes Press 'Y' If No then 'N' ");
            char nextbill = char.ToUpper(Console.ReadKey().KeyChar);
            Console.WriteLine("");
            if (nextbill == 'Y')
            {
                GetDetails other= new GetDetails();
                other.Final();
            }
            else if(nextbill == 'N')
            {
                Console.WriteLine("Press any key to exit");
            }
            else
            {
                Console.WriteLine("Press any key to exit");
            }
            Console.ReadKey();
        }


    }
}
