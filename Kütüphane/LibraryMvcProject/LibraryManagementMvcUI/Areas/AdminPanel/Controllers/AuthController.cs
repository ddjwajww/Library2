using LibraryManagementMvcUI.ApiService;
using LibraryManagementMvcUI.Areas.AdminPanel.Models.ApiTypes;
using LibraryManagementMvcUI.Areas.AdminPanel.Models.Dtos;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;

namespace LibraryManagementMvcUI.Areas.AdminPanel.Controllers
{
    [Area("AdminPanel")]
    public class AuthController : Controller
    {
        private readonly IHttpApiService _apiService;

        public AuthController(IHttpApiService apiService)
        {
            _apiService = apiService;
        }
        public IActionResult LogIn()
        {
            return View();
        }
        

        [HttpPost]
        public async Task<IActionResult> LogIn(LogInDto dto)
        {

            string endPoint = $"/adminpanelusers/login?userName={dto.UserName}&password={dto.Password}";

            var response = await _apiService.GetData<ResponseBody<AdminPanelUserItem>>(endPoint);

            if (response.StatusCode == 200)
            {
                HttpContext.Session.SetString("ActiveAdminPanelUser", JsonSerializer.Serialize(response.Data));

                // Servise gidip token alacağız, bu token'ı session'a kaydedeceğiz,
                // sonra iç sayfalarda token lazım olduğunda sesiondan token'ı okuyup servise göndereceğiz
                await GetTokenAndSetInSession();

                return Json(new { isSuccess = true, Messages = "Kullanıcı Bulundu." });

            }
            else
            {
                return Json(new { isSuccess = false, Messages = response.ErrorMessages });
            }
        }

        public async Task GetTokenAndSetInSession()
        {
            var response = await _apiService.GetData<ResponseBody<AccessTokenItem>>($"/adminpanelusers/gettoken");

            HttpContext.Session.SetString("AccessToken", JsonSerializer.Serialize(response.Data));
        }
    }
    
}
