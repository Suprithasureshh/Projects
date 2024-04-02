using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace Ebill_Abstract_factroy__pattern_
{
    public class VillageBillGenerator : IBillGenerator
    {
        private readonly int _unitsConsumed;

        public VillageBillGenerator(int units)
        {
            _unitsConsumed = units;
        }

        public double CalculateBill(int units)
        {
            // Calculate bill based on unit consumption for commercial sites
            CalculateUnits vil = new CalculateUnits();
            double bill = vil.CalculateBill(units);
            return bill * 0;
        }
    }
    }

 