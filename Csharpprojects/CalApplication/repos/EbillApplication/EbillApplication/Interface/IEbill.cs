using EbillApplication.Model;
using Microsoft.AspNetCore.Mvc;

namespace EbillApplication.Interface
{
    public interface IEbill
    {
        ActionResult<string> BillCalculate(EbillProperties cal);


        void delete(int CustomerId);
        void update(EbillProperties Bill);
        //EbillProperties GetId(int CustomerId);

        IEnumerable<EbillProperties> Get1(bool? isActive);
        public void UpdateActive(int[] ids);
        public void UpdateNotActive(int[] ids);
    }
}
