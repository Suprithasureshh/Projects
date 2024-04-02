using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Data;
using System.IdentityModel.Tokens.Jwt;
using TimesheetPoject.Context_Timesheet;
using TimesheetPoject.Interface;
using TimesheetPoject.Model;

namespace TimesheetPoject.Repository
{
    public class UploadRepo : ControllerBase, IUploadInterface
    {
        private readonly Timesheet_Context _context;
        public UploadRepo(Timesheet_Context con)
        {
            _context = con;
        }

        //public void add(UploadModel[] entries)
        //{
        //    DateTime now = DateTime.Now;
        //    int daysInMonth = DateTime.DaysInMonth(now.Year, now.Month);

        //    for (var i = 0; i <= daysInMonth; i++)
        //    {
        //        var model = new UploadModel();
        //        model.total_hours = entries[i].total_hours;
        //        model.Date = DateTime.Now.Date;
        //        model.Day = entries[i].Day;
        //        model.Status = entries[i].Status;
        //        _context.TS_table.Add(model);               
        //    }
        //    _context.SaveChanges();

        //}


        public void add(UploadModel[] entries)
        {
            DateTime now = DateTime.Now;
            var daysInMonth = DateTime.DaysInMonth(now.Year, now.Month);

            foreach (var entry in entries)
            {
                var model = new UploadModel();
                model.total_hours = entry.total_hours;
                model.Date = DateTime.Now.Date;
                model.Day = entry.Day;
                model.Status = entry.Status;
                _context.TS_table.Add(model);
            }

            _context.SaveChanges();
        }
        public IActionResult add1(EmployeeModel[] entries)
        {
            var login=new LoginModel();
            var user = _context.Register.SingleOrDefault(u => u.UserId == login.UserId && u.Password == login.Password);

            //var user = _context.Register.SingleOrDefault(u => u.UserId == userId && u.Password == passw);

            if (user == null)
            {
                return Unauthorized();
            }

            var model1 = new EmployeeModel
            {
                UserId = user.UserId,
                Employee_Name = user.Username,
                Employee_Email = user.Email,
                Joining_date = user.DateOfJoin,
                Phone_Number = user.PhoneNumber
            };
            _context.SaveChanges();
            return Ok();           
        }
        public List<EmployeeModel> GetEmpDet()
        {
            var data = from t in this._context.ETS_table
                       select new EmployeeModel
                       {
                           Id = t.Id,
                           UserId= t.UserId,
                           Employee_Name=t.Employee_Name,
                           Employee_Email= t.Employee_Email,
                           Joining_date= t.Joining_date,
                           Phone_Number= t.Phone_Number   
                       };
            return data.ToList();
        }
        public List<UploadModel> GetTSDet()
        {
            var data = from t in this._context.TS_table
                       select new UploadModel
                       {
                           Id = t.Id,
                           Date= t.Date,
                           Day= t.Day,                         
                           Status= t.Status,
                           total_hours =t.total_hours
                       };
            return data.ToList();
        }

    }
}