using LibraryManagementMvcUI.Areas.AdminPanel.Models.ApiTypes;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;

namespace LibraryManagementMvcUI.ViewModels
{
    public class NavBarViewComponent :ViewComponent
    {
        public IViewComponentResult Invoke()
        {
            var sessionData = HttpContext.Session.GetString("ActiveUser");

            var admin = JsonSerializer.Deserialize<UserItem>(sessionData);

            return View(admin);
        }
    }
}
