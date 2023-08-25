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
    public class AuthorRepository : BaseRepository<Author, LibraryManagementDBContext>, IAuthorRepository
    {
        public async Task<List<Author>> GetByBirthDateAsync(DateTime birthDate)
        {
            return await GetAllAsync(a => a.BirthDate == birthDate && a.IsDeleted == false);
        }

        public async Task<List<Author>> GetByAuthorNameAsync(string authorName)
        {
            return await GetAllAsync(a => a.AuthorName == authorName && a.IsDeleted == false);
        }

        public async Task<Author> GetByIdAsync(int authorId)
        {
            return await GetAsync(a => a.AuthorId == authorId && a.IsDeleted == false);
        }

        
    }
}
