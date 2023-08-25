using Humanizer;
using LibraryManagementMvcUI.ApiService;
using LibraryManagementMvcUI.Areas.AdminPanel.Filters;
using LibraryManagementMvcUI.Areas.AdminPanel.Models.ApiTypes;
using LibraryManagementMvcUI.Areas.AdminPanel.Models.Dtos.BorrowedBook;
using LibraryManagementMvcUI.Areas.AdminPanel.Models.Dtos.Reservation;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;

namespace LibraryManagementMvcUI.Areas.AdminPanel.Controllers
{
    [Area("AdminPanel")]
    [SessionControlAspect]
    public class ReservationController :Controller
    {
        private readonly IHttpApiService _apiService;
        public ReservationController(IHttpApiService apiService)
        {
            _apiService = apiService;
        }
        public async Task<IActionResult> Index()
        {

            var jsonTokenData = HttpContext.Session.GetString("AccessToken");

            var jwt = JsonSerializer.Deserialize<AccessTokenItem>(jsonTokenData);

            var response = await _apiService.GetData<ResponseBody<List<ReservationItem>>>("/reservations", jwt.Token);

            return View(response.Data);

        }

        [HttpPost]
        public async Task<IActionResult> Save(NewReservationDto dto)
        {

            var postData = new
            {
                UserId = dto.UserId,
                BookId = dto.BookId,
                ExpirationDate = dto.ExpirationDate,
                ReservationDate = dto.ReservationDate,
                Active = dto.Active

            };

            var response = await _apiService.PostData<ResponseBody<ReservationItem>>("/reservations", JsonSerializer.Serialize(postData));

            if (response.StatusCode == 200)
                return Json(new { IsSuccess = true, Message = "Rezervasyon BAŞARIYLA KAYDEDİLDİ" });
            if (response.ErrorMessages == null)
                return Json(new { IsSuccess = true, Message = "Rezervasyon BAŞARIYLA KAYDEDİLDİ" });

            var errorMessages = string.Empty;
            foreach (var item in response.ErrorMessages)
            {
                errorMessages += item + "<br/>";
            }

            return Json(new { IsSuccess = false, Message = $"Rezervasyon Kaydedilemedi. <br/> {errorMessages}" });

        }

        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            var response = await _apiService.DeleteData($"/reservations/{id}");

            if (response)
                return Json(new { IsSuccess = true, Message = "Rezervasyon Silindi" });

            return Json(new { IsSuccess = false, Message = "Rezervasyon Silinemedi" });

        }

        [HttpPost]
        public async Task<IActionResult> GetReservation(int id)
        {
            var response = await _apiService.GetData<ResponseBody<ReservationItem>>($"/reservations/{id}");

            return Json(new
            {
                ReservationId = response.Data.ReservationId,
                UserId = response.Data.UserId,
                BookId = response.Data.BookId,
                ExpirationDate = response.Data.ExpirationDate,
                ReservationDate = response.Data.ReservationDate,
                Active = response.Data.Active
            });
        }

        [HttpPost]
        public async Task<IActionResult> Update(UpdateReservationDto dto)
        {

            var putData = new
            {
                ReservationId = dto.ReservationId,
                UserId = dto.UserId,
                BookId = dto.BookId,
                ExpirationDate = dto.ExpirationDate,
                ReservationDate = dto.ReservationDate,
                Active = dto.Active
            };

            var response = await _apiService.PutData<ResponseBody<ReservationItem>>($"/reservations", JsonSerializer.Serialize(putData));

            if (response.StatusCode == 200)
                return Json(new { IsSuccess = true, Message = "Rezervasyon BAŞARIYLA GÜNCELLENDİ" });
            if (response.ErrorMessages == null)
                return Json(new { IsSuccess = true, Message = "Rezervasyon BAŞARIYLA GÜNCELLENDİ" });

            var errorMessages = string.Empty;
            foreach (var item in response.ErrorMessages)
            {
                errorMessages += item + "<br/>";
            }

            return Json(new { IsSuccess = false, Message = $"Rezervasyon Güncellenemedi. <br/> {errorMessages}" });
        }
    }
}
