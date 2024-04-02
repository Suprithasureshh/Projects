using EbillApplication.Data;
using EbillApplication.Interface;
using EbillApplication.Model;
using Microsoft.AspNetCore.Mvc;

namespace EbillApplication.Repository
{
    public class EbillRepository :ControllerBase, IEbill
    {
        private readonly EbillContext _context;

        public EbillRepository(EbillContext context)
        {
            _context = context;
        }

        private double CalculateTotalCharges(double units, string site, string site1)
        {
            double value = units <= 100 ? 0 :
                units >= 101 && units <= 1000 ? ((units - 100) * 5) :
                units >= 1001 && units <= 10000 ? ((100 * 5) + ((units - 1000) * 10)) :
                units >= 10001 && units <= 30000 ? ((100 * 5) + (100 * 10) + ((units - 10000) * 20)) :
                ((100 * 5) + (100 * 10) + (20000 * 20) + ((units - 30000) * 35));

            return (site == "1") ?
                (site1 == "1") ? value * 5 :
                (site1 == "2") ? value * 2 :
                (site1 == "3") ? value * 0 :
                throw new ArgumentException("Invalid site1 value") :

            (site == "2") ? value * 10 :
            (site == "3") ? value * 0 :
            throw new ArgumentException("Invalid site value");
        }
        public ActionResult <string> BillCalculate(EbillProperties cal)
        {
            if (_context.EbillTable.Any(b => b.CustomerId == cal.CustomerId))
            {
                return BadRequest($"BillId {cal.CustomerId} is already exists in CustomerTable");
            }
            double charges = CalculateTotalCharges(cal.units,cal.site,cal.site1);
             cal.charges = charges;
             _context.EbillTable.Add(cal);
             _context.SaveChanges();
             return charges.ToString();
        }
        public void delete(int CustomerId)
        {
            var delete = _context.EbillTable.FirstOrDefault(b => b.CustomerId == CustomerId);
            _context.EbillTable.Remove(delete);
            _context.SaveChanges();
            
        }
        public void update(EbillProperties Bill)
        {
            var update = _context.EbillTable.FirstOrDefault(b => b.CustomerId == Bill.CustomerId);
            update.Id = Bill.Id;
            update.CustomerName = Bill.CustomerName;
            update.Address = Bill.Address;
            update.site = Bill.site;
            update.site1 = Bill.site1;
            update.units = Bill.units;
            update.charges = CalculateTotalCharges(Bill.units, Bill.site, Bill.site1);
            update.isActive = Bill.isActive;
            _context.EbillTable.Update(update);
            _context.SaveChanges();
            
        }
        public void UpdateActive(int[] ids)
        {
            var records = _context.EbillTable.Where(b => ids.Contains(b.CustomerId) && !b.isActive);
            foreach (var record in records)
            {
                record.isActive = true;
            }
            _context.SaveChanges();
        }

        public void UpdateNotActive(int[] ids)
        {
            var records = _context.EbillTable.Where(b => ids.Contains(b.CustomerId) && b.isActive);
            foreach (var record in records)
            {
                record.isActive = false;
            }
            _context.SaveChanges();
        }
        //public EbillProperties GetId(int CustomerId)
        //{
        //    return _context.EbillTable.FirstOrDefault(b => b.CustomerId == CustomerId);
        //}
        public IEnumerable<EbillProperties> Get1(bool? isActive)
        {
            if (isActive == true)
            {
                return _context.EbillTable.Where(b => b.isActive);
            }
            else if(isActive==false)
            {
                return _context.EbillTable.Where(b => !b.isActive);
            }
            else
            {
               return  _context.EbillTable;
            }
          
        }
    }
}
