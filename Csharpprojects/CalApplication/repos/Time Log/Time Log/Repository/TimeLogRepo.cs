using System.Net.Mail;
using System.Net;
using Time_Log.Data_Context;
using Time_Log.Interface;
using Time_Log.Model;
using static Time_Log.DomainModel.TimeLog_DomainModel;
using static Time_Log.Exceptions.TimeLogException;

namespace Time_Log.Repository
{
    public class TimeLogRepo: TimeLogInterface
    {

        private readonly TimeLogContext _timelogContext;
        public TimeLogRepo(TimeLogContext timelogContext)
        {
            _timelogContext = timelogContext;
        }

        //Branch
        public void AddBranch(AddBranchModel model)
        {
            var BranchCheck = _timelogContext.branch.FirstOrDefault(e => e.Branch_Name == model.Branch_Name);
            if (BranchCheck == null)
            {
                var data = new Branch();
                data.Branch_Name = model.Branch_Name;
                data.Create_Date = DateTime.UtcNow.Date;
                data.Is_Active = true;

                _timelogContext.branch.Add(data);
                _timelogContext.SaveChanges();
            }
            else
            {
                throw new BranchNameExistException();
            }
        }

        public void EditBranch(EditBranchModel editBranchModel)
        {
            var ClientId = _timelogContext.branch.FirstOrDefault(e => e.Branch_Id == editBranchModel.Branch_Id);
            var ClientCheck = _timelogContext.branch.FirstOrDefault(e => e.Branch_Id != editBranchModel.Branch_Id && e.Branch_Name == editBranchModel.Branch_Name);

            if (ClientId != null)
            {
                if (ClientCheck == null)
                {
                    ClientId.Branch_Name = editBranchModel.Branch_Name;
                    ClientId.Modified_Date = DateTime.UtcNow.Date;
                    _timelogContext.SaveChanges();
                }
                else
                {
                    throw new BranchNameExistException();
                }
            }
            else
            {
                throw new BranchIdException();
            }
        }

        public void EditBranchIsActive(IsActiveModel BranchIsActiveModel, bool Is_Active)
        {

             var records = _timelogContext.branch.Where(a => BranchIsActiveModel.Id.Contains(a.Branch_Id));
             if (records.Count() != 0)
             {
                  foreach (var r in records)
                  {
                     r.Is_Active = Is_Active;
                  }
                  _timelogContext.SaveChanges();
             }
             else
             {
                throw new BranchIdException();
             }
        }

        public IQueryable<Branch> GetByBranchId(int id)
        {
            var clients = _timelogContext.branch.AsQueryable();
            var item = _timelogContext.branch.FirstOrDefault(d => d.Branch_Id == id);
            if (item != null)
            {
                return clients.Where(e => e.Branch_Id == id);
            }
            else
            {
                throw new BranchIdException();
            }
        }

       public IEnumerable<BranchIsActiveModel> GetBranchIsActive(bool? isActive)
       {
            var data = (from a in _timelogContext.branch
                        join b in _timelogContext.faculties
                        on a.Branch_Id equals b.Branch_Id into faculties
                        from b in faculties.DefaultIfEmpty()
                        select new { a, b }
                        into t1
                        group t1 by new { t1.a.Branch_Name, t1.a.Branch_Id, t1.a.Is_Active } into g
                        orderby g.Key.Branch_Name

                        select new BranchIsActiveModel
                        {
                            Branch_Id = g.Key.Branch_Id,
                            Branch_Name = g.Key.Branch_Name,
                            Is_Active = g.Key.Is_Active,
                            No_Of_Faculties = g.Count(x => x.b != null)

                        });
            if (isActive == true)
            {
                return data.Where(e => e.Is_Active).ToList();
            }
            else if (isActive == false)
            {
                return data.Where(e => !e.Is_Active).ToList();
            }
            else
            {
                return data.ToList();
            }

        }

        public IEnumerable<GetAllBranchByFacultiesModel> GetAllBranchByFaculties()
        {
            var data = from a in _timelogContext.branch
                       join b in _timelogContext.faculties
                       on a.Branch_Id equals b.Branch_Id
                       where (a.Is_Active == true)
                       select new { a, b } into t1
                       group t1 by new { t1.a.Branch_Name, t1.a.Branch_Id, faccount = t1.b.Faculty_Id == null ? 0 : 1 }
                        into g
                       orderby g.Key.Branch_Name
                       select new GetAllBranchByFacultiesModel
                       {
                           Branch_Id = g.Key.Branch_Id,
                           Branch_Name = g.Key.Branch_Name,
                           No_Of_Faculties = g.Key.faccount == 0 ? 0 : g.Count()
                       };
            return data.ToList();
        }

