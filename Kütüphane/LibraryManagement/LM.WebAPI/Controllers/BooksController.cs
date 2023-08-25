using Infrastructure.Utilities.ApiResponses;
using LM.Business.Interfaces;
using LM.Model.Dtos.Book;
using LM.Model.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace LM.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BooksController : BaseController
    {
        private readonly IBookBs _bookBs;

        public BooksController(IBookBs bookBs)
        {
           _bookBs = bookBs;
        }
        
        #region Swagger
        [Produces("application/json", "text/plain")]
        [ProducesResponseType(200, Type = typeof(ApiResponse<List<BookGetDto>>))]
        [ProducesResponseType(404, Type = typeof(ApiResponse<NoData>))]
        #endregion
        [HttpGet]
        [Authorize]
        public async Task<ActionResult> GetAllBooks()
        {
            var response = await _bookBs.GetBooksAsync("Author", "Category");
            return SendResponse(response);
        }
        #region Swagger
        [Produces("application/json", "text/plain")]
        [ProducesResponseType(200, Type = typeof(ApiResponse<List<BookGetDto>>))]
        [ProducesResponseType(404, Type = typeof(ApiResponse<NoData>))]
        #endregion
        [HttpGet("byauthorid")]
        public async Task<ActionResult> GetBooksByAuthorId([FromQuery] int author)
        {
            var response = await _bookBs.GetBooksByAuthorIdAsync(author, "Author", "Category");
            return SendResponse(response);
        }
        #region Swagger
        [Produces("application/json", "text/plain")]
        [ProducesResponseType(200, Type = typeof(ApiResponse<List<BookGetDto>>))]
        [ProducesResponseType(404, Type = typeof(ApiResponse<NoData>))]
        #endregion
        [HttpGet("bycategoryid")]
        public async Task<ActionResult> GetBooksByCategoryId([FromQuery] int category)
        {
            var response = await _bookBs.GetBooksByCategoryIdAsync(category, "Author", "Category");
            return SendResponse(response);
        }
        
        #region Swagger
        [Produces("application/json", "text/plain")]
        [ProducesResponseType(200, Type = typeof(ApiResponse<List<BookGetDto>>))]
        [ProducesResponseType(404, Type = typeof(ApiResponse<NoData>))]
        #endregion
        [HttpGet("byavailablecopies")]
        public async Task<ActionResult> GetBooksByAvaibleCopies([FromQuery] int min, [FromQuery] int max)
        {
            var response = await _bookBs.GetBooksByAvailableCopiesAsync(min, max, "Author", "Category");
            return SendResponse(response);
        }
        #region Swagger
        [Produces("application/json", "text/plain")]
        [ProducesResponseType(200, Type = typeof(ApiResponse<List<BookGetDto>>))]
        [ProducesResponseType(404, Type = typeof(ApiResponse<NoData>))]
        #endregion
        [HttpGet("bytotalcopies")]
        public async Task<ActionResult> GetBooksByTotalCopies([FromQuery] int min, [FromQuery] int max)
        {
            var response = await _bookBs.GetBooksByTotalCopiesAsync(min, max, "Author", "Category");
            return SendResponse(response);
        }
        #region Swagger
        [Produces("application/json", "text/plain")]
        [ProducesResponseType(200, Type = typeof(ApiResponse<BookGetDto>))]
        [ProducesResponseType(404, Type = typeof(ApiResponse<NoData>))]
        #endregion
        [HttpGet("{id}")]
        public async Task<ActionResult> GetById([FromRoute] int id)
        {
            var response = await _bookBs.GetByIdAsync(id, "Author", "Category");
            return SendResponse(response);
        }
        #region Swagger
        [Produces("application/json", "text/plain")]
        [ProducesResponseType(200, Type = typeof(ApiResponse<Book>))]
        [ProducesResponseType(404, Type = typeof(ApiResponse<NoData>))]
        #endregion
        [HttpPost]
        public async Task<ActionResult> SaveNewBook([FromBody] BookPostDto dto)
        {
            var response = await _bookBs.InsertAsync(dto);
            return CreatedAtAction(nameof(GetById), new { id = response.Data.BookId }, response.Data);
        }
        #region Swagger
        [Produces("application/json", "text/plain")]
        [ProducesResponseType(200, Type = typeof(ApiResponse<NoData>))]
        [ProducesResponseType(404, Type = typeof(ApiResponse<NoData>))]
        #endregion
        [HttpPut]
        public async Task<IActionResult> UpdateBook([FromBody] BookPutDto dto)
        {
            var response = await _bookBs.UpdateAsync(dto);
            return SendResponse(response);
        }
        #region Swagger
        [Produces("application/json", "text/plain")]
        [ProducesResponseType(200, Type = typeof(ApiResponse<NoData>))]
        [ProducesResponseType(404, Type = typeof(ApiResponse<NoData>))]
        #endregion
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteBook([FromRoute] int id)
        {
            var response = await _bookBs.DeleteAsync(id);
            return SendResponse(response);
        }
    }
}
