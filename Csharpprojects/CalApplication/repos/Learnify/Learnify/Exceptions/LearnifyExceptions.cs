using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc;

namespace Learnify.Exceptions
{
    public class EmailPhonenoException : Exception
    {
        public EmailPhonenoException() { }
        public EmailPhonenoException(string message) : base(message) { }
    }
    public class ConfirmpasswordException : Exception
    {
        public ConfirmpasswordException() { }
        public ConfirmpasswordException(string message) : base(message) { }
    }


    public class CustomExceptionFilterAttribute : ExceptionFilterAttribute
    {
        public override void OnException(ExceptionContext context)
        {
            switch (context.Exception)
            {
                //Client
                case EmailPhonenoException:
                    context.Result = new BadRequestObjectResult("Email or Phone Number already Exists");
                    break;
                case ConfirmpasswordException:
                    context.Result = new BadRequestObjectResult("Confirm password does not match");
                    break;
                case ArgumentNullException:
                    context.Result = new BadRequestObjectResult("Argument Null Exception exception occurred.");
                    break;
            }
            context.ExceptionHandled = true;
        }
    }
}


