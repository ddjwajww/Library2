using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc;

namespace LibraryManagementMvcUI.Filters
{
    public class SessionControlAspectUser : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            var sessionData = context.HttpContext.Session.GetString("ActiveUser");

            if (string.IsNullOrEmpty(sessionData))
                context.Result = new RedirectToActionResult("LogIn", "User", null);
        }        
    
    }
}
