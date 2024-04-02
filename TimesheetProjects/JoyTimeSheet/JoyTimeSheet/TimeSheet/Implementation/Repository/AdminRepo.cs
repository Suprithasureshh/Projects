using Microsoft.AspNetCore.Mvc;
using TimeSheet.Data;
using TimeSheet.ExceptionFilter;
using TimeSheet.Implementation.Interface;
using TimeSheet.Model;
using static TimeSheet.Implementation.DomainModel.AdminDomainModel;

namespace TimeSheet.Implementation.Repository
{
    public class AdminRepo : IAdmin
    {

        private readonly TimeSheetContext _timesheetContext;
        public AdminRepo(TimeSheetContext timesheetContext)
        {
            _timesheetContext = timesheetContext;
        }

        //Client

        public void AddClient(AddClientModel model)
        {
            var data = new Client();
            data.Client_Name = model.Client_Name;
            data.Create_Date = DateTime.UtcNow.Date;
            data.Is_Active = true;

            _timesheetContext.clients.Add(data);
            _timesheetContext.SaveChanges();
        }

        public void EditClient(EditClientModel editClientModel)
        {
            var ClientId = _timesheetContext.clients.FirstOrDefault(e =>
            e.Client_Id == editClientModel.Client_Id);

            if (ClientId != null)
            {
                ClientId.Client_Name = editClientModel.Client_Name;
                ClientId.Modified_Date = DateTime.UtcNow.Date;
                _timesheetContext.SaveChanges();
            }
            else
            {
                throw new ClientIdException();
            }
        }

        public IQueryable<Client> GetByClientId(int id)
        {
            var clients = _timesheetContext.clients.AsQueryable();
            var item = _timesheetContext.clients.FirstOrDefault(d => d.Client_Id == id);
            if (item != null)
            {
                return clients.Where(e => e.Client_Id == id);
            }
            else
            {
                throw new ClientIdException();
            }
        }

        public IQueryable<Client> GetAllClients()
        {
            return _timesheetContext.clients.Where(e => e.Is_Active == true).OrderBy(c => c.Client_Name).AsQueryable();
        }

        //Designation

        public void AddDesignation(PostDesignationModel postDesignationModel)
        {
            var table = _timesheetContext.designations.FirstOrDefault(e => e.Designation == postDesignationModel.Designation);
            if (table == null)
            {
                var data = new Designations();
                data.Designation = postDesignationModel.Designation;
                data.Create_Date = DateTime.UtcNow.Date;
                data.Is_Active = true;
                _timesheetContext.designations.Add(data);
                _timesheetContext.SaveChanges();
            }
            else
            {
                throw new DesignationNameException();
            }
        }

        public void EditDesignation(EditDesignationModel editDesignationModel)
        {
            var DesignationIdCheck = _timesheetContext.designations.FirstOrDefault
                 (e => (e.Designation_Id == editDesignationModel.Designation_Id));
            var DesignationNameCheck = _timesheetContext.designations.FirstOrDefault
                 (e => (e.Designation == editDesignationModel.Designation));

            if (DesignationNameCheck == null)
            {
                if (DesignationIdCheck != null)
                {
                    DesignationIdCheck.Designation = editDesignationModel.Designation;
                    DesignationIdCheck.Modified_Date = DateTime.UtcNow.Date;
                    _timesheetContext.SaveChanges();
                }
                else
                {
                    throw new DesignationIdException();
                }
            }
            else
            {
                throw new DesignationNameException();
            }
        }

        public IQueryable<Designations> GetByDesignationId(int id)
        {
            var designations = _timesheetContext.designations.AsQueryable();
            var item = _timesheetContext.designations.FirstOrDefault(d => d.Designation_Id == id);
            if (item != null)
            {
                return designations.Where(e => e.Designation_Id == id);
            }
            else
            {
                throw new DesignationIdException();
            }
        }