        public IQueryable<Branch> GetAllBranch()

        {
            return _timelogContext.branch.Where(e => e.Is_Active == true).OrderBy(c => c.Branch_Name).AsQueryable();
        }

        //Subject

        public void AddSubject(AddSubjectsModel addSubjectsModel)
        {
            var name = _timelogContext.subjects.FirstOrDefault(p => p.Subject_Name == addSubjectsModel.Subject_Name);
            var code = _timelogContext.subjects.FirstOrDefault(e => e.Subject_Code == addSubjectsModel.Subject_Code);
            var branch = _timelogContext.branch.FirstOrDefault(c => c.Branch_Id == addSubjectsModel.Branch_Id);
            if (code == null)
            {
                if (name == null)
                {
                    if (branch != null)
                    {
                        var sub = new Subject();

                        sub.Subject_Name = addSubjectsModel.Subject_Name;
                        sub.Subject_Code = addSubjectsModel.Subject_Code;
                        sub.Branch_Id = addSubjectsModel.Branch_Id;
                        sub.Subject_Start_Date = addSubjectsModel.Subject_Start_Date;

                        sub.Create_Date = DateTime.UtcNow.Date;
                        sub.Is_Active = true;

                        _timelogContext.subjects.Add(sub);
                        _timelogContext.SaveChanges();
                    }
                    else
                    {
                        throw new BranchNotExistException();
                    }
                }
                else
                {
                    throw new SubjectNameExistException();
                }
            }
            else
            {
                throw new SubjectCodeExistException();
            }
        }

        public void EditSubject(EditSubjectsModel editSubjectsModel)
        {
            var IdCheck = _timelogContext.subjects.FirstOrDefault(p => p.Subject_Id == editSubjectsModel.Subject_Id);
            var doubleentry = _timelogContext.subjects.FirstOrDefault(e => e.Subject_Id != editSubjectsModel.Subject_Id &&
                                e.Subject_Name == editSubjectsModel.Subject_Name);
            var branch = _timelogContext.branch.FirstOrDefault(c => c.Branch_Id == editSubjectsModel.Branch_Id);

            if (IdCheck != null)
            {
                if (branch != null)
                {
                    if (doubleentry == null || doubleentry.Subject_Id == IdCheck.Subject_Id)
                    {
                        IdCheck.Subject_Name = editSubjectsModel.Subject_Name;
                        IdCheck.Subject_Code = IdCheck.Subject_Code  ;
                        IdCheck.Branch_Id = editSubjectsModel.Branch_Id;
                        IdCheck.Subject_Start_Date = IdCheck.Subject_Start_Date;
                        IdCheck.Subject_End_Date = editSubjectsModel.End_Date;
                        IdCheck.Modified_Date = DateTime.UtcNow.Date;
                        IdCheck.Is_Active = true;
                        _timelogContext.SaveChanges();

                    }
                    else
                    {
                        throw new SubjectNameExistException();
                    }
                }
                else
                {
                    throw new BranchNotExistException();
                }
            }
            else
            {
                throw new SubjectIdNotExistException();
            }
        }

        public void EditSubjectIsActive(IsActiveModel SubjectIsActiveModel, bool Is_Active)
        {
            var records = _timelogContext.subjects.Where(a => SubjectIsActiveModel.Id.Contains(a.Subject_Id));
            if (records.Count() != 0)
            {
                foreach (var r in records)
                {
                    r.Is_Active = Is_Active;
                }
                _timelogContext.SaveChanges();
            }
            else
            {
                throw new SubjectIdNotExistException();
            }
        }
        public IQueryable<Subject> GetBySubjectId(int id)
        {
            var subject = _timelogContext.subjects.AsQueryable();
            var item = _timelogContext.subjects.FirstOrDefault(d => d.Subject_Id == id);
            if (item != null)
            {
                return subject.Where(e => e.Subject_Id == id);
            }
            else
            {
                throw new SubjectIdNotExistException();
            }
        }

