using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Time_Log.Exceptions
{
    public class TimeLogException
    {
        //Branch
        public class BranchIdException : Exception
        {
            public BranchIdException() { }
            public BranchIdException(string message) : base(message) { }
        }
        public class BranchNameExistException : Exception
        {
            public BranchNameExistException() { }
            public BranchNameExistException(string message) : base(message) { }
        }

        //Subject
        public class SubjectIdNotExistException : Exception
        {
            public SubjectIdNotExistException() { }
            public SubjectIdNotExistException(string message) : base(message) { }
        }
        public class SubjectNameExistException : Exception
        {
            public SubjectNameExistException() { }
            public SubjectNameExistException(string message) : base(message) { }
        }
        public class SubjectCodeExistException : Exception
        {
            public SubjectCodeExistException() { }
            public SubjectCodeExistException(string message) : base(message) { }
        }
        public class BranchNotExistException : Exception
        {
            public BranchNotExistException() { }
            public BranchNotExistException(string message) : base(message) { }
        }

        //Designation
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

        //FacultyType
        public class FacultyTypeIdException : Exception
        {
            public FacultyTypeIdException() { }
            public FacultyTypeIdException(string message) : base(message) { }
        }
        public class FacultyTypeNameException : Exception
        {
            public FacultyTypeNameException() { }
            public FacultyTypeNameException(string message) : base(message) { }
        }

        //Role
        public class RoleIdException : Exception
        {
            public RoleIdException() { }
            public RoleIdException(string message) : base(message) { }
        }
        public class RoleNameException : Exception
        {
            public RoleNameException() { }
            public RoleNameException(string message) : base(message) { }
        }

        //Faculties
        public class FacultiesIdNotExistException : Exception
        {
            public FacultiesIdNotExistException() { }
            public FacultiesIdNotExistException(string message) : base(message) { }
        }
        public class FacultiesEmailExistException : Exception
        {
            public FacultiesEmailExistException() { }
            public FacultiesEmailExistException(string message) : base(message) { }
        }
        public class FacultiesContactExistException : Exception
        {
            public FacultiesContactExistException() { }
            public FacultiesContactExistException(string message) : base(message) { }
        }
        public class FacultiesCodeExistException : Exception
        {
            public FacultiesCodeExistException() { }
            public FacultiesCodeExistException(string message) : base(message) { }
        }
        public class OEmailAEmailSameException : Exception
        {
            public OEmailAEmailSameException() { }
            public OEmailAEmailSameException(string message) : base(message) { }
        }
        public class OEmailAEmailExistException : Exception
        {
            public OEmailAEmailExistException() { }
            public OEmailAEmailExistException(string message) : base(message) { }
        }

       

        //HODContactInfo
        public class HODIdException : Exception
        {
            public HODIdException() { }
            public HODIdException(string message) : base(message) { }
        }
        public class HODMailExistException : Exception
        {
            public HODMailExistException() { }
            public HODMailExistException(string message) : base(message) { }
        }
        public class HODMailNotExistException : Exception
        {
            public HODMailNotExistException() { }
            public HODMailNotExistException(string message) : base(message) { }
        }
        public class HODContactException : Exception
        {
            public HODContactException() { }
            public HODContactException(string message) : base(message) { }
        }




        public class CustomExceptionFilterAttribute : ExceptionFilterAttribute
        {
            public override void OnException(ExceptionContext context)
            {
                switch (context.Exception)
                {
                    //Branch
                    case BranchIdException:
                        context.Result = new BadRequestObjectResult("No branch has the entered id");
                        break;
                    case BranchNameExistException:
                        context.Result = new BadRequestObjectResult("Branch Name already exists");
                        break;

                    //Subject
                    case SubjectIdNotExistException:
                        context.Result = new BadRequestObjectResult("No subject with the entered id");
                        break;
                    case SubjectNameExistException:
                        context.Result = new BadRequestObjectResult("Subject Name already exists");
                        break;
                    case SubjectCodeExistException:
                        context.Result = new BadRequestObjectResult("Subject Code already exists");
                        break;
                    case BranchNotExistException:
                        context.Result = new BadRequestObjectResult("No Branch with the entered id");
                        break;

                    //Designation
                    case DesignationIdException:
                        context.Result = new BadRequestObjectResult("No designation has the entered id");
                        break;
                    case DesignationNameException:
                        context.Result = new BadRequestObjectResult("Designation already exists");
                        break;

                    //FacultyType
                    case FacultyTypeIdException:
                        context.Result = new BadRequestObjectResult("No faculty type has the entered id");
                        break;
                    case FacultyTypeNameException:
                        context.Result = new BadRequestObjectResult("Faculty Type already exists");
                        break;

                    //Role
                    case RoleIdException:
                        context.Result = new BadRequestObjectResult("No role has the entered id");
                        break;
                    case RoleNameException:
                        context.Result = new BadRequestObjectResult("Role already exists");
                        break;

                    //Faculties
                    case FacultiesIdNotExistException:
                        context.Result = new BadRequestObjectResult("No Faculty with the entered id");
                        break;
                    case FacultiesEmailExistException:
                        context.Result = new BadRequestObjectResult("Official mail already exists");
                        break;
                    case FacultiesContactExistException:
                        context.Result = new BadRequestObjectResult("Contact Number already exists");
                        break;
                    case FacultiesCodeExistException:
                        context.Result = new BadRequestObjectResult("Faculty Code already exists");
                        break;
                    case OEmailAEmailSameException:
                        context.Result = new BadRequestObjectResult("Faculty Alternate Email should not be same as Official Email");
                        break;
                    case OEmailAEmailExistException:
                        context.Result = new BadRequestObjectResult("Alternate Email already exists");
                        break;

                    //HODContactInfo
                    case HODIdException:
                        context.Result = new BadRequestObjectResult("No HOD has the entered id");
                        break;

                    case HODMailExistException:
                        context.Result = new BadRequestObjectResult("Email already exists");
                        break;

                    case HODMailNotExistException:
                        context.Result = new BadRequestObjectResult("Email does not exists");
                        break;

                    case HODContactException:
                        context.Result = new BadRequestObjectResult("Contact already exists");
                        break;




                    case ArgumentNullException:
                        context.Result = new BadRequestObjectResult("Argument Null Exception exception occurred.");
                        break;
                }
                context.ExceptionHandled = true;
            }
        }
    }
}