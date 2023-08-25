using LibraryManagementMvcUI.ApiService;
using LibraryManagementMvcUI.Areas.AdminPanel.Models.ApiTypes;
using LibraryManagementMvcUI.Areas.AdminPanel.Models.Dtos;
using LibraryManagementMvcUI.Areas.AdminPanel.Models.Dtos.Category;
using LibraryManagementMvcUI.Areas.AdminPanel.Models.Dtos.User;
using LibraryManagementMvcUI.Areas.AdminPanel.Models.ViewModels;
using LibraryManagementMvcUI.Filters;
using LibraryManagementMvcUI.Models.Dtos.User;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;
using LogInDto = LibraryManagementMvcUI.Models.Dtos.User.LogInDto;
using NewUserDto = LibraryManagementMvcUI.Models.Dtos.User.NewUserDto;

namespace LibraryManagementMvcUI.Controllers
{
    
    public class UserController : Controller
    {
        private readonly IHttpApiService _apiService;

        public UserController(IHttpApiService apiService)
        {
            _apiService = apiService;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> LogIn(LogInDto dto)
        {
            string endPoint = $"/users/login?userName={dto.UserName}&password={dto.Password}";

            var response = await _apiService.GetData<ResponseBody<UserItem>>(endPoint);

            if (response.StatusCode == 200)
            {               

                HttpContext.Session.SetString("ActiveUser", JsonSerializer.Serialize(response.Data));

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

        [HttpPost]
        public async Task<IActionResult> Save(NewUserDto dto)
        {           

            var postData = new
            {
                FullName = dto.FullName ?? "default",
                UserName = dto.UserName ?? "default",
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

        public async Task GetTokenAndSetInSession()
        {
            var response = await _apiService.GetData<ResponseBody<AccessTokenItem>>($"/adminpanelusers/gettoken");

            HttpContext.Session.SetString("AccessToken", JsonSerializer.Serialize(response.Data));
        }

        public async Task<IActionResult> Home()
        {
            var jsonTokenData = HttpContext.Session.GetString("AccessToken");

            var jwt = JsonSerializer.Deserialize<AccessTokenItem>(jsonTokenData);

            var responseMsgCat = await _apiService.GetData<ResponseBody<List<CategoryItem>>>("/categories", jwt.Token);
            var responseBook = await _apiService.GetData<ResponseBody<List<BookItem>>>("/books", jwt.Token);
            var resbb = await _apiService.GetData<ResponseBody<List<BorrowedBookItem>>>("/borrowedbooks", jwt.Token);
            var reser = await _apiService.GetData<ResponseBody<List<ReservationItem>>>("/reservations", jwt.Token);

            var sessionData = HttpContext.Session.GetString("ActiveUser");

            var admin = JsonSerializer.Deserialize<UserItem>(sessionData);

            BookIndexViewModel vm = new BookIndexViewModel();

            vm.CategoryList = responseMsgCat.Data;
            vm.BooktList = responseBook.Data;
            vm.ActiveUserId = admin.UserId;
            vm.BorrowedBookList = resbb.Data;
            vm.ReservationList = reser.Data;

            return View(vm);

        }
    }
}
