using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ebill_Abstract_factroy__pattern_
{
    public class CommercialBill : IElectricBillFactory
    {
        public IBillGenerator CreateBillGenerator(int units)
        {
            return new CommercialBillGenerator(units);
        }
    }
}
