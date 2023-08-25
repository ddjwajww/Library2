using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace LibraryManagementMvcUI.Areas.AdminPanel.Filters
{
    public class SessionControlAspect : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            var sessionData = context.HttpContext.Session.GetString("ActiveAdminPanelUser");

            if (string.IsNullOrEmpty(sessionData))
                context.Result = new RedirectToActionResult("LogIn", "Auth", null);
        }        
    }
}
