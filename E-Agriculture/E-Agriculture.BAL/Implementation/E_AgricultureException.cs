using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc;

namespace E_Agriculture.BAL.Implementation
{

    public class ConfirmpasswordException : Exception
    {
        public ConfirmpasswordException() { }
        public ConfirmpasswordException(string message) : base(message) { }
    }


    public class EmailException : Exception
    {
        public EmailException() { }
        public EmailException(string message) : base(message) { }
    }
    public class UserAsException : Exception
    {
        public UserAsException() { }
        public UserAsException(string message) : base(message) { }
    }
    public class InvalidOtp : Exception
    {
        public InvalidOtp() { }
        public InvalidOtp(string message) : base(message) { }
    }
    public class DataNotFound : Exception
    {
        public DataNotFound() { }
        public DataNotFound(string message) : base(message) { }
    }
    public class DeleteRecord : Exception
    {
        public DeleteRecord() { }
        public DeleteRecord(string message) : base(message) { }
    }
    public class Password : Exception
    {
        public Password() { }
        public Password(string message) : base(message) { }
    }

    public class CustomExceptionFilterAttribute : ExceptionFilterAttribute
    {
        public override void OnException(ExceptionContext context)
        {
            switch (context.Exception)
            {
                //Client

                case EmailException:
                    context.Result = new BadRequestObjectResult("Entered Email is Wrong.");
                    break;
                case UserAsException:
                    context.Result = new BadRequestObjectResult("Entered UserAs is Wrong Please check it once.");
                    break;
                case ConfirmpasswordException:
                    context.Result = new BadRequestObjectResult("Confirm password does not match");
                    break;

                case InvalidOtp:
                    context.Result = new BadRequestObjectResult("Entered OTP is InValid");
                    break;
                case DataNotFound:
                    context.Result = new BadRequestObjectResult("There is no data with entered Id");
                    break;
                case DeleteRecord:
                    context.Result = new BadRequestObjectResult("Record deleted successfully");
                    break;
                case Password:
                    context.Result = new BadRequestObjectResult("Password not found");
                    break;
                case ArgumentNullException:
                    context.Result = new BadRequestObjectResult("Argument Null Exception exception occurred.");
                    break;

            }
            context.ExceptionHandled = true;
        }
    }
}
