using Infrastructure.DataAccess.EF;
using LM.DataAccess.Implementations.EF.Context;
using LM.DataAccess.Interfaces;
using LM.Model.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LM.DataAccess.Implementations.EF.Repositories
{
    public class UserRepository : BaseRepository<User, LibraryManagementDBContext>, IUserRepository
    {
        public async Task<List<User>> GetByFullNameAsync(string fullName)
        {
            return await GetAllAsync(u => u.FullName == fullName && u.IsDeleted == false);
        }

        public async Task<User> GetByIdAsync(int userId)
        {
            return await GetAsync(u => u.UserId == userId && u.IsDeleted == false);
        }

        public async Task<User> GetByUserNameAndPasswordAsync(string userName, string password, params string[] includeList)
        {
            return await GetAsync(x => x.UserName == userName && x.Password == password && x.IsDeleted == false, includeList);
        }
    }
}
