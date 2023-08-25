using LibraryManagementMvcUI.ApiService;
using LibraryManagementMvcUI.Areas.AdminPanel.Filters;
using LibraryManagementMvcUI.Areas.AdminPanel.Models.ApiTypes;
using LibraryManagementMvcUI.Areas.AdminPanel.Models.Dtos.Author;
using LibraryManagementMvcUI.Areas.AdminPanel.Models.Dtos.Category;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;

namespace LibraryManagementMvcUI.Areas.AdminPanel.Controllers
{
    [Area("AdminPanel")]
    [SessionControlAspect]
    public class AuthorController : Controller
    {       

       
        private readonly IHttpApiService _apiService;
        private readonly IWebHostEnvironment _webHostEnvironment;
        public AuthorController(IHttpApiService apiService, IWebHostEnvironment webHostEnvironment)
        {
            _apiService = apiService;
            _webHostEnvironment = webHostEnvironment;
        }

        public async Task<IActionResult> Index()
        {

            var jsonTokenData = HttpContext.Session.GetString("AccessToken");

            var jwt = JsonSerializer.Deserialize<AccessTokenItem>(jsonTokenData);

            var response = await _apiService.GetData<ResponseBody<List<AuthorItem>>>("/authors", jwt.Token);

            return View(response.Data); 

        }

        [HttpPost]
        public async Task<IActionResult> Save(NewAuthorDto dto, IFormFile authorImage)
        {
            //categoryImage.ContentType --> images/jpg (MIME TYPE)
            //categoryImage.FileName    --> 4098
            //categoryImage.Length      --> ayva.jpg

            if (authorImage == null)
                return Json(new { IsSuccess = false, Message = "Yazar Resmi Seçilmelidir." });

            if (!authorImage.ContentType.StartsWith("image/"))
                return Json(new { IsSuccess = false, Message = "Sadece resim dosyası seçilmelidir." });

            if (authorImage.Length > 1024 * 250)
                return Json(new { IsSuccess = false, Message = "Dosya boyutu en fazla 250 kb olmalı" });
            // eğer clientdan gelen resim bilgisini tabloya sadece binary olarak kaydeceksek, bu resim bilgisini base64 e çevirip servise göndeririz,
            //servis tarafında da bu base64ü byte[] e dönüştürüp tabloya insert edilmesini sağlarız.

            //eğer clientdan gelen resim bilgisini bir klasörde saklayıp  tabloya da yolunun tutulmasını istiyorsak o zaman sunucya bu dosyayı upload et


            // guid (global unique identifier) evrensel tekil tanımlayıcı : benzersiz eşsiz bir değer yaratmamızı sağlıyor
            var randomFileName = $"{Guid.NewGuid()}{Path.GetExtension(authorImage.FileName)}";
            var uploadPath = $@"{_webHostEnvironment.WebRootPath}/adminPanel/authorImages/{randomFileName}";

            using (var fs = new FileStream(uploadPath, FileMode.Create))
            {
                await authorImage.CopyToAsync(fs);
            }

            var postData = new
            {

                AuthorName = dto.AuthorName ?? "default",
                BirthDate = dto.BirthDate,
                PicturePath = $@"/adminPanel/authorImages/{randomFileName}"

            };

            var response = await _apiService.PostData<ResponseBody<AuthorItem>>("/authors", JsonSerializer.Serialize(postData));

            if (response.StatusCode == 200)
                return Json(new { IsSuccess = true, Message = "Yazar BAŞARIYLA KAYDEDİLDİ" });
            if (response.ErrorMessages == null)
                return Json(new { IsSuccess = true, Message = "Yazar BAŞARIYLA KAYDEDİLDİ" });

            var errorMessages = string.Empty;
            foreach (var item in response.ErrorMessages)
            {
                errorMessages += item + "<br/>";
            }

            return Json(new { IsSuccess = false, Message = $"Yazar Kaydedilemedi. <br/> {errorMessages}" });

        }


        [HttpPost]
        public async Task<IActionResult> GetAuthor(int id)
        {
            var response = await _apiService.GetData<ResponseBody<AuthorItem>>($"/authors/{id}");

            return Json(new
            {
                AuthorName = response.Data.AuthorName,
                BirthDate = response.Data.BirthDate.Value.ToShortDateString(),
                PicturePath = response.Data.PicturePath,
                AuthorId = response.Data.AuthorId
            });
        }

        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            var response = await _apiService.DeleteData($"/authors/{id}");

            if (response)
                return Json(new { IsSuccess = true, Message = "Yazar Silindi" });

            return Json(new { IsSuccess = false, Message = "Yazar Silinemedi" });

        }

        [HttpPost]
        public async Task<IActionResult> Update(UpdateAuthorDto dto, IFormFile authorImage)
        {
            if (authorImage == null)
                return Json(new { IsSuccess = false, Message = "Yazar Resmi Seçilmelidir." });

            if (!authorImage.ContentType.StartsWith("image/"))
                return Json(new { IsSuccess = false, Message = "Sadece resim dosyası seçilmelidir." });

            if (authorImage.Length > 1024 * 250)
                return Json(new { IsSuccess = false, Message = "Dosya boyutu en fazla 250 kb olmalı" });

            var randomFileName = $"{Guid.NewGuid()}{Path.GetExtension(authorImage.FileName)}";
            var uploadPath = $@"{_webHostEnvironment.WebRootPath}/adminPanel/authorImages/{randomFileName}";

            using (var fs = new FileStream(uploadPath, FileMode.Create))
            {
                await authorImage.CopyToAsync(fs);
            }

            var putData = new
            {
                AuthorId = dto.AuthorId,
                AuthorName = dto.AuthorName ?? "default",
                BirthDate = dto.BirthDate ,
                PicturePath = $@"/adminPanel/authorImages/{randomFileName}"

            };

            var response = await _apiService.PutData<ResponseBody<AuthorItem>>($"/authors", JsonSerializer.Serialize(putData));

            if (response.StatusCode == 200)
                return Json(new { IsSuccess = true, Message = "Yazar BAŞARIYLA GÜNCELLENDİ" });
            if (response.ErrorMessages == null)
                return Json(new { IsSuccess = true, Message = "Yazar BAŞARIYLA GÜNCELLENDİ" });

            var errorMessages = string.Empty;
            foreach (var item in response.ErrorMessages)
            {
                errorMessages += item + "<br/>";
            }

            return Json(new { IsSuccess = false, Message = $"Yazar Güncellenemedi. <br/> {errorMessages}" });
        }
    }
}
