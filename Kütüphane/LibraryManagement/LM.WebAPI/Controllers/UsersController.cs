using Infrastructure.Utilities.ApiResponses;
using LM.Business.Implementations;
using LM.Business.Interfaces;
using LM.Model.Dtos.AdminPanelUser;
using LM.Model.Dtos.Book;
using LM.Model.Dtos.Category;
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
    public class UsersController : BaseController
    {
        private readonly IUserBs _userBs;

        public UsersController(IUserBs userBs)
        {
            _userBs = userBs;
        }

        #region SWAGGER DOC
        [Produces("application/json", "text/plain")]
        [ProducesResponseType(200, Type = typeof(ApiResponse<List<UserGetDto>>))]
        [ProducesResponseType(404, Type = typeof(ApiResponse<NoData>))]
        #endregion
        [HttpGet]
        [Authorize]
        public async Task<ActionResult> GetAllUsers()
        {
            var response = await _userBs.GetUsersAsync();
            return SendResponse(response);
        }
        #region SWAGGER DOC
        [Produces("application/json", "text/plain")]
        [ProducesResponseType(200, Type = typeof(ApiResponse<List<UserGetDto>>))]
        [ProducesResponseType(404, Type = typeof(ApiResponse<NoData>))]
        #endregion
        [HttpGet("byfirstname")]
        public async Task<ActionResult> GetUsersByFullName([FromQuery] string fullName)
        {
            var response = await _userBs.GetUsersByFullNameAsync(fullName);
            return SendResponse(response);
        }
       
        #region Swagger
        [Produces("application/json", "text/plain")]
        [ProducesResponseType(200, Type = typeof(ApiResponse<UserGetDto>))]
        [ProducesResponseType(404, Type = typeof(ApiResponse<NoData>))]
        #endregion
        [HttpGet("{id}")]
        public async Task<ActionResult> GetById([FromRoute] int id)
        {
            var response = await _userBs.GetByIdAsync(id);
            return SendResponse(response);
        }
        #region Swagger
        [Produces("application/json", "text/plain")]
        [ProducesResponseType(200, Type = typeof(ApiResponse<User>))]
        [ProducesResponseType(404, Type = typeof(ApiResponse<NoData>))]
        #endregion
        [HttpPost]
        public async Task<ActionResult> SaveNewUser([FromBody] UserPostDto dto)
        {
            var response = await _userBs.InsertAsync(dto);
            return CreatedAtAction(nameof(GetById), new { id = response.Data.UserId }, response.Data);
        }
        #region Swagger
        [Produces("application/json", "text/plain")]
        [ProducesResponseType(200, Type = typeof(ApiResponse<NoData>))]
        [ProducesResponseType(404, Type = typeof(ApiResponse<NoData>))]
        #endregion
        [HttpPut]
        public async Task<IActionResult> UpdateUser([FromBody] UserPutDto dto)
        {
            var response = await _userBs.UpdateAsync(dto);
            return SendResponse(response);
        }

        #region Swagger
        [Produces("application/json", "text/plain")]
        [ProducesResponseType(200, Type = typeof(ApiResponse<NoData>))]
        [ProducesResponseType(404, Type = typeof(ApiResponse<NoData>))]
        #endregion
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUser([FromRoute] int id)
        {
            var response = await _userBs.DeleteAsync(id);
            return SendResponse(response);
        }

        #region Swagger
        [Produces("application/json", "text/plain")]
        [ProducesResponseType(200, Type = typeof(ApiResponse<UserGetDto>))]
        [ProducesResponseType(404, Type = typeof(ApiResponse<NoData>))]
        #endregion
        [HttpGet("login")]
        public async Task<IActionResult> LogIn([FromQuery] string userName, string password)
        {
            var response = await _userBs.LogInAsync(userName, password);
            return SendResponse(response);
        }
    }
}
