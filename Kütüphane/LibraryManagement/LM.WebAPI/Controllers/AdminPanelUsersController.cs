using Infrastructure.Utilities.ApiResponses;
using Infrastructure.Utilities.Security.JWT;
using LM.Business.Interfaces;
using LM.Model.Dtos.AdminPanelUser;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace LM.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AdminPanelUsersController : BaseController
    {
        private readonly IAdminPanelUserBs _adminPanelUserBs;
        private readonly IConfiguration _config;

        public AdminPanelUsersController(IAdminPanelUserBs adminPanelUserBs, IConfiguration config)
        {
            _adminPanelUserBs = adminPanelUserBs;
            _config = config;
            
        }

        #region Swagger
        [Produces("application/json", "text/plain")]
        [ProducesResponseType(200, Type = typeof(ApiResponse<AdminPanelUserGetDto>))]
        [ProducesResponseType(404, Type = typeof(ApiResponse<NoData>))]
        #endregion
        [HttpGet("login")]
        public async Task<IActionResult> LogIn([FromQuery] string userName, string password)
        {
            var response = await _adminPanelUserBs.LogInAsync(userName, password);
            return SendResponse(response);
        }

        #region Swagger
        [Produces("application/json", "text/plain")]
        [ProducesResponseType(200, Type = typeof(ApiResponse<AccessToken>))]
        #endregion
        [HttpGet("gettoken")]
        public async Task<ActionResult> GetToken()
        {
            var accessToken = new JwtGenerator(_config).GenerateAccessToken();

            var response = new ApiResponse<AccessToken>();

            response.Data = accessToken;
            response.StatusCode = 200;
            return SendResponse(response);
        }
    }
}
