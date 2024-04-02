using TimeSheet.Model;
using static TimeSheet.Implementation.DomainModel.AdminDomainModel;

namespace TimeSheet.Implementation.Interface
{
    public interface IAdmin
    {
        //Client
        void AddClient(AddClientModel model);
        void EditClient(EditClientModel editClientModel);
        IQueryable<Client> GetByClientId(int id);
        IQueryable<Client> GetAllClients();

        //Designation
        void AddDesignation(PostDesignationModel postDesignationModel);
        void EditDesignation(EditDesignationModel editDesignationModel);
        IQueryable<Designations> GetByDesignationId(int id);
        IEnumerable<GetAllDesignationsByEmployeeModel> GetAllDesignationsByEmployee();
        IQueryable<Designations> GetAllDesignations();

        //EmployeeType
        void AddEmployeeType(PostEmployeeTypeModel postEmployeeTypeModel);
        void EditEmployeetype(EditEmployeeTypeModel editEmployeeTypeModel);
        IQueryable<EmployeeType> GetByEmployeeTypeId(int id);
        IEnumerable<GetAllEmployeeTypeByEmployeeModel> GetAllEmployeeTypesByEmployee();
        IQueryable<EmployeeType> GetAllEmplyoeeTypes();

       
        
        

    }
}
