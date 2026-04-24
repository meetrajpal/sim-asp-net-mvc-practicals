using System;
using System.Web.Mvc;

namespace Test4.Models.Filters
{
    public class CustomDivideByZeroFilter : HandleErrorAttribute
    {
        public override void OnException(ExceptionContext filterContext)
        {
            if (filterContext.Exception is DivideByZeroException)
            {
                filterContext.ExceptionHandled = true;
                filterContext.Result = new ViewResult
                {
                    ViewName = "Error",
                    ViewData = new ViewDataDictionary
                    {
                        { "ErrorMessage", "Second number can not be zero while performing division." },
                        { "Title", "Error" }
                    }
                };
            }
        }
    }
}