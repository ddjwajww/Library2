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
    public class AdminPanelUserRepository : BaseRepository<AdminPanelUser , LibraryManagementDBContext>, IAdminPanelUserRepository
    {
        public async Task<AdminPanelUser> GetByUserNameAndPasswordAsync(string userName, string password, params string[] includeList)
        {
            return await GetAsync(x => x.UserName == userName && x.Password == password && x.IsDeleted == false, includeList);
        }
    }
}