        public IEnumerable<GetAllDesignationsByEmployeeModel> GetAllDesignationsByEmployee()                                        //GetAllDesignations
        {
            var data = from a in _timesheetContext.designations
                       join b in _timesheetContext.employees
                       on a.Designation_Id equals b.Designation_Id
                       where (a.Is_Active == true)
                       select new { a, b } into t1
                       group t1 by new { t1.a.Designation, t1.a.Designation_Id, empcount = t1.b.Employee_Id == null ? 0 : 1 }
                        into g
                       orderby g.Key.Designation
                       select new GetAllDesignationsByEmployeeModel
                       {
                           Designation_Id = g.Key.Designation_Id,
                           Designation = g.Key.Designation,
                           No_of_Employees = g.Key.empcount == 0 ? 0 : g.Count()
                       };
            return data.ToList();
        }


        public IQueryable<Designations> GetAllDesignations()
        {
            return _timesheetContext.designations.Where(e => e.Is_Active == true).OrderBy(e => e.Designation).AsQueryable();
        }



        //Employee Type

        public void AddEmployeeType(PostEmployeeTypeModel postEmployeeTypeModel)
        {
            var table = _timesheetContext.employeeTypes.FirstOrDefault(e => e.Employee_Type == postEmployeeTypeModel.Employee_Type);

            if (table == null)
            {
                var data = new EmployeeType();
                data.Employee_Type = postEmployeeTypeModel.Employee_Type;
                data.Create_Date = DateTime.UtcNow.Date;
                data.Is_Active = true;

                _timesheetContext.employeeTypes.Add(data);
                _timesheetContext.SaveChanges();
            }
            else
            {
                throw new EmployeeTypeNameException();
            }
        }

        public void EditEmployeetype(EditEmployeeTypeModel editEmployeeTypeModel)                                //EditEmployeeType
        {
            var IdCheck = _timesheetContext.employeeTypes.FirstOrDefault
                 (e => (e.Employee_Type_Id == editEmployeeTypeModel.Employee_Type_Id));
            var NameCheck = _timesheetContext.employeeTypes.FirstOrDefault
                 (e => (e.Employee_Type == editEmployeeTypeModel.Employee_Type));

            if (NameCheck == null)
            {
                if (IdCheck != null)
                {
                    IdCheck.Employee_Type = editEmployeeTypeModel.Employee_Type;
                    IdCheck.Modified_Date = DateTime.UtcNow.Date;
                    _timesheetContext.SaveChanges();
                }
                else
                {
                    throw new EmployeeTypeIdException();
                }
            }
            else
            {
                throw new EmployeeTypeNameException();
            }
        }

        public IQueryable<EmployeeType> GetByEmployeeTypeId(int id)
        {
            var employeeType = _timesheetContext.employeeTypes.AsQueryable();
            var item = _timesheetContext.employeeTypes.FirstOrDefault(d => d.Employee_Type_Id == id);
            if (item != null)
            {
                return employeeType.Where(e => e.Employee_Type_Id == id);
            }
            else
            {
                throw new EmployeeTypeIdException();
            }
        }



        public IEnumerable<GetAllEmployeeTypeByEmployeeModel> GetAllEmployeeTypesByEmployee()                                           //GetAllEmployeeType
        {
            var data = (from a in _timesheetContext.employeeTypes
                        join b in _timesheetContext.employees
                        on a.Employee_Type_Id equals b.Employee_Type_Id
                        where (a.Is_Active == true)
                        select new { a, b } into t1
                        group t1 by new { t1.a.Employee_Type, t1.a.Employee_Type_Id }
                         into g
                        orderby g.Key.Employee_Type ascending
                        select new GetAllEmployeeTypeByEmployeeModel
                        {
                            Employee_Type_Id = g.Key.Employee_Type_Id,
                            Employee_Type = g.Key.Employee_Type,
                            No_of_Employees = g.Count()
                        });
            return data.ToList();
        }

        public IQueryable<EmployeeType> GetAllEmplyoeeTypes()
        {
            return _timesheetContext.employeeTypes.Where(e => e.Is_Active == true).OrderBy(e => e.Employee_Type).AsQueryable();
        }

