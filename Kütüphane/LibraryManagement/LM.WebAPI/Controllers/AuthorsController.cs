using Infrastructure.Utilities.ApiResponses;
using LM.Business.Interfaces;
using LM.Model.Dtos.Author;
using LM.Model.Dtos.Book;
using LM.Model.Dtos.BorrowedBook;
using LM.Model.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace LM.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthorsController : BaseController
    {
        private readonly IAuthorBs _authorBs;

        public AuthorsController(IAuthorBs authorBs)
        {
            _authorBs = authorBs;
        }

        #region SWAGGER DOC
        [Produces("application/json", "text/plain")]
        [ProducesResponseType(200, Type = typeof(ApiResponse<List<AuthorGetDto>>))]
        [ProducesResponseType(404, Type = typeof(ApiResponse<NoData>))]
        #endregion
        [HttpGet]
        [Authorize]
        public async Task<ActionResult> GetAllAuthors()
        {
            var response = await _authorBs.GetAuthorsAsync();
            return SendResponse(response);
        }
        #region SWAGGER DOC
        [Produces("application/json", "text/plain")]
        [ProducesResponseType(200, Type = typeof(ApiResponse<List<AuthorGetDto>>))]
        [ProducesResponseType(404, Type = typeof(ApiResponse<NoData>))]
        #endregion
        [HttpGet("byauthorname")]
        public async Task<ActionResult> GetAuthorsByAuthorName([FromQuery] string authorName)
        {
            var response = await _authorBs.GetAuthorsByAuthorNameAsync(authorName);
            return SendResponse(response);
        }
        #region SWAGGER DOC
        [Produces("application/json", "text/plain")]
        [ProducesResponseType(200, Type = typeof(ApiResponse<List<AuthorGetDto>>))]
        [ProducesResponseType(404, Type = typeof(ApiResponse<NoData>))]
        #endregion
        [HttpGet("bybirthdate")]
        public async Task<ActionResult> GetAuthorsByBirthDate([FromQuery] DateTime birthDate)
        {
            var response = await _authorBs.GetAuthorsByBirthDateAsync(birthDate);
            return SendResponse(response);
        }
        #region Swagger
        [Produces("application/json", "text/plain")]
        [ProducesResponseType(200, Type = typeof(ApiResponse<AuthorGetDto>))]
        [ProducesResponseType(404, Type = typeof(ApiResponse<NoData>))]
        #endregion
        [HttpGet("{id}")]
        public async Task<ActionResult> GetById([FromRoute] int id)
        {
            var response = await _authorBs.GetByIdAsync(id);
            return SendResponse(response);
        }
        #region Swagger
        [Produces("application/json", "text/plain")]
        [ProducesResponseType(200, Type = typeof(ApiResponse<Author>))]
        [ProducesResponseType(404, Type = typeof(ApiResponse<NoData>))]
        #endregion
        [HttpPost]
        public async Task<ActionResult> SaveNewAuthor([FromBody] AuthorPostDto dto)
        {
            var response = await _authorBs.InsertAsync(dto);
            return CreatedAtAction(nameof(GetById), new { id = response.Data.AuthorId }, response.Data);
        }
        #region Swagger
        [Produces("application/json", "text/plain")]
        [ProducesResponseType(200, Type = typeof(ApiResponse<NoData>))]
        [ProducesResponseType(404, Type = typeof(ApiResponse<NoData>))]
        #endregion
        [HttpPut]
        public async Task<IActionResult> UpdateAuthor([FromBody] AuthorPutDto dto)
        {
            var response = await _authorBs.UpdateAsync(dto);
            return SendResponse(response);
        }
        #region Swagger
        [Produces("application/json", "text/plain")]
        [ProducesResponseType(200, Type = typeof(ApiResponse<NoData>))]
        [ProducesResponseType(404, Type = typeof(ApiResponse<NoData>))]
        #endregion
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAuthor([FromRoute] int id)
        {
            var response = await _authorBs.DeleteAsync(id);
            return SendResponse(response);
        }

    }
}
