using Infrastructure.Utilities.ApiResponses;
using LM.Business.Interfaces;
using LM.Model.Dtos.Book;
using LM.Model.Dtos.BorrowedBook;
using LM.Model.Dtos.Reservation;
using LM.Model.Dtos.User;
using LM.Model.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace LM.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReservationsController : BaseController
    {
        private readonly IReservationBs _reservationBs;

        public ReservationsController(IReservationBs reservationBs)
        {
             _reservationBs = reservationBs;
        }

        #region SWAGGER DOC
        [Produces("application/json", "text/plain")]
        [ProducesResponseType(200, Type = typeof(ApiResponse<List<ReservationGetDto>>))]
        [ProducesResponseType(404, Type = typeof(ApiResponse<NoData>))]
        #endregion
        [HttpGet]
        [Authorize]
        public async Task<ActionResult> GetAllReservations()
        {
            var response = await _reservationBs.GetReservationAsync("User", "Book");
            return SendResponse(response);
        }
        #region SWAGGER DOC
        [Produces("application/json", "text/plain")]
        [ProducesResponseType(200, Type = typeof(ApiResponse<List<ReservationGetDto>>))]
        [ProducesResponseType(404, Type = typeof(ApiResponse<NoData>))]
        #endregion
        [HttpGet("byuserid")]
        public async Task<ActionResult> GetReservationsByUserId([FromQuery] int userId)
        {
            var response = await _reservationBs.GetReservationByUserIdAsync(userId, "User", "Book");
            return SendResponse(response);
        }
        #region SWAGGER DOC
        [Produces("application/json", "text/plain")]
        [ProducesResponseType(200, Type = typeof(ApiResponse<List<ReservationGetDto>>))]
        [ProducesResponseType(404, Type = typeof(ApiResponse<NoData>))]
        #endregion
        [HttpGet("bybookid")]
        public async Task<ActionResult> GetReservationsByBookId([FromQuery] int bookId)
        {
            var response = await _reservationBs.GetReservationByBookIdAsync(bookId, "User", "Book");
            return SendResponse(response);
        }
        #region SWAGGER DOC
        [Produces("application/json", "text/plain")]
        [ProducesResponseType(200, Type = typeof(ApiResponse<List<ReservationGetDto>>))]
        [ProducesResponseType(404, Type = typeof(ApiResponse<NoData>))]
        #endregion
        [HttpGet("byreservationdate")]
        public async Task<ActionResult> GetReservationsByReservationDate([FromQuery] DateTime reservationDate)
        {
            var response = await _reservationBs.GetReservationByReservationDateAsync(reservationDate, "User", "Book");
            return SendResponse(response);
        }
        #region SWAGGER DOC
        [Produces("application/json", "text/plain")]
        [ProducesResponseType(200, Type = typeof(ApiResponse<List<ReservationGetDto>>))]
        [ProducesResponseType(404, Type = typeof(ApiResponse<NoData>))]
        #endregion
        [HttpGet("byexpirationdate")]
        public async Task<ActionResult> GetReservationsByExpirationDate([FromQuery] DateTime expirationDate)
        {
            var response = await _reservationBs.GetReservationByExpirationDateAsync(expirationDate, "User", "Book");
            return SendResponse(response);
        }
        #region SWAGGER DOC
        [Produces("application/json", "text/plain")]
        [ProducesResponseType(200, Type = typeof(ApiResponse<List<ReservationGetDto>>))]
        [ProducesResponseType(404, Type = typeof(ApiResponse<NoData>))]
        #endregion
        [HttpGet("byactive")]
        public async Task<ActionResult> GetReservationsByActive([FromQuery] bool isActive)
        {
            var response = await _reservationBs.GetReservationByActiveAsync(isActive, "User", "Book");
            return SendResponse(response);
        }
        #region Swagger
        [Produces("application/json", "text/plain")]
        [ProducesResponseType(200, Type = typeof(ApiResponse<ReservationGetDto>))]
        [ProducesResponseType(404, Type = typeof(ApiResponse<NoData>))]
        #endregion
        [HttpGet("{id}")]
        public async Task<ActionResult> GetById([FromRoute] int id)
        {
            var response = await _reservationBs.GetByIdAsync(id, "User", "Book");
            return SendResponse(response);
        }
        #region Swagger
        [Produces("application/json", "text/plain")]
        [ProducesResponseType(200, Type = typeof(ApiResponse<Reservation>))]
        [ProducesResponseType(404, Type = typeof(ApiResponse<NoData>))]
        #endregion
        [HttpPost]
        public async Task<ActionResult> SaveNewReservation([FromBody] ReservationPostDto dto)
        {
            var response = await _reservationBs.InsertAsync(dto);
            return CreatedAtAction(nameof(GetById), new { id = response.Data.ReservationId }, response.Data);
        }
        #region Swagger
        [Produces("application/json", "text/plain")]
        [ProducesResponseType(200, Type = typeof(ApiResponse<NoData>))]
        [ProducesResponseType(404, Type = typeof(ApiResponse<NoData>))]
        #endregion
        [HttpPut]
        public async Task<IActionResult> UpdateReservation([FromBody] ReservationPutDto dto)
        {
            var response = await _reservationBs.UpdateAsync(dto);
            return SendResponse(response);
        }
        #region Swagger
        [Produces("application/json", "text/plain")]
        [ProducesResponseType(200, Type = typeof(ApiResponse<NoData>))]
        [ProducesResponseType(404, Type = typeof(ApiResponse<NoData>))]
        #endregion
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteReservation([FromRoute] int id)
        {
            var response = await _reservationBs.DeleteAsync(id);
            return SendResponse(response);
        }
    }
}
