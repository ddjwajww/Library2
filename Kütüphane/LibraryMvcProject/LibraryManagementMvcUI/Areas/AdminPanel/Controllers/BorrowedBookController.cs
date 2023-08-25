using Humanizer;
using LibraryManagementMvcUI.ApiService;
using LibraryManagementMvcUI.Areas.AdminPanel.Filters;
using LibraryManagementMvcUI.Areas.AdminPanel.Models.ApiTypes;
using LibraryManagementMvcUI.Areas.AdminPanel.Models.Dtos.BorrowedBook;
using LibraryManagementMvcUI.Areas.AdminPanel.Models.Dtos.Category;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;

namespace LibraryManagementMvcUI.Areas.AdminPanel.Controllers
{
    [Area("AdminPanel")]
    [SessionControlAspect]
    public class BorrowedBookController:Controller
    {
        private readonly IHttpApiService _apiService;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public BorrowedBookController(IHttpApiService apiService, IWebHostEnvironment webHostEnvironment)
        {
            _apiService = apiService;
            _webHostEnvironment = webHostEnvironment;

        }
        public async Task<IActionResult> Index()
        {

            var jsonTokenData = HttpContext.Session.GetString("AccessToken");

            var jwt = JsonSerializer.Deserialize<AccessTokenItem>(jsonTokenData);

            var response = await _apiService.GetData<ResponseBody<List<BorrowedBookItem>>>("/borrowedbooks", jwt.Token);

            return View(response.Data);

        }

        [HttpPost]
        public async Task<IActionResult> Save(NewBorrowedBookDto dto)
        {           

            var postData = new
            {
                UserId = dto.UserId ,
                BookId = dto.BookId ,
                BorrowDate = dto.BorrowDate ,
                ReturnDate = dto.ReturnDate ,
                Returned = dto.Returned                 

            };

            var response = await _apiService.PostData<ResponseBody<BorrowedBookItem>>("/borrowedbooks", JsonSerializer.Serialize(postData));

            if (response.StatusCode == 200)
                return Json(new { IsSuccess = true, Message = "Ödünç Kitap BAŞARIYLA KAYDEDİLDİ" });
            if (response.ErrorMessages == null)
                return Json(new { IsSuccess = true, Message = "Ödünç Kitap BAŞARIYLA KAYDEDİLDİ" });

            var errorMessages = string.Empty;
            foreach (var item in response.ErrorMessages)
            {
                errorMessages += item + "<br/>";
            }

            return Json(new { IsSuccess = false, Message = $"Ödünç Kitap Kaydedilemedi. <br/> {errorMessages}" });

        }

        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            var response = await _apiService.DeleteData($"/borrowedbooks/{id}");

            if (response)
                return Json(new { IsSuccess = true, Message = "Ödünç Kitap Silindi" });

            return Json(new { IsSuccess = false, Message = "Ödünç Kitap Silinemedi" });

        }

        [HttpPost]
        public async Task<IActionResult> GetBorrowedBook(int id)
        {
            var response = await _apiService.GetData<ResponseBody<BorrowedBookItem>>($"/borrowedbooks/{id}");

            return Json(new
            {
                BorrowedBookId = response.Data.BorrowedBookId,
                UserId = response.Data.UserId,
                BookId = response.Data.BookId,
                BorrowDate = response.Data.BorrowDate,
                ReturnDate = response.Data.ReturnDate,
                Returned = response.Data.Returned
            });
        }

        [HttpPost]
        public async Task<IActionResult> Update(UpdateBorrowedBookDto dto)
        {            

            var putData = new
            {
                BorrowedBookId = dto.BorrowedBookId,
                UserId = dto.UserId,
                BookId = dto.BookId,
                BorrowDate = dto.BorrowDate,
                ReturnDate = dto.ReturnDate,
                Returned = dto.Returned
            };

            var response = await _apiService.PutData<ResponseBody<BorrowedBookItem>>($"/borrowedbooks", JsonSerializer.Serialize(putData));

            if (response.StatusCode == 200)
                return Json(new { IsSuccess = true, Message = "Ödünç Kitap BAŞARIYLA GÜNCELLENDİ" });
            if (response.ErrorMessages == null)
                return Json(new { IsSuccess = true, Message = "Ödünç Kitap BAŞARIYLA GÜNCELLENDİ" });

            var errorMessages = string.Empty;
            foreach (var item in response.ErrorMessages)
            {
                errorMessages += item + "<br/>";
            }

            return Json(new { IsSuccess = false, Message = $"Ödünç Kitap Güncellenemedi. <br/> {errorMessages}" });
        }
    }
}
