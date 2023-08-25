using Humanizer;
using LibraryManagementMvcUI.ApiService;
using LibraryManagementMvcUI.Areas.AdminPanel.Filters;
using LibraryManagementMvcUI.Areas.AdminPanel.Models.ApiTypes;
using LibraryManagementMvcUI.Areas.AdminPanel.Models.Dtos.Category;
using LibraryManagementMvcUI.Areas.AdminPanel.Models.Dtos.User;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;

namespace LibraryManagementMvcUI.Areas.AdminPanel.Controllers
{
    [Area("AdminPanel")]
    [SessionControlAspect]
    public class UserController : Controller
    {
        private readonly IHttpApiService _apiService;
        private readonly IWebHostEnvironment _webHostEnvironment;
        public UserController(IHttpApiService apiService, IWebHostEnvironment webHostEnvironment)
        {
            _apiService = apiService;
            _webHostEnvironment = webHostEnvironment;
        }

        public async Task<IActionResult> Index()
        {

            var jsonTokenData = HttpContext.Session.GetString("AccessToken");

            var jwt = JsonSerializer.Deserialize<AccessTokenItem>(jsonTokenData);

            var response = await _apiService.GetData<ResponseBody<List<UserItem>>>("/users", jwt.Token);

            return View(response.Data);

        }

        [HttpPost]
        public async Task<IActionResult> Save(NewUserDto dto)
        {
           
            var postData = new
            {
                UserName = dto.UserName ?? "default",
                FullName = dto.FullName ?? "default",
                Email = dto.Email ?? "default",
                Password = dto.Password ?? "default"

            };

            var response = await _apiService.PostData<ResponseBody<UserItem>>("/users", JsonSerializer.Serialize(postData));

            if (response.StatusCode == 200)
                return Json(new { IsSuccess = true, Message = "Kullanıcı BAŞARIYLA KAYDEDİLDİ" });
            if (response.ErrorMessages == null)
                return Json(new { IsSuccess = true, Message = "Kullanıcı BAŞARIYLA KAYDEDİLDİ" });

            var errorMessages = string.Empty;
            foreach (var item in response.ErrorMessages)
            {
                errorMessages += item + "<br/>";
            }

            return Json(new { IsSuccess = false, Message = $"Kullanıcı Kaydedilemedi. <br/> {errorMessages}" });

        }

        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            var response = await _apiService.DeleteData($"/users/{id}");

            if (response)
                return Json(new { IsSuccess = true, Message = "Kullanıcı Silindi" });

            return Json(new { IsSuccess = false, Message = "Kullanıcı Silinemedi" });

        }

        [HttpPost]
        public async Task<IActionResult> GetUser(int id)
        {
            var response = await _apiService.GetData<ResponseBody<UserItem>>($"/users/{id}");

            return Json(new
            {
                UserId = response.Data.UserId,
                UserName = response.Data.UserName ?? "default",
                FullName = response.Data.FullName ?? "default",
                Email = response.Data.Email ?? "default",
                Password = response.Data.Password ?? "default"
            });
        }

        [HttpPost]
        public async Task<IActionResult> Update(UpdateUserDto dto)
        {
            
            var putData = new
            {
                UserId = dto.UserId,
                UserName = dto.UserName ?? "default",
                FullName = dto.FullName ?? "default",
                Email = dto.Email ?? "default",
                Password = dto.Password ?? "default"

            };

            var response = await _apiService.PutData<ResponseBody<UserItem>>($"/users", JsonSerializer.Serialize(putData));

            if (response.StatusCode == 200)
                return Json(new { IsSuccess = true, Message = "Kullanıcı Başarıyla Güncellendi" });
            if (response.ErrorMessages == null)
                return Json(new { IsSuccess = true, Message = "Kullanıcı Başarıyla Güncellendi" });

            var errorMessages = string.Empty;
            foreach (var item in response.ErrorMessages)
            {
                errorMessages += item + "<br/>";
            }

            return Json(new { IsSuccess = false, Message = $"Kullanıcı Güncellenemedi. <br/> {errorMessages}" });
        }
    }
}
