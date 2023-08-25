using Infrastructure.Utilities.ApiResponses;
using Microsoft.AspNetCore.Mvc;

namespace LM.WebAPI.Controllers
{
    public class BaseController:ControllerBase
    {
        public ActionResult SendResponse<T>(ApiResponse<T> response)
        {
            if (response.StatusCode == StatusCodes.Status204NoContent)
                return new ObjectResult(null) { StatusCode = response.StatusCode };

            return new ObjectResult(response) { StatusCode = response.StatusCode };
        }
    }
}
