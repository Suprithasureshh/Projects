using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ebill_Abstract_factroy__pattern_ 
{
    public class CalculateUnits 
    {
      public double CalculateBill(int units)
        {
            double charge;
            charge = (units <= 100) ? units * 0 :
           (units <= 1000) ? (100 * 0) + (units - 100) * 5 :
           (units <= 10000) ? (100 * 0) + (900 * 5) + (units - 1000) * 10 :
           (units <= 30000) ? (100 * 0) + (900 * 5) + (9000 * 10) + (units - 30000) * 20 :
           (units > 30000) ? (100 * 0) + (900 * 5) + (9000 * 10) + (20000 * 20) + (units - 30000) * 35 : 0;
            return charge;
        }
    }
}
