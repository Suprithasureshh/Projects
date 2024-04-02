using EmployeeRegisterDetails.Model;
using Microsoft.AspNetCore.Mvc;

namespace EmployeeRegisterDetails.Interface
{
    public interface IEmployeeRegister
    {
        ActionResult Post(EmployeeProperties emp);
        IEnumerable<EmployeeProperties> Get();
        void UpdateEmployee(EmployeeProperties emp);
        void Delete(int Id);
        void ActivateEmployee(int[] id);
        void DeactivateEmployee(int[] id);
        IActionResult select(bool? isActive);
    }
}
