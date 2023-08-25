using LibraryManagementMvcUI.ApiService;
using LibraryManagementMvcUI.Areas.AdminPanel.Filters;
using LibraryManagementMvcUI.Areas.AdminPanel.Models.ApiTypes;
using LibraryManagementMvcUI.Areas.AdminPanel.Models.Dtos.Category;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;

namespace LibraryManagementMvcUI.Areas.AdminPanel.Controllers
{
    [Area("AdminPanel")]
    [SessionControlAspect]

    public class CategoryController : Controller
    {
        private readonly IHttpApiService _apiService;
        private readonly IWebHostEnvironment _webHostEnvironment;
        public CategoryController(IHttpApiService apiService, IWebHostEnvironment webHostEnvironment)
        {
            _apiService = apiService;
            _webHostEnvironment = webHostEnvironment;
        }

        public async Task<IActionResult> Index()
        {
            var jsonTokenData = HttpContext.Session.GetString("AccessToken");

            var jwt = JsonSerializer.Deserialize<AccessTokenItem>(jsonTokenData);

            var response = await _apiService.GetData<ResponseBody<List<CategoryItem>>>("/categories", jwt.Token);

            return View(response.Data);

        }

        [HttpPost]
        public async Task<IActionResult> Save(NewCategoryDto dto, IFormFile categoryImage)
        {
            //categoryImage.ContentType --> images/jpg (MIME TYPE)
            //categoryImage.FileName    --> 4098
            //categoryImage.Length      --> ayva.jpg

            if (categoryImage == null)
                return Json(new { IsSuccess = false, Message = "Kategori Resmi Seçilmelidir." });

            if (!categoryImage.ContentType.StartsWith("image/"))
                return Json(new { IsSuccess = false, Message = "Sadece resim dosyası seçilmelidir." });

            if (categoryImage.Length > 1024 * 250)
                return Json(new { IsSuccess = false, Message = "Dosya boyutu en fazla 250 kb olmalı" });
            // eğer clientdan gelen resim bilgisini tabloya sadece binary olarak kaydeceksek, bu resim bilgisini base64 e çevirip servise göndeririz,
            //servis tarafında da bu base64ü byte[] e dönüştürüp tabloya insert edilmesini sağlarız.

            //eğer clientdan gelen resim bilgisini bir klasörde saklayıp  tabloya da yolunun tutulmasını istiyorsak o zaman sunucya bu dosyayı upload et


            // guid (global unique identifier) evrensel tekil tanımlayıcı : benzersiz eşsiz bir değer yaratmamızı sağlıyor
            var randomFileName = $"{Guid.NewGuid()}{Path.GetExtension(categoryImage.FileName)}";
            var uploadPath = $@"{_webHostEnvironment.WebRootPath}/adminPanel/categoryImages/{randomFileName}";

            using (var fs = new FileStream(uploadPath, FileMode.Create))
            {
                await categoryImage.CopyToAsync(fs);
            }

            var postData = new
            {

                CategoryName = dto.CategoryName ?? "default",
                Description = dto.Description ?? "default",
                PicturePath = $@"/adminPanel/categoryImages/{randomFileName}"                

            };

            var response = await _apiService.PostData<ResponseBody<CategoryItem>>("/categories", JsonSerializer.Serialize(postData));

            if (response.StatusCode == 200)
                return Json(new { IsSuccess = true, Message = "KATEGORİ BAŞARIYLA KAYDEDİLDİ" });
            if (response.ErrorMessages == null)
                return Json(new { IsSuccess = true, Message = "KATEGORİ BAŞARIYLA KAYDEDİLDİ" });

            var errorMessages = string.Empty;
            foreach (var item in response.ErrorMessages)
            {
                errorMessages += item + "<br/>";
            }

            return Json(new { IsSuccess = false, Message = $"Kategori Kaydedilemedi. <br/> {errorMessages}" });

        }

        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            var response = await _apiService.DeleteData($"/categories/{id}");

            if (response)
                return Json(new { IsSuccess = true, Message = "Kategori Silindi" });

            return Json(new { IsSuccess = false, Message = "Kategori Silinemedi" });

        }
        [HttpPost]
        public async Task<IActionResult> GetCategory(int id)
        {
            var response = await _apiService.GetData<ResponseBody<CategoryItem>>($"/categories/{id}");

            return Json(new
            {
                CategoryName = response.Data.CategoryName,
                Description = response.Data.Description,
                PicturePath = response.Data.PicturePath,
                CategoryId = response.Data.CategoryId
            });
        }

        [HttpPost]
        public async Task<IActionResult> Update(UpdateCategoryDto dto, IFormFile categoryImage)
        {
            if (categoryImage == null)
                return Json(new { IsSuccess = false, Message = "Kategori Resmi Seçilmelidir." });

            if (!categoryImage.ContentType.StartsWith("image/"))
                return Json(new { IsSuccess = false, Message = "Sadece resim dosyası seçilmelidir." });

            if (categoryImage.Length > 1024 * 250)
                return Json(new { IsSuccess = false, Message = "Dosya boyutu en fazla 250 kb olmalı" });

            var randomFileName = $"{Guid.NewGuid()}{Path.GetExtension(categoryImage.FileName)}";
            var uploadPath = $@"{_webHostEnvironment.WebRootPath}/adminPanel/categoryImages/{randomFileName}";

            using (var fs = new FileStream(uploadPath, FileMode.Create))
            {
                await categoryImage.CopyToAsync(fs);
            }

            var putData = new 
            {
                CategoryId = dto.CategoryId,
                CategoryName = dto.CategoryName ?? "default",
                Description = dto.Description ?? "default",
                PicturePath = $@"/adminPanel/categoryImages/{randomFileName}"

            };

            var response = await _apiService.PutData<ResponseBody<CategoryItem>>($"/categories", JsonSerializer.Serialize(putData));

            if (response.StatusCode == 200)
                return Json(new { IsSuccess = true, Message = "KATEGORİ BAŞARIYLA GÜNCELLENDİ" });
            if (response.ErrorMessages == null)
                return Json(new { IsSuccess = true, Message = "KATEGORİ BAŞARIYLA GÜNCELLENDİ" });

            var errorMessages = string.Empty;
            foreach (var item in response.ErrorMessages)
            {
                errorMessages += item + "<br/>";
            }

            return Json(new { IsSuccess = false, Message = $"Kategori Güncellenemedi. <br/> {errorMessages}" });
        }
    }
}
