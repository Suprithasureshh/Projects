using Employee.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Win32;

namespace Employee.Interface
{
    public interface IEmployee
    {
        Task<ActionResult<string>> Register(EmployeeRegister register);
        Task<ActionResult<string>> Login(Login request);
        IQueryable<EmployeeRegister> Selects(string? search, bool? isActive, int page=1 , int pagesize=10 );
        void Roles(Roles role);
        void UpdateEmployee(EmployeeRegister employee);
        void ActiveEmployee(int[] id);
        void DeactiveEmployee(int[] id);
        void ActivateRole(int[] id);
        void DeactivateRole(int[] id);
        IQueryable<EmployeeRegister> EmployeeBasedOnRole(int? Roleid, string? search, bool? isActive, int page = 1, int pagesize = 10);
        IQueryable<EmployeeRegister> GetAll();

       
    }
}
