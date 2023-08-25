using Infrastructure.DataAccess;
using LM.Model.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LM.DataAccess.Interfaces
{
    public interface IAdminPanelUserRepository :IBaseRepository<AdminPanelUser>
    {
        Task<AdminPanelUser> GetByUserNameAndPasswordAsync(string userName, string password, params string[] includeList);

    }
}
