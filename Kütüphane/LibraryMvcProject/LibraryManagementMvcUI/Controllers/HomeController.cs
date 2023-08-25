using LibraryManagementMvcUI.ApiService;
using LibraryManagementMvcUI.Areas.AdminPanel.Models.ApiTypes;
using LibraryManagementMvcUI.Areas.AdminPanel.Models.ViewModels;
using LibraryManagementMvcUI.Filters;
using LibraryManagementMvcUI.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.Text.Json;

namespace LibraryManagementMvcUI.Controllers
{
    [SessionControlAspectUser]
    public class HomeController : Controller
    {
        private readonly IHttpApiService _apiService;
        public HomeController(IHttpApiService apiService)
        {
            _apiService = apiService;
        }


        public async Task<IActionResult> Index()
        {

            var jsonTokenData = HttpContext.Session.GetString("AccessToken");

            var jwt = JsonSerializer.Deserialize<AccessTokenItem>(jsonTokenData);

            var responseBook = await _apiService.GetData<ResponseBody<List<BookItem>>>("/books",jwt.Token);
            var responseCat = await _apiService.GetData<ResponseBody<List<CategoryItem>>>("/categories", jwt.Token);

            BookIndexViewModel vm = new BookIndexViewModel();

            vm.CategoryList = responseCat.Data;
            vm.BooktList = responseBook.Data;            

            return View(vm);
        }

       
    }
}