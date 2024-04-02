using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace Ebill_Abstract_factroy__pattern_
{
    public class DomesticCityBill : IBillGenerator
    {
        private readonly int _unitsConsumed;

        public DomesticCityBill(int units)
        {
            _unitsConsumed = units;
        }

        public double CalculateBill(int units)
        {
            // Calculate bill based on unit consumption for city sites
            CalculateUnits city = new CalculateUnits();
            double bill = city.CalculateBill(units);
            return bill * 5;
        }
    }
}
