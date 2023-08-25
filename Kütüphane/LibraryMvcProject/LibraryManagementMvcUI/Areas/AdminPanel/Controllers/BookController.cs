using LibraryManagementMvcUI.ApiService;
using LibraryManagementMvcUI.Areas.AdminPanel.Filters;
using LibraryManagementMvcUI.Areas.AdminPanel.Models.ApiTypes;
using LibraryManagementMvcUI.Areas.AdminPanel.Models.Dtos.Book;
using LibraryManagementMvcUI.Areas.AdminPanel.Models.Dtos.Category;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;

namespace LibraryManagementMvcUI.Areas.AdminPanel.Controllers
{
    [Area("AdminPanel")]
    [SessionControlAspect]
    public class BookController : Controller
    {
        private readonly IHttpApiService _apiService;
        private readonly IWebHostEnvironment _webHostEnvironment;
        public BookController(IHttpApiService apiService, IWebHostEnvironment webHostEnvironment)
        {
            _apiService = apiService;
            _webHostEnvironment = webHostEnvironment;
        }

        public async Task<IActionResult> Index()
        {

            var jsonTokenData = HttpContext.Session.GetString("AccessToken");

            var jwt = JsonSerializer.Deserialize<AccessTokenItem>(jsonTokenData);

            var response = await _apiService.GetData<ResponseBody<List<BookItem>>>("/books", jwt.Token);

            return View(response.Data);

        }

        [HttpPost]
        public async Task<IActionResult> Save(NewBookDto dto, IFormFile bookImage)
        {
            //categoryImage.ContentType --> images/jpg (MIME TYPE)
            //categoryImage.FileName    --> 4098
            //categoryImage.Length      --> ayva.jpg

            if (bookImage == null)
                return Json(new { IsSuccess = false, Message = "Kitap Resmi Seçilmelidir." });

            if (!bookImage.ContentType.StartsWith("image/"))
                return Json(new { IsSuccess = false, Message = "Sadece resim dosyası seçilmelidir." });

            if (bookImage.Length > 1024 * 250)
                return Json(new { IsSuccess = false, Message = "Dosya boyutu en fazla 250 kb olmalı" });
            // eğer clientdan gelen resim bilgisini tabloya sadece binary olarak kaydeceksek, bu resim bilgisini base64 e çevirip servise göndeririz,
            //servis tarafında da bu base64ü byte[] e dönüştürüp tabloya insert edilmesini sağlarız.

            //eğer clientdan gelen resim bilgisini bir klasörde saklayıp  tabloya da yolunun tutulmasını istiyorsak o zaman sunucya bu dosyayı upload et


            // guid (global unique identifier) evrensel tekil tanımlayıcı : benzersiz eşsiz bir değer yaratmamızı sağlıyor
            var randomFileName = $"{Guid.NewGuid()}{Path.GetExtension(bookImage.FileName)}";
            var uploadPath = $@"{_webHostEnvironment.WebRootPath}/adminPanel/bookImages/{randomFileName}";

            using (var fs = new FileStream(uploadPath, FileMode.Create))
            {
                await bookImage.CopyToAsync(fs);
            }

            var postData = new
            {
                BookId = dto.BookId,
                Title = dto.Title,
                PublishDate = dto.PublishDate,
                AuthorId = dto.AuthorId,
                CategoryId = dto.CategoryId,                
                TotalCopies = dto.TotalCopies,
                AvailableCopies = dto.AvailableCopies,
                PicturePath = $@"/adminPanel/bookImages/{randomFileName}"

            };
            

            var response = await _apiService.PostData<ResponseBody<BookItem>>("/books", JsonSerializer.Serialize(postData));

            if (response.StatusCode == 200)
                return Json(new { IsSuccess = true, Message = "Kitap BAŞARIYLA KAYDEDİLDİ" });
            if (response.ErrorMessages == null)
                return Json(new { IsSuccess = true, Message = "Kitap BAŞARIYLA KAYDEDİLDİ" });

            var errorMessages = string.Empty;
            foreach (var item in response.ErrorMessages)
            {
                errorMessages += item + "<br/>";
            }

            return Json(new { IsSuccess = false, Message = $"Kitap Kaydedilemedi. <br/> {errorMessages}" });
        }

        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            var response = await _apiService.DeleteData($"/books/{id}");

            if (response)
                return Json(new { IsSuccess = true, Message = "Kitap Silindi" });

            return Json(new { IsSuccess = false, Message = "Kitap Silinemedi" });

        }
        [HttpPost]
        public async Task<IActionResult> GetBook(int id)
        {
            var response = await _apiService.GetData<ResponseBody<BookItem>>($"/books/{id}");

            return Json(new
            {
                BookId = response.Data.BookId,
                Title = response.Data.Title,
                PublishDate = response.Data.PublishDate.ToShortDateString(),
                AuthorId = response.Data.AuthorId,
                CategoryId = response.Data.CategoryId,
                PicturePath = response.Data.PicturePath,
                AvailableCopies = response.Data.AvailableCopies,
                TotalCopies = response.Data.TotalCopies

            });
        }

        [HttpPost]
        public async Task<IActionResult> Update(UpdateBookDto dto, IFormFile bookImage)
        {
            if (bookImage == null)
                return Json(new { IsSuccess = false, Message = "Kategori Resmi Seçilmelidir." });

            if (!bookImage.ContentType.StartsWith("image/"))
                return Json(new { IsSuccess = false, Message = "Sadece resim dosyası seçilmelidir." });

            if (bookImage.Length > 1024 * 250)
                return Json(new { IsSuccess = false, Message = "Dosya boyutu en fazla 250 kb olmalı" });

            var randomFileName = $"{Guid.NewGuid()}{Path.GetExtension(bookImage.FileName)}";
            var uploadPath = $@"{_webHostEnvironment.WebRootPath}/adminPanel/bookImages/{randomFileName}";

            using (var fs = new FileStream(uploadPath, FileMode.Create))
            {
                await bookImage.CopyToAsync(fs);
            }

            var putData = new
            {               
                BookId = dto.BookId,
                Title = dto.Title,
                PublishDate = dto.PublishDate,
                AuthorId = dto.AuthorId,
                CategoryId = dto.CategoryId,
                TotalCopies = dto.TotalCopies,
                AvailableCopies = dto.AvailableCopies,
                PicturePath = $@"/adminPanel/bookImages/{randomFileName}"

            };

            var response = await _apiService.PutData<ResponseBody<BookItem>>($"/books", JsonSerializer.Serialize(putData));

            if (response.StatusCode == 200)
                return Json(new { IsSuccess = true, Message = "Kitap BAŞARIYLA GÜNCELLENDİ" });
            if (response.ErrorMessages == null)
                return Json(new { IsSuccess = true, Message = "Kitap BAŞARIYLA GÜNCELLENDİ" });

            var errorMessages = string.Empty;
            foreach (var item in response.ErrorMessages)
            {
                errorMessages += item + "<br/>";
            }

            return Json(new { IsSuccess = false, Message = $"Kitap Güncellenemedi. <br/> {errorMessages}" });
        }

    }
}
