using Infrastructure.Utilities.ApiResponses;
using LM.Model.Dtos.Author;
using LM.Model.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LM.Business.Interfaces
{
    public interface IAuthorBs
    {
        Task<ApiResponse<List<AuthorGetDto>>> GetAuthorsAsync();
        Task<ApiResponse<List<AuthorGetDto>>> GetAuthorsByAuthorNameAsync(string authorFirstName);        
        Task<ApiResponse<List<AuthorGetDto>>> GetAuthorsByBirthDateAsync(DateTime birthDate);
        Task<ApiResponse<AuthorGetDto>> GetByIdAsync(int authorId);
        Task<ApiResponse<Author>> InsertAsync(AuthorPostDto dto);
        Task<ApiResponse<NoData>> UpdateAsync(AuthorPutDto dto);
        Task<ApiResponse<NoData>> DeleteAsync(int id);

    }
}
