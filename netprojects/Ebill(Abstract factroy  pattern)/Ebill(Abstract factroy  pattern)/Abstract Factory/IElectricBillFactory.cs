using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ebill_Abstract_factroy__pattern_
{
    public interface IElectricBillFactory
    {
        IBillGenerator CreateBillGenerator(int units);
     
    }
}
