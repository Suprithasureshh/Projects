using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc;

namespace TimeSheet.ExceptionFilter
{
    public class ClientIdException : Exception
    {
        public ClientIdException() { }
        public ClientIdException(string message) : base(message) { }
    }
    public class DesignationIdException : Exception
    {
        public DesignationIdException() { }
        public DesignationIdException(string message) : base(message) { }
    }

    public class DesignationNameException : Exception
    {
        public DesignationNameException() { }
        public DesignationNameException(string message) : base(message) { }
    }

    public class EmployeeTypeIdException : Exception
    {
        public EmployeeTypeIdException() { }
        public EmployeeTypeIdException(string message) : base(message) { }
    }
    public class EmployeeTypeNameException : Exception
    {
        public EmployeeTypeNameException() { }
        public EmployeeTypeNameException(string message) : base(message) { }
    }


    public class CustomExceptionFilterAttribute : Microsoft.AspNetCore.Mvc.Filters.ExceptionFilterAttribute
    {
        public override void OnException(ExceptionContext context)
        {
            switch (context.Exception)
            {
                case ClientIdException:
                    context.Result = new BadRequestObjectResult("No client has the entered id");
                    break;

                case DesignationIdException:
                    context.Result = new BadRequestObjectResult("No designation has the entered id");
                    break;
                case DesignationNameException:
                    context.Result = new BadRequestObjectResult("Designation already exists");
                    break;

                case EmployeeTypeIdException:
                    context.Result = new BadRequestObjectResult("No employee type has the entered id");
                    break;
                case EmployeeTypeNameException:
                    context.Result = new BadRequestObjectResult("Employee Type already exists");
                    break;


                case ArgumentNullException:
                    context.Result = new BadRequestObjectResult("Argument Null Exception exception occurred.");
                    break;
            }
            context.ExceptionHandled = true;
        }
    }
}
