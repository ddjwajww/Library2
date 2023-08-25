using LibraryManagementMvcUI.Areas.AdminPanel.Models.ApiTypes;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;

namespace LibraryManagementMvcUI.Areas.AdminPanel.ViewComponents
{
    public class SideBarViewComponent : ViewComponent
    {
        public IViewComponentResult Invoke()
        {
            var sessionData = HttpContext.Session.GetString("ActiveAdminPanelUser");

            var admin = JsonSerializer.Deserialize<AdminPanelUserItem>(sessionData);

            return View(admin);
        }
    }
}
