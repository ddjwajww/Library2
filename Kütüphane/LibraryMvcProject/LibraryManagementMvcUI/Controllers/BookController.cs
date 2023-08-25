using LibraryManagementMvcUI.ApiService;
using LibraryManagementMvcUI.Areas.AdminPanel.Models.ApiTypes;
using LibraryManagementMvcUI.Areas.AdminPanel.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;

namespace LibraryManagementMvcUI.Controllers
{
    public class BookController : Controller
    {
        private readonly IHttpApiService _apiService;
        public BookController(IHttpApiService apiService)
        {
            _apiService = apiService;
        }

        
        public async Task<IActionResult> Index(int? id)
        {
            var jsonTokenData = HttpContext.Session.GetString("AccessToken");

            var jwt = JsonSerializer.Deserialize<AccessTokenItem>(jsonTokenData);

            var responseMsgCat = await _apiService.GetData<ResponseBody<List<CategoryItem>>>("/categories",jwt.Token);


            var responseMsgBook =
                id == null
                ? await _apiService.GetData<ResponseBody<List<BookItem>>>("/books" ,jwt.Token)
                : await _apiService.GetData<ResponseBody<List<BookItem>>>($"/books/bycategoryid?category={id}");

            BookIndexViewModel vm = new BookIndexViewModel();

            vm.CategoryList = responseMsgCat.Data;
            vm.BooktList = responseMsgBook.Data;
            vm.ActiveCategoryId = id;

            return View(vm);
        }
    }
}
