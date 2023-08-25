using LibraryManagementMvcUI.ApiService;
using LibraryManagementMvcUI.Areas.AdminPanel.Filters;
using LibraryManagementMvcUI.Areas.AdminPanel.Models.ApiTypes;
using LibraryManagementMvcUI.Areas.AdminPanel.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;

namespace LibraryManagementMvcUI.Areas.AdminPanel.Controllers
{
    [Area("AdminPanel")]
    [SessionControlAspect]

    public class HomeController : Controller
    {
        private readonly IHttpApiService _apiService;        

        public HomeController(IHttpApiService apiService)
        {
            _apiService = apiService;           

        }

        [LogAspect]
        public async Task<IActionResult> Index()
        {

            var jsonTokenData = HttpContext.Session.GetString("AccessToken");

            var jwt = JsonSerializer.Deserialize<AccessTokenItem>(jsonTokenData);

            var cat = await _apiService.GetData<ResponseBody<List<CategoryItem>>>("/categories",jwt.Token);
            var user = await _apiService.GetData<ResponseBody<List<UserItem>>>("/users", jwt.Token);
            var book = await _apiService.GetData<ResponseBody<List<BookItem>>>("/books", jwt.Token);
            var aut = await _apiService.GetData<ResponseBody<List<AuthorItem>>>("/authors", jwt.Token);

            HomeIndexViewModel vm = new HomeIndexViewModel();

            vm.CategoryList = cat.Data;
            vm.UserList = user.Data;
            vm.BooktList = book.Data;
            vm.AuthorList = aut.Data;


            return View(vm);
        }
    }
}