        //public IEnumerable<SubjectIsActiveModel> GetSubjectIsActive(bool? isActive)
        //{
        //    var data = from p in _timelogContext.subjects
        //               join ep in _timelogContext.semister
        //               on p.Project_Id equals ep.Project_Id into emp
        //               from e in emp.DefaultIfEmpty()
        //               select new { p, e } into t1
        //               group t1 by new
        //               {
        //                   t1.p.Project_Id,
        //                   t1.p.Project_Name,
        //                   t1.p.Project_Code,
        //                   t1.p.Client_Id,
        //                   t1.p.Project_Start_Date,
        //                   t1.p.Project_End_Date,
        //                   t1.p.Is_Active
        //               } into g
        //               orderby g.Key.Project_Name
        //               select new ProjectIsActiveModel
        //               {
        //                   Project_Id = g.Key.Project_Id,
        //                   Project_Name = g.Key.Project_Name,
        //                   Project_Code = g.Key.Project_Code,
        //                   Client_Id = g.Key.Client_Id,
        //                   Project_Start_Date = g.Key.Project_Start_Date,
        //                   Project_End_Date = g.Key.Project_End_Date,
        //                   Is_Active = g.Key.Is_Active,
        //                   No_Of_Employees = g.Count(x => x.e != null)
        //               };
        //    if (isActive == true)
        //    {
        //        return data.Where(e => e.Is_Active).ToList();
        //    }
        //    else if (isActive == false)
        //    {
        //        return data.Where(e => !e.Is_Active).ToList();
        //    }
        //    else
        //    {
        //        return data.ToList();
        //    }
        //}

        public IEnumerable<GetAllSubjectsByFacultiesModel> GetAllSubjectsByFaculties()
        {
            var data = (from a in _timelogContext.subjects
                        join b in _timelogContext.faculties
                        on a.Branch_Id equals b.Branch_Id
                        where (a.Is_Active == true)
                        select new { a, b } into t1
                        group t1 by new { t1.a.Subject_Name, t1.a.Subject_Id, t1.a.Subject_Code, t1.a.Subject_Start_Date, t1.a.Subject_End_Date }
                        into g
                        orderby g.Key.Subject_Name ascending
                        select new GetAllSubjectsByFacultiesModel
                        {
                            Subject_Id = g.Key.Subject_Id,
                            Subject_Name = g.Key.Subject_Name,
                            Subject_Code = g.Key.Subject_Code,
                            No_Of_Faculties = g.Count(),
                            Start_Date = g.Key.Subject_Start_Date,
                            End_Date = g.Key.Subject_End_Date,
                        });
            return data.ToList();
        }

        public IQueryable<Subject> GetAllSubjects()
        {
            return _timelogContext.subjects.Where(e => e.Is_Active == true).OrderBy(e => e.Subject_Name).AsQueryable();
        }

        //Designation
        public void AddDesignation(AddDesignationModel addDesignationModel)
        {
            var table = _timelogContext.designations.FirstOrDefault(e => e.Designation == addDesignationModel.Designation);
            if (table == null)
            {
                var data = new Designations();
                data.Designation = addDesignationModel.Designation;
                data.Create_Date = DateTime.UtcNow.Date;
                data.Is_Active = true;
                _timelogContext.designations.Add(data);
                _timelogContext.SaveChanges();
            }
            else
            {
                throw new DesignationNameException();
            }
        }

