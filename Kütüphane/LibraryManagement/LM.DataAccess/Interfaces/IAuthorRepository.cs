using Infrastructure.DataAccess;
using LM.Model.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LM.DataAccess.Interfaces
{
    public interface IAuthorRepository :IBaseRepository<Author>
    {
        Task<List<Author>> GetByAuthorNameAsync(string authorName);        
        Task<List<Author>> GetByBirthDateAsync(DateTime birthDate);
        Task<Author> GetByIdAsync(int authorId);
    }
}