        //Employee

        public void AddEmploye(AddEmployeeModel addEmployeeModel)                                                            //Addemployee
        {
            var EmailContactCheck = _timesheetContext.employees.FirstOrDefault(e => e.Official_Email == addEmployeeModel.Official_Email || e.Contact_No == addEmployeeModel.Contact_No);
            var ts = new TimeSheetSummary();
            if (EmailContactCheck == null)
            {
                var emp = new Employee();
                emp.First_Name = addEmployeeModel.First_Name;
                emp.Last_Name = addEmployeeModel.Last_Name;
                emp.Employee_code = addEmployeeModel.Employee_code;
                emp.Reporting_Manager1 = addEmployeeModel.Reporting_Manager1;
                emp.Reportinng_Manager2 = addEmployeeModel.Reportinng_Manager2;
                emp.Employee_Type_Id = addEmployeeModel.Employee_Type_Id;
                emp.Official_Email = addEmployeeModel.Official_Email;
                emp.Alternate_Email = addEmployeeModel.Alternate_Email;
                emp.Contact_No = addEmployeeModel.Contact_No;
                emp.Password = addEmployeeModel.Password;
                emp.Designation_Id = addEmployeeModel.Designation_Id;
                emp.Employee_Type_Id = addEmployeeModel.Employee_Type_Id;
                emp.Joining_Date = addEmployeeModel.Joining_Date;
                emp.Create_Date = DateTime.UtcNow.Date;
                 _timesheetContext.employees.Add(emp);
                _timesheetContext.SaveChanges();

                var max = _timesheetContext.employees.Max(e => e.Employee_Id);
                ts.Created_Date = DateTime.UtcNow.Date;
                ts.Employee_Id = max;
                ts.No_Of_days_Worked = 0;
                ts.No_Of_Leave_Taken = 0;
                ts.Status = "Pending";
                ts.Total_Working_Hours = 0;
                ts.Year = DateTime.UtcNow.Year;
                _timesheetContext.timeSheetSummarys.Add(ts);
                _timesheetContext.SaveChanges();
            }
            else
            {
                var a = _timesheetContext.employees.FirstOrDefault(e => ((e.Official_Email == addEmployeeModel.Official_Email || e.Alternate_Email == addEmployeeModel.Official_Email)) || e.Contact_No == addEmployeeModel.Contact_No);
                if (a.Official_Email == addEmployeeModel.Official_Email && a.Contact_No != addEmployeeModel.Contact_No)
                {
                    throw new Exception( addEmployeeModel.Official_Email + " " + " Already Exist");
                }
                else if (a.Official_Email != addEmployeeModel.Official_Email && a.Contact_No == addEmployeeModel.Contact_No)
                {
                    throw new Exception (addEmployeeModel.Contact_No + " " + " Already Exist");
                }
                else
                {
                    throw new Exception( addEmployeeModel.Contact_No + " " + addEmployeeModel.Official_Email + " " + " Already Exist");
                }

            }


        }

        //EditEmployee

