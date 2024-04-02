using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ebill_Abstract_factroy__pattern_
{
    public class CommercialBillGenerator : IBillGenerator
    {
        private readonly int _units;

        public CommercialBillGenerator(int units)
        {
            _units = units;
        }

        public double CalculateBill(int units)
        {
            // Calculate bill based on unit consumption for commercial sites
            CalculateUnits com= new CalculateUnits();
            double bill=com.CalculateBill(units);
            return bill * 10;
        }
    }
}
