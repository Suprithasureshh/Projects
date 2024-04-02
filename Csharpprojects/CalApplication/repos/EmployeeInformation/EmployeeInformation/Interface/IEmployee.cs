using EmployeeInformation.Model;
using Microsoft.AspNetCore.Mvc;

namespace EmployeeInformation.Interface
{
    public interface IEmployee
    {
        ActionResult post(EmployeeProperties emp);
        IEnumerable<EmployeeProperties> Get();
        void Delete(int Id);
    }
}
