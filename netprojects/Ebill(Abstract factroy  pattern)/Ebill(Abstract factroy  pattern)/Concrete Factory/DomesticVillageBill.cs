using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ebill_Abstract_factroy__pattern_
{
    public class DomesticVillageBill : IBillGenerator
    {
        private readonly int _unitsConsumed;

        public DomesticVillageBill(int units)
        {
            _unitsConsumed = units;
        }

        public double CalculateBill(int units)
        {
            // Calculate bill based on unit consumption for Village sites
            CalculateUnits Dvil = new CalculateUnits();
            double bill = Dvil.CalculateBill(units);
            return bill * 0;
        }
    }
}
