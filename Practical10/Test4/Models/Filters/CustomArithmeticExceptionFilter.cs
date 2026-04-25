using System;
using System.Web.Mvc;

namespace Test4.Models.Filters
{
    public class CustomArithmeticExceptionFilter : HandleErrorAttribute
    {
        public override void OnException(ExceptionContext filterContext)
        {
            if (filterContext.Exception is ArithmeticException)
            {
                filterContext.ExceptionHandled = true;

                var session = filterContext.HttpContext.Session;
                if (session != null)
                {
                    if (filterContext.Exception is DivideByZeroException)
                    {
                        session["ErrorMessage"] = "Second number can not be zero while performing division.";
                    }
                    else
                    {
                        session["ErrorMessage"] = $"{filterContext.Exception.Message} \t {filterContext.Exception.InnerException?.Message}";
                    }
                }

                filterContext.Result = new RedirectResult("Home/Index");
            }
        }
    }
}