using Microsoft.AspNetCore.Mvc.Filters;

namespace LibraryManagementMvcUI.Areas.AdminPanel.Filters
{
    public class LogAspect :ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            // metot çalışmaya başladığında
        }

        public override void OnResultExecuting(ResultExecutingContext context)
        {
            // metot geriye değer dönmeden hemen önce
        }
    }
}
