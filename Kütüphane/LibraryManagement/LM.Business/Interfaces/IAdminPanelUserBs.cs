using Infrastructure.Utilities.ApiResponses;
using LM.Model.Dtos.AdminPanelUser;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LM.Business.Interfaces
{
    public interface IAdminPanelUserBs
    {
        Task<ApiResponse<AdminPanelUserGetDto>> LogInAsync(string userName, string password, params string[] includeList);
    }
}
