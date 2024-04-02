using Employee_Authentication.Models;
using Microsoft.AspNetCore.Mvc;

namespace Employee_Authentication.Interface
{
    public interface EmployeeInterface
    {
        IActionResult Register1(Register register);
        IActionResult Roles(Role_Model role_Model);
        IActionResult UpdateUser(UpdateUser updateUser);
        ActionResult<Register> Login(Login login);
        public IActionResult delete(int id);
        void UpdateMultipleData(long[] id);
        void UpdateMultipleData12(long[] id);
        IEnumerable<Register> GetEmplyee(int pageNumber, int pageSize, string? SearchKey, bool? active1);
        IEnumerable<Role_Model> GetRole(int pageNumber, int pageSize, string? SearchKey, bool? active1);
    }
}