        public string EditEmployee(EditEmployeeModel editEmployeeModel)                                            
        {
            var IdCheck = _timesheetContext.employees.FirstOrDefault(e => e.Employee_Id == editEmployeeModel.Employee_Id);
            var doubleentry = _timesheetContext.employees.FirstOrDefault(e => e.Employee_Id != editEmployeeModel.Employee_Id && (e.Official_Email == editEmployeeModel.Email || e.Contact_No == editEmployeeModel.Contact_No
            || e.Official_Email == editEmployeeModel.Official_Email || e.Alternate_Email == editEmployeeModel.Alternate_Email));
            var data = new ViewPreviousChanges();
            if (IdCheck != null)
            {
                if (doubleentry == null || doubleentry.Employee_Id == IdCheck.Employee_Id)
                {
                    data.Employee_Id = IdCheck.Employee_Id;
                    data.First_Name = IdCheck.First_Name;
                    data.Last_Name = IdCheck.Last_Name;
                    data.Employee_code = IdCheck.Employee_code;
                    data.Employee_Type_Id = IdCheck.Employee_Type_Id;
                    data.Email = IdCheck.Official_Email;
                    data.Alternate_Email = IdCheck.Alternate_Email;
                    data.Designation_Id = IdCheck.Designation_Id;
                    data.Contact_No = IdCheck.Contact_No;
                    data.Reporting_Manager1 = IdCheck.Reporting_Manager1;
                    data.Reportinng_Manager2 = IdCheck.Reportinng_Manager2;
                    data.Is_Active = IdCheck.Is_Active;
                    data.Joining_Date = IdCheck.Joining_Date;
                    data.End_Date = IdCheck.End_Date;
                    data.Modified_Date = IdCheck.Modified_Date;
                    _timesheetContext.viewPreviousChanges.Update(data);
                    _timesheetContext.SaveChanges();

                    data.Employee_Id = editEmployeeModel.Employee_Id;
                    data.First_Name = editEmployeeModel.First_Name;
                    data.Last_Name = editEmployeeModel.Last_Name;
                    data.Employee_code = editEmployeeModel.Employee_code;
                    data.Employee_Type_Id = editEmployeeModel.Employee_Type_Id;
                    data.Email = editEmployeeModel.Email;
                    data.Alternate_Email = editEmployeeModel.Alternate_Email;
                    data.Designation_Id = editEmployeeModel.Designation_Id;
                    data.Contact_No = editEmployeeModel.Contact_No;
                    data.Reporting_Manager1 = editEmployeeModel.Reporting_Manager1;
                    data.Reportinng_Manager2 = editEmployeeModel.Reportinng_Manager2;
                    data.Joining_Date = editEmployeeModel.Joining_Date;
                    data.End_Date = editEmployeeModel.End_Date;
                    data.Modified_Date = DateTime.Now.Date;
                    _timesheetContext.viewPreviousChanges.Update(data);
                    _timesheetContext.SaveChanges();

                    IdCheck.Employee_Id = editEmployeeModel.Employee_Id;
                    IdCheck.First_Name = editEmployeeModel.First_Name;
                    IdCheck.Last_Name = editEmployeeModel.Last_Name;
                    IdCheck.Employee_Type_Id = editEmployeeModel.Employee_Type_Id;
                    IdCheck.Official_Email = editEmployeeModel.Email;
                    IdCheck.Employee_code = editEmployeeModel.Employee_code;
                    IdCheck.Alternate_Email = editEmployeeModel.Alternate_Email;
                    IdCheck.Designation_Id = editEmployeeModel.Designation_Id;
                    IdCheck.Contact_No = editEmployeeModel.Contact_No;
                    IdCheck.Reporting_Manager1 = editEmployeeModel.Reporting_Manager1;
                    IdCheck.Reportinng_Manager2 = editEmployeeModel.Reportinng_Manager2;
                    IdCheck.Role_Id = 1;
                    IdCheck.Joining_Date = editEmployeeModel.Joining_Date;
                    IdCheck.End_Date = editEmployeeModel.End_Date;
                    IdCheck.Modified_Date = DateTime.Now.Date;
                    _timesheetContext.SaveChanges();
                    return "Employee Updated Successfully";
                }
                else
                {
                    var p = _timesheetContext.employees.FirstOrDefault(e => e.Official_Email == editEmployeeModel.Email || e.Contact_No == editEmployeeModel.Contact_No);
                    if (p.Official_Email == editEmployeeModel.Email && p.Contact_No != editEmployeeModel.Contact_No)
                    {
                        return editEmployeeModel.Contact_No + " " + " Already Exist";
                    }
                    else if (p.Official_Email != editEmployeeModel.Email && p.Contact_No == editEmployeeModel.Contact_No)
                    {
                        return editEmployeeModel.Email + " " + " Already Exist";
                    }
                    else
                    {
                        return editEmployeeModel.Contact_No + " " + editEmployeeModel.Email + " Already Exist";
                    }
                }
            }
            return "The Id Does Not Exist";
        }
    }
}
