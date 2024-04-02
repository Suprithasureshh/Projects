using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace Ebill_Abstract_factroy__pattern_
{
    public class DomesticTownBill : IBillGenerator
    {
        private readonly int _unitsConsumed;

        public DomesticTownBill(int units)
        {
            _unitsConsumed = units;
        }

        public double CalculateBill(int units)
        {
            // Calculate bill based on unit consumption for town sites
            CalculateUnits town = new CalculateUnits();
            double bill = town.CalculateBill(units);
            return bill * 2;
        }
    }
}
