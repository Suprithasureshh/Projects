using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ebill_Abstract_factroy__pattern_
{
    public class DomesticBill : IElectricBillFactory
    {
        private readonly string _siteType;
      

        public DomesticBill(string siteType)
        {
            _siteType = siteType;
        }

        public IBillGenerator CreateBillGenerator(int units)
        {
            switch (_siteType)
            {
                case "1":
                    return new DomesticCityBill(units);
                case "2":
                    return new DomesticTownBill(units);
                case "3":
                    return new DomesticVillageBill(units);
                default:
                    throw new ArgumentException($"Unknown site type: {_siteType}");
            }
        }
      
    }
}
