using Employee.Migrations;
using Employee.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.DependencyModel;
using System;

namespace Employee
{
    public class LoggingFilter : IActionFilter
    {
        private readonly ILogger<LoggingFilter> _filter;

        public LoggingFilter(ILogger<LoggingFilter> filter)
        {
            _filter = filter;
        }

        public void OnActionExecuting(ActionExecutingContext context)
        {
            _filter.LogInformation("Action starting:{ActionName}", context.HttpContext.Request.Path);
        }

        public void OnActionExecuted(ActionExecutedContext context)
        {
            _filter.LogInformation("Action Finished:{ActionName}", context.HttpContext.Response.StatusCode);
        }
    }
   




}
