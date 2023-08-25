using Infrastructure.Utilities.ApiResponses;
using LM.Model.Dtos.AdminPanelUser;
using LM.Model.Dtos.User;
using LM.Model.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LM.Business.Interfaces
{
    public interface IUserBs
    {
        Task<ApiResponse<List<UserGetDto>>> GetUsersAsync();
        Task<ApiResponse<List<UserGetDto>>> GetUsersByFullNameAsync(string fullName);        
        Task<ApiResponse<UserGetDto>> GetByIdAsync(int userId);
        Task<ApiResponse<User>> InsertAsync(UserPostDto dto);
        Task<ApiResponse<NoData>> UpdateAsync(UserPutDto dto);
        Task<ApiResponse<NoData>> DeleteAsync(int id);
        Task<ApiResponse<UserGetDto>> LogInAsync(string userName, string password, params string[] includeList);



    }
}
