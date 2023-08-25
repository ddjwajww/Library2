using Infrastructure.DataAccess;
using LM.Model.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LM.DataAccess.Interfaces
{
    public interface IUserRepository :IBaseRepository<User>
    {
        Task<List<User>> GetByFullNameAsync(string fullName);        
        Task<User> GetByIdAsync(int userId);
        Task<User> GetByUserNameAndPasswordAsync(string userName, string password, params string[] includeList);

    }
}
