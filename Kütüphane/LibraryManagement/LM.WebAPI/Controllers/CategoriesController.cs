using Infrastructure.Utilities.ApiResponses;
using LM.Business.Implementations;
using LM.Business.Interfaces;
using LM.Model.Dtos.Book;
using LM.Model.Dtos.Category;
using LM.Model.Dtos.Reservation;
using LM.Model.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace LM.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriesController : BaseController
    {
        private readonly ICategoryBs _categoryBs;

        public CategoriesController(ICategoryBs categoryBs)
        {
                _categoryBs = categoryBs;
        }
        #region SWAGGER DOC
        [Produces("application/json", "text/plain")]
        [ProducesResponseType(200, Type = typeof(ApiResponse<List<CategoryGetDto>>))]
        [ProducesResponseType(404, Type = typeof(ApiResponse<NoData>))]
        #endregion
        [HttpGet]
        [Authorize]
        public async Task<ActionResult> GetAllCategories()
        {
            var response = await _categoryBs.GetCategoriesAsync();
            return SendResponse(response);
        }
        #region SWAGGER DOC
        [Produces("application/json", "text/plain")]
        [ProducesResponseType(200, Type = typeof(ApiResponse<List<CategoryGetDto>>))]
        [ProducesResponseType(404, Type = typeof(ApiResponse<NoData>))]
        #endregion
        [HttpGet("bycategoryname")]
        public async Task<ActionResult> GetCategoriesByCategoryName([FromQuery] string categoryName)
        {
            var response = await _categoryBs.GetCategoriesByCategoryNameAsync(categoryName);
            return SendResponse(response);
        }
        #region Swagger
        [Produces("application/json", "text/plain")]
        [ProducesResponseType(200, Type = typeof(ApiResponse<CategoryGetDto>))]
        [ProducesResponseType(404, Type = typeof(ApiResponse<NoData>))]
        #endregion
        [HttpGet("{id}")]
        public async Task<ActionResult> GetById([FromRoute] int id)
        {
            var response = await _categoryBs.GetByIdAsync(id);
            return SendResponse(response);
        }
        #region Swagger
        [Produces("application/json", "text/plain")]
        [ProducesResponseType(200, Type = typeof(ApiResponse<Category>))]
        [ProducesResponseType(404, Type = typeof(ApiResponse<NoData>))]
        #endregion
        [HttpPost]
        public async Task<ActionResult> SaveNewCategory([FromBody] CategoryPostDto dto)
        {
            var response = await _categoryBs.InsertAsync(dto);
            return CreatedAtAction(nameof(GetById), new { id = response.Data.CategoryId }, response.Data);
        }
        #region Swagger
        [Produces("application/json", "text/plain")]
        [ProducesResponseType(200, Type = typeof(ApiResponse<NoData>))]
        [ProducesResponseType(404, Type = typeof(ApiResponse<NoData>))]
        #endregion
        [HttpPut]
        public async Task<IActionResult> UpdateCategory([FromBody] CategoryPutDto dto)
        {
            var response = await _categoryBs.UpdateAsync(dto);
            return SendResponse(response);
        }
        #region Swagger
        [Produces("application/json", "text/plain")]
        [ProducesResponseType(200, Type = typeof(ApiResponse<NoData>))]
        [ProducesResponseType(404, Type = typeof(ApiResponse<NoData>))]
        #endregion
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCategory([FromRoute] int id)
        {
            var response = await _categoryBs.DeleteAsync(id);
            return SendResponse(response);
        }
    }
}