        public void EditDesignation(EditDesignationModel editDesignationModel)
        {
            var DesignationIdCheck = _timelogContext.designations.FirstOrDefault
                 (e => (e.Designation_Id == editDesignationModel.Designation_Id));
            var DesignationNameCheck = _timelogContext.designations.FirstOrDefault
                 (e => e.Designation_Id != editDesignationModel.Designation_Id && (e.Designation == editDesignationModel.Designation));

            if (DesignationNameCheck == null)
            {
                if (DesignationIdCheck != null)
                {
                    DesignationIdCheck.Designation = editDesignationModel.Designation;
                    DesignationIdCheck.Modified_Date = DateTime.UtcNow.Date;
                    _timelogContext.SaveChanges();
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

        public void EditDesignationIsActive(IsActiveModel DesignationIsActiveModel, bool Is_Active)
        {
            var records = _timelogContext.designations.Where(a => DesignationIsActiveModel.Id.Contains(a.Designation_Id));
            if (records.Count() != 0)
            {
                foreach (var r in records)
                {
                    r.Is_Active = Is_Active;
                }
                _timelogContext.SaveChanges();
            }
            else
            {
                throw new DesignationIdException();
            }
        }
        public IQueryable<Designations> GetByDesignationId(int id)
        {
            var designations = _timelogContext.designations.AsQueryable();
            var item = _timelogContext.designations.FirstOrDefault(d => d.Designation_Id == id);
            if (item != null)
            {
                return designations.Where(e => e.Designation_Id == id);
            }
            else
            {
                throw new DesignationIdException();
            }
        }

        public IEnumerable<DesignationIsActiveModel> GetDesignationIsActive(bool? isActive)
        {

            var data = (from a in _timelogContext.designations
                        join b in _timelogContext.faculties
                        on a.Designation_Id equals b.Designation_Id into faculties
                        from b in faculties.DefaultIfEmpty()
                        select new { a, b }
                        into t1
                        group t1 by new { t1.a.Designation, t1.a.Designation_Id, t1.a.Is_Active } into g
                        orderby g.Key.Designation

                        select new DesignationIsActiveModel
                        {
                            Designation_Id = g.Key.Designation_Id,
                            Designation = g.Key.Designation,
                            Is_Active = g.Key.Is_Active,
                            No_Of_Faculties = g.Count(x => x.b != null)

                        });

            if (isActive == true)
            {
                return data.Where(e => e.Is_Active).ToList();
            }
            else if (isActive == false)
            {
                return data.Where(e => !e.Is_Active).ToList();
            }
            else
            {
                return data.ToList();
            }
        }
        public IEnumerable<GetAllDesignationsByFacultiesModel> GetAllDesignationsByFaculties()
        {
            var data = from a in _timelogContext.designations
                       join b in _timelogContext.faculties
                       on a.Designation_Id equals b.Designation_Id
                       where (a.Is_Active == true)
                       select new { a, b } into t1
                       group t1 by new { t1.a.Designation, t1.a.Designation_Id, faccount = t1.b.Faculty_Id == null ? 0 : 1 }
                        into g
                       orderby g.Key.Designation
                       select new GetAllDesignationsByFacultiesModel
                       {
                           Designation_Id = g.Key.Designation_Id,
                           Designation = g.Key.Designation,
                           No_of_Faculties = g.Key.faccount == 0 ? 0 : g.Count()
                       };
            return data.ToList();
        }

        public IQueryable<Designations> GetAllDesignations()
        {
            return _timelogContext.designations.Where(e => e.Is_Active == true).OrderBy(e => e.Designation).AsQueryable();
        }

        //Faculty Type

        public void AddFacultyType(AddFacultyTypeModel addFacultyTypeModel)
        {
            var table = _timelogContext.facultytypes.FirstOrDefault(e => e.Faculty_Type == addFacultyTypeModel.Faculty_Type);

            if (table == null)
            {
                var data = new FacultyType();
                data.Faculty_Type = addFacultyTypeModel.Faculty_Type;
                data.Create_Date = DateTime.UtcNow.Date;
                data.Is_Active = true;

                _timelogContext.facultytypes.Add(data);
                _timelogContext.SaveChanges();
            }
            else
            {
                throw new FacultyTypeNameException();
            }
        }

        public void EditFacultyType(EditFacultyTypeModel editFacultyTypeModel)
        {
            var IdCheck = _timelogContext.facultytypes.FirstOrDefault
                 (e => (e.FacultyType_Id == editFacultyTypeModel.FacultyType_Id));
            var NameCheck = _timelogContext.facultytypes.FirstOrDefault
                 (e => (e.Faculty_Type == editFacultyTypeModel.Faculty_Type));

            if (NameCheck == null)
            {
                if (IdCheck != null)
                {
                    IdCheck.Faculty_Type = editFacultyTypeModel.Faculty_Type;
                    IdCheck.Modified_Date = DateTime.UtcNow.Date;
                    _timelogContext.SaveChanges();
                }
                else
                {
                    throw new FacultyTypeIdException();
                }
            }
            else
            {
                throw new FacultyTypeNameException();
            }
        }

        public void EditFacultyTypeIsActive(IsActiveModel FacultyTypeIsActiveModel, bool Is_Active)
        {
            var records = _timelogContext.facultytypes.Where(a => FacultyTypeIsActiveModel.Id.Contains(a.FacultyType_Id));
            if (records.Count() != 0)
            {
                foreach (var r in records)
                {
                    r.Is_Active = Is_Active;
                }
                _timelogContext.SaveChanges();
            }
            else
            {
                throw new FacultyTypeIdException();
            }
        }

        public IQueryable<FacultyType> GetByFacultyTypeId(int id)
        {
            var employeeType = _timelogContext.facultytypes.AsQueryable();
            var item = _timelogContext.facultytypes.FirstOrDefault(d => d.FacultyType_Id == id);
            if (item != null)
            {
                return employeeType.Where(e => e.FacultyType_Id == id);
            }
            else
            {
                throw new   FacultyTypeIdException();
            }
        }

        public IEnumerable<FacultyTypeIsActiveModel> GetFacultyTypeIsActive(bool? isActive)
        {

            var data = from a in _timelogContext.facultytypes
                       join b in _timelogContext.faculties
                       on a.FacultyType_Id equals b.FacultyType_Id into faculties
                       from b in faculties.DefaultIfEmpty()
                       select new { a, b } into t1
                       group t1 by new { t1.a.Faculty_Type, t1.a.FacultyType_Id, t1.a.Is_Active } into g
                       orderby g.Key.Faculty_Type
                       select new FacultyTypeIsActiveModel
                       {
                           FacultyType_Id = g.Key.FacultyType_Id,
                           Faculty_Type = g.Key.Faculty_Type,
                           Is_Active = g.Key.Is_Active,
                           No_Of_Faculties = g.Count(x => x.b != null)
                       };

            if (isActive == true)
            {
                return data.Where(e => e.Is_Active).ToList();
            }
            else if (isActive == false)
            {
                return data.Where(e => !e.Is_Active).ToList();
            }
            else
            {
                return data.ToList();
            }
        }

        public IEnumerable<GetAllFacultyTypeByFacultiesModel> GetAllFacultyTypeByFaculties()
        {
            var data = (from a in _timelogContext.facultytypes
                        join b in _timelogContext.faculties
                        on a.FacultyType_Id equals b.FacultyType_Id
                        where (a.Is_Active == true)
                        select new { a, b } into t1
                        group t1 by new { t1.a.Faculty_Type, t1.a.FacultyType_Id }
                         into g
                        orderby g.Key.Faculty_Type ascending
                        select new GetAllFacultyTypeByFacultiesModel
                        {
                            FacultyType_Id = g.Key.FacultyType_Id,
                            Faculty_Type = g.Key.Faculty_Type,
                            No_of_Faculties = g.Count()
                        });
            return data.ToList();
        }

        public IQueryable<FacultyType> GetAllFacultyTypes()
        {
            return _timelogContext.facultytypes.Where(e => e.Is_Active == true).OrderBy(e => e.Faculty_Type).AsQueryable();
        }

        //Role

        public void AddRole(AddRoleModel addRoleModel)
        {
            var table = _timesheetContext.roles.FirstOrDefault(e => e.Role == addRoleModel.Role);

            if (table == null)
            {
                var data = new Roles();
                data.Role = addRoleModel.Role;
                data.Create_Date = DateTime.UtcNow.Date;

                _timesheetContext.roles.Add(data);
                _timesheetContext.SaveChanges();
            }
            else
            {
                throw new RoleNameException();
            }
        }

        public void EditRole(EditRoleModel editRoleModel)
        {
            var IdCheck = _timesheetContext.roles.FirstOrDefault
                 (e => (e.Role_Id == editRoleModel.Role_Id));
            var NameCheck = _timesheetContext.roles.FirstOrDefault
                 (e => (e.Role == editRoleModel.Role));

            if (NameCheck == null)
            {
                if (IdCheck != null)
                {
                    IdCheck.Role = editRoleModel.Role;
                    IdCheck.Modified_Date = DateTime.UtcNow.Date;
                    _timesheetContext.SaveChanges();
                }
                else
                {
                    throw new RoleIdException();
                }
            }
            else
            {
                throw new RoleNameException();
            }
        }

        //Employee

        public void AddEmployee(AddEmployeeModel addEmployeeModel)
        {
            var EmailContactCheck = _timesheetContext.employees.FirstOrDefault(e => e.Official_Email == addEmployeeModel.Official_Email || e.Contact_No == addEmployeeModel.Contact_No);
            var ts = new TimeSheetSummary();
            if (EmailContactCheck == null)
            {
                if (_timesheetContext.designations.FirstOrDefault(e => e.Designation_Id == addEmployeeModel.Designation_Id) != null)
                {
                    if (_timesheetContext.employeeTypes.FirstOrDefault(e => e.Employee_Type_Id == addEmployeeModel.Employee_Type_Id) != null)
                    {
                        if (_timesheetContext.employees.FirstOrDefault(e => e.Employee_code == addEmployeeModel.Employee_code) == null)
                        {
                            if (_timesheetContext.employees.FirstOrDefault(e => e.Official_Email == addEmployeeModel.Alternate_Email) == null)
                            {
                                if (_timesheetContext.employees.FirstOrDefault(e => addEmployeeModel.Official_Email == addEmployeeModel.Alternate_Email) == null)
                                {
                                    var Role = _timesheetContext.designations.FirstOrDefault(e => e.Designation_Id == addEmployeeModel.Designation_Id);

                                    var emp = new Employee();
                                    emp.First_Name = addEmployeeModel.First_Name;
                                    emp.Last_Name = addEmployeeModel.Last_Name;
                                    emp.Employee_code = addEmployeeModel.Employee_code;
                                    emp.Reporting_Manager1 = addEmployeeModel.Reporting_Manager1;
                                    emp.Official_Email = addEmployeeModel.Official_Email;
                                    emp.Alternate_Email = addEmployeeModel.Alternate_Email;
                                    emp.Contact_No = addEmployeeModel.Contact_No;
                                    emp.Password = "Joyit@1234";
                                    string passwordHash = BCrypt.Net.BCrypt.HashPassword(emp.Password);
                                    emp.Hashpassword = passwordHash;
                                    emp.Designation_Id = addEmployeeModel.Designation_Id;
                                    emp.Employee_Type_Id = addEmployeeModel.Employee_Type_Id;
                                    emp.Is_Active = true;
                                    emp.Joining_Date = addEmployeeModel.Joining_Date;
                                    emp.Create_Date = DateTime.UtcNow.Date;
                                    emp.Otp = "1";

                                    if (Role.Designation.ToLower() == "hr" || Role.Designation.ToLower() == "human resource" || Role.Designation.ToLower() == " admin"
                                         || Role.Designation.ToLower() == "hr manager" || Role.Designation.ToLower() == "hr admin")
                                    {
                                        emp.Role_Id = 1;
                                    }
                                    else
                                    {
                                        emp.Role_Id = 2;
                                    }

                                    _timesheetContext.employees.Add(emp);
                                    _timesheetContext.SaveChanges();
                                    var fullname = emp.First_Name + " " + emp.Last_Name;
                                    string fromAddress = "Joyitsolutions1@gmail.com";
                                    string Password = "rpcfydphzeoafsig";
                                    string toAddress = addEmployeeModel.Official_Email;
                                    string emailHeader = "<html><body><h1>Congratulations</h1></body></html>";
                                    string emailFooter = $"<html><head><title>JoyItsolutions</title></head><body><p>Hi {fullname}, <br> This is the confidential email. Don't share your password with anyone..!<br> Click here to change your credentials or fill Timesheet  <a href=\"http://localhost:3000/\"> : http://localhost:3000/ </a> </p></body></html>";
                                    string emailBody = $"<html><head><title>Don't replay this Mail</title></head><body><p>Your password is: {emp.Password}</p></body></html>";
                                    string emailContent = emailHeader + emailBody + emailFooter;
                                    MailMessage message = new MailMessage();
                                    message.From = new MailAddress(fromAddress);
                                    message.Subject = "Welcome To JOY IT SOLUTIONS";
                                    message.To.Add(new MailAddress(toAddress));
                                    message.Body = emailContent;
                                    message.IsBodyHtml = true;

                                    var smtpClient = new System.Net.Mail.SmtpClient("smtp.gmail.com")
                                    {
                                        Port = 587,
                                        Credentials = new NetworkCredential(fromAddress, Password),
                                        EnableSsl = true,
                                    };

                                    smtpClient.Send(message);
                                }
                                else
                                {
                                    throw new OEmailAEmailSameException();
                                }
                            }
                            else
                            {
                                throw new OEmailAEmailExistException();
                            }
                        }
                        else
                        {
                            throw new EmployeeCodeExistException();
                        }
                    }
                    else
                    {
                        throw new EmployeeTypeIdException();
                    }
                }
                else
                {
                    throw new DesignationIdException();
                }
            }

            else
            {
                var a = _timesheetContext.employees.FirstOrDefault(e => ((e.Official_Email == addEmployeeModel.Official_Email || e.Alternate_Email == addEmployeeModel.Official_Email)) || e.Contact_No == addEmployeeModel.Contact_No);
                if (a.Official_Email == addEmployeeModel.Official_Email && a.Contact_No != addEmployeeModel.Contact_No)
                {
                    throw new EmployeeEmailExistException();
                }
                else if (a.Official_Email != addEmployeeModel.Official_Email && a.Contact_No == addEmployeeModel.Contact_No)
                {
                    throw new EmployeeContactExistException();
                }
                else
                {
                    throw new EmployeeEmailExistException();
                    throw new EmployeeContactExistException();
                }
            }
        }

        public void EditEmployee(EditEmployeeModel editEmployeeModel)
        {
            var IdCheck = _timesheetContext.employees.FirstOrDefault(e => e.Employee_Id == editEmployeeModel.Employee_Id);
            var doubleentry = _timesheetContext.employees.FirstOrDefault(e => e.Employee_Id != editEmployeeModel.Employee_Id && (e.Official_Email == editEmployeeModel.Official_Email || e.Contact_No == editEmployeeModel.Contact_No
            || e.Official_Email == editEmployeeModel.Alternate_Email || (e.Alternate_Email == editEmployeeModel.Alternate_Email && e.Alternate_Email != null)));
            var data = new ViewPreviousChanges();
            var hrContact = _timesheetContext.hrContactInformations.FirstOrDefault(e => e.Hr_Email_Id == editEmployeeModel.Official_Email);
            var Role = _timesheetContext.designations.FirstOrDefault(e => e.Designation == editEmployeeModel.Designation);
            if (IdCheck != null)
            {
                if (_timesheetContext.employees.FirstOrDefault(e => editEmployeeModel.Official_Email == editEmployeeModel.Alternate_Email) == null)
                {
                    if (_timesheetContext.employees.FirstOrDefault(e => e.Official_Email == editEmployeeModel.Alternate_Email) == null)
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
                            data.Role_Id = IdCheck.Role_Id;
                            data.Contact_No = IdCheck.Contact_No;
                            data.Reporting_Manager1 = IdCheck.Reporting_Manager1;
                            data.Is_Active = IdCheck.Is_Active;
                            //data.Joining_Date = IdCheck.Joining_Date;
                            data.End_Date = IdCheck.End_Date;
                            data.Modified_Date = IdCheck.Modified_Date;
                            _timesheetContext.viewPreviousChanges.Update(data);
                            _timesheetContext.SaveChanges();

                            IdCheck.Employee_Id = editEmployeeModel.Employee_Id;
                            IdCheck.First_Name = editEmployeeModel.First_Name;
                            IdCheck.Last_Name = editEmployeeModel.Last_Name;
                            IdCheck.Employee_Type_Id = editEmployeeModel.Employee_Type_Id;
                            IdCheck.Official_Email = editEmployeeModel.Official_Email;
                            IdCheck.Employee_code = editEmployeeModel.Employee_code;
                            IdCheck.Alternate_Email = editEmployeeModel.Alternate_Email;

                            if (Role.Designation.ToLower() == "hr" || Role.Designation.ToLower() == "human resource" || Role.Designation.ToLower() == " admin"
                             || Role.Designation.ToLower() == "hr manager" || Role.Designation.ToLower() == "hr admin")
                            {
                                IdCheck.Role_Id = 1;
                            }
                            else
                            {
                                IdCheck.Role_Id = 2;
                            }

                            var des = _timesheetContext.designations.FirstOrDefault(e => e.Designation == editEmployeeModel.Designation);
                            var empType = _timesheetContext.employeeTypes.FirstOrDefault(e => e.Employee_Type == editEmployeeModel.Employee_Type);

                            if (des != null)
                            {
                                IdCheck.Designation_Id = des.Designation_Id;
                            }
                            if (empType != null)
                            {
                                IdCheck.Employee_Type_Id = empType.Employee_Type_Id;
                            }

                            IdCheck.Contact_No = editEmployeeModel.Contact_No;
                            IdCheck.Reporting_Manager1 = editEmployeeModel.Reporting_Manager1;
                            //IdCheck.Joining_Date = editEmployeeModel.Joining_Dates;
                            IdCheck.End_Date = editEmployeeModel.End_Date;
                            IdCheck.Modified_Date = DateTime.Now.Date;
                            _timesheetContext.SaveChanges();

                            if (_timesheetContext.hrContactInformations.FirstOrDefault(e => e.Hr_Email_Id == editEmployeeModel.Official_Email) != null)
                            {
                                hrContact.First_Name = editEmployeeModel.First_Name;
                                hrContact.Last_Name = editEmployeeModel.Last_Name;
                                hrContact.Hr_Email_Id = editEmployeeModel.Official_Email;
                                hrContact.Hr_Contact_No = editEmployeeModel.Contact_No;
                                _timesheetContext.SaveChanges();
                            }
                        }
                        else
                        {
                            var e = _timesheetContext.employees.FirstOrDefault(e => e.Official_Email != editEmployeeModel.Official_Email);
                            var c = _timesheetContext.employees.FirstOrDefault(e => e.Contact_No != editEmployeeModel.Contact_No);
                            if (e.Official_Email == editEmployeeModel.Official_Email)
                            {
                                throw new EmployeeEmailExistException();
                            }
                            else if (c.Official_Email == editEmployeeModel.Official_Email)
                            {
                                throw new EmployeeContactExistException();
                            }
                        }
                    }
                    else
                    {
                        throw new OEmailAEmailExistException();
                    }
                }
                else
                {
                    throw new OEmailAEmailSameException();
                }
            }
            else
            {
                throw new EmployeeIdNotExistException();
            }
        }

        public void EditEmployeeIsActive(IsActiveModel EmployeIsActiveModel, bool Is_Active)
        {

            var records = _timesheetContext.employees.Where(a => EmployeIsActiveModel.Id.Contains(a.Employee_Id));
            if (records.Count() != 0)
            {
                foreach (var r in records)
                {
                    r.Is_Active = Is_Active;
                }
                _timesheetContext.SaveChanges();
            }
            else
            {
                throw new EmployeeIdNotExistException();
            }

        }
        public IQueryable<Employee> GetByEmployeeId(int id)
        {
            var employee = _timesheetContext.employees.AsQueryable();
            var item = _timesheetContext.employees.FirstOrDefault(d => d.Employee_Id == id);
            if (item != null)
            {
                return employee.Where(e => e.Employee_Id == id);
            }
            else
            {
                throw new EmployeeIdNotExistException();
            }
        }

        public IEnumerable<EmployeeIsActiveModel> GetEmployeeIsActive(bool? isActive)
        {

            var data = from Emp in _timesheetContext.employees
                       join Des in this._timesheetContext.designations
                       on Emp.Designation_Id equals Des.Designation_Id
                       join ty in this._timesheetContext.employeeTypes
                       on Emp.Employee_Type_Id equals ty.Employee_Type_Id
                       orderby Emp.First_Name
                       select new EmployeeIsActiveModel
                       {
                           Employee_Id = Emp.Employee_Id,
                           First_Name = Emp.First_Name,
                           Last_Name = Emp.Last_Name,
                           Full_Name = Emp.First_Name + " " + Emp.Last_Name,
                           Employee_code = Emp.Employee_code,
                           Reporting_Manager1 = Emp.Reporting_Manager1,
                           Employee_Type = ty.Employee_Type,
                           Official_Email = Emp.Official_Email,
                           Role_Id = Emp.Role_Id,
                           Designation = Des.Designation,
                           Contact_No = Emp.Contact_No,
                           Joining_Date = Emp.Joining_Date.Date,
                           End_Date = Emp.End_Date.HasValue ?
                                      Emp.End_Date.Value : DateTime.MinValue.Date,
                           Is_Active = Emp.Is_Active
                       };
            if (isActive == true)
            {
                return data.Where(e => e.Is_Active).ToList();
            }
            else if (isActive == false)
            {
                return data.Where(e => !e.Is_Active).ToList();
            }
            else
            {
                return data.ToList();
            }
        }

        public List<GetAllEmployeeByDesIdEmpTypeIdModel> GetAllEmployeeByDesIdEmpTypeId()
        {
            var data = from Emp in _timesheetContext.employees
                       join Des in _timesheetContext.designations
                       on Emp.Designation_Id equals Des.Designation_Id
                       join EmpTy in _timesheetContext.employeeTypes
                       on Emp.Employee_Type_Id equals EmpTy.Employee_Type_Id
                       where Emp.Is_Active == true
                       orderby Emp.First_Name ascending
                       select new GetAllEmployeeByDesIdEmpTypeIdModel
                       {
                           Employee_Id = Emp.Employee_Id,
                           First_Name = Emp.First_Name,
                           last_Name = Emp.Last_Name,
                           Full_Name = Emp.First_Name + " " + Emp.Last_Name,
                           Employee_code = Emp.Employee_code,
                           Reporting_Manager1 = Emp.Reporting_Manager1,
                           Employee_Type_Id = Emp.Employee_Type_Id,
                           Employee_Type = EmpTy.Employee_Type,
                           Official_Email = Emp.Official_Email,
                           Alternate_Email = Emp.Alternate_Email,

                           Designation_Id = Emp.Designation_Id,
                           Designation = Des.Designation,
                           Contact_No = Emp.Contact_No,
                           Joining_Date = Emp.Joining_Date,
                           End_Date = Emp.End_Date.HasValue ?
                                      Emp.End_Date.Value : DateTime.MinValue
                       };
            return data.ToList();
        }

        public IQueryable<Employee> GetAllEmployees()
        {
            return _timesheetContext.employees.Where(e => e.Is_Active == true).OrderBy(e => e.First_Name).AsQueryable();
        }
    }
}
