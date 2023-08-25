using Infrastructure.Utilities.ApiResponses;
using LM.Business.Implementations;
using LM.Business.Interfaces;
using LM.Model.Dtos.Book;
using LM.Model.Dtos.BorrowedBook;
using LM.Model.Dtos.Category;
using LM.Model.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace LM.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BorrowedBooksController : BaseController
    {
        private readonly IBorrowedBookBs _borrowedBookBs;

        public BorrowedBooksController(IBorrowedBookBs borrowedBookBs)
        {
            _borrowedBookBs = borrowedBookBs;
        }

        #region SWAGGER DOC
        [Produces("application/json", "text/plain")]
        [ProducesResponseType(200, Type = typeof(ApiResponse<List<BorrowedBookGetDto>>))]
        [ProducesResponseType(404, Type = typeof(ApiResponse<NoData>))]
        #endregion
        [HttpGet]
        [Authorize]
        public async Task<ActionResult> GetAllBorrowedBooks()
        {
            var response = await _borrowedBookBs.GetBorrowedBooksAsync("User", "Book");
            return SendResponse(response);
        }
        #region SWAGGER DOC
        [Produces("application/json", "text/plain")]
        [ProducesResponseType(200, Type = typeof(ApiResponse<List<BorrowedBookGetDto>>))]
        [ProducesResponseType(404, Type = typeof(ApiResponse<NoData>))]
        #endregion
        [HttpGet("byuserid")]
        public async Task<ActionResult> GetBorrowedBooksByUserId([FromQuery] int userId)
        {
            var response = await _borrowedBookBs.GetBorrowedBooksByUserIdAsync(userId, "User", "Book");
            return SendResponse(response);
        }
        #region SWAGGER DOC
        [Produces("application/json", "text/plain")]
        [ProducesResponseType(200, Type = typeof(ApiResponse<List<BorrowedBookGetDto>>))]
        [ProducesResponseType(404, Type = typeof(ApiResponse<NoData>))]
        #endregion
        [HttpGet("bybookid")]
        public async Task<ActionResult> GetBorrowedBooksByBookId([FromQuery] int bookId)
        {
            var response = await _borrowedBookBs.GetBorrowedBooksByBookIdAsync(bookId, "User", "Book");
            return SendResponse(response);
        }
        #region SWAGGER DOC
        [Produces("application/json", "text/plain")]
        [ProducesResponseType(200, Type = typeof(ApiResponse<List<BorrowedBookGetDto>>))]
        [ProducesResponseType(404, Type = typeof(ApiResponse<NoData>))]
        #endregion
        [HttpGet("byborrowdate")]
        public async Task<ActionResult> GetBorrowedBooksByBorrowDate([FromQuery] DateTime borrowDate)
        {
            var response = await _borrowedBookBs.GetBorrowedBooksByBorrowDateAsync(borrowDate, "User", "Book");
            return SendResponse(response);
        }
        #region SWAGGER DOC
        [Produces("application/json", "text/plain")]
        [ProducesResponseType(200, Type = typeof(ApiResponse<List<BorrowedBookGetDto>>))]
        [ProducesResponseType(404, Type = typeof(ApiResponse<NoData>))]
        #endregion
        [HttpGet("byreturndate")]
        public async Task<ActionResult> GetBorrowedBooksByReturnDate([FromQuery] DateTime returnDate)
        {
            var response = await _borrowedBookBs.GetBorrowedBooksByReturnDateAsync(returnDate, "User", "Book");
            return SendResponse(response);
        }
        #region SWAGGER DOC
        [Produces("application/json", "text/plain")]
        [ProducesResponseType(200, Type = typeof(ApiResponse<List<BorrowedBookGetDto>>))]
        [ProducesResponseType(404, Type = typeof(ApiResponse<NoData>))]
        #endregion
        [HttpGet("byreturned")]
        public async Task<ActionResult> GetBorrowedBooksByReturned([FromQuery] bool isReturned)
        {
            var response = await _borrowedBookBs.GetBorrowedBooksByReturnedAsync(isReturned, "User", "Book");            
            return SendResponse(response);
        }
        #region Swagger
        [Produces("application/json", "text/plain")]
        [ProducesResponseType(200, Type = typeof(ApiResponse<BorrowedBookGetDto>))]
        [ProducesResponseType(404, Type = typeof(ApiResponse<NoData>))]
        #endregion
        [HttpGet("{id}")]
        public async Task<ActionResult> GetById([FromRoute] int id)
        {
            var response = await _borrowedBookBs.GetByIdAsync(id, "User", "Book");
            return SendResponse(response);
        }
        #region Swagger
        [Produces("application/json", "text/plain")]
        [ProducesResponseType(200, Type = typeof(ApiResponse<BorrowedBook>))]
        [ProducesResponseType(404, Type = typeof(ApiResponse<NoData>))]
        #endregion
        [HttpPost]
        public async Task<ActionResult> SaveNewBorrowedBook([FromBody] BorrowedBookPostDto dto)
        {
            var response = await _borrowedBookBs.InsertAsync(dto);
            return CreatedAtAction(nameof(GetById), new { id = response.Data.BorrowedBookId }, response.Data);
        }
        #region Swagger
        [Produces("application/json", "text/plain")]
        [ProducesResponseType(200, Type = typeof(ApiResponse<NoData>))]
        [ProducesResponseType(404, Type = typeof(ApiResponse<NoData>))]
        #endregion
        [HttpPut]
        public async Task<IActionResult> UpdateBorrowedBook([FromBody] BorrowedBookPutDto dto)
        {
            var response = await _borrowedBookBs.UpdateAsync(dto);
            return SendResponse(response);
        }
        #region Swagger
        [Produces("application/json", "text/plain")]
        [ProducesResponseType(200, Type = typeof(ApiResponse<NoData>))]
        [ProducesResponseType(404, Type = typeof(ApiResponse<NoData>))]
        #endregion
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteBorrowedBook([FromRoute] int id)
        {
            var response = await _borrowedBookBs.DeleteAsync(id);
            return SendResponse(response);
        }
    }
}
