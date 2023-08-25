using AutoMapper;
using Infrastructure.Utilities.ApiResponses;
using LM.Business.CustomExceptions;
using LM.Business.Interfaces;
using LM.DataAccess.Interfaces;
using LM.Model.Dtos.AdminPanelUser;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LM.Business.Implementations
{
    public class AdminPanelUserBs : IAdminPanelUserBs
    {
        private readonly IAdminPanelUserRepository _repo;
        private readonly IMapper _mapper;
        public AdminPanelUserBs(IAdminPanelUserRepository repo, IMapper mapper)
        {
            _repo = repo;
            _mapper = mapper;
        }

        public async Task<ApiResponse<AdminPanelUserGetDto>> LogInAsync(string userName, string password, params string[] includeList)
        {
            if (string.IsNullOrEmpty(userName))
                throw new BadRequestException("Kullanıcı adı boş bırakılamaz.");
            if (string.IsNullOrEmpty(password))
                throw new BadRequestException("Şifre boş bırakılamaz.");
            if (userName.Length <= 2)
                throw new BadRequestException("Kullanıcı adı en az 2 karakter olmalıdır.");

            var adminUser = await _repo.GetByUserNameAndPasswordAsync(userName, password, includeList);

            if (adminUser == null)
                throw new BadRequestException("Kullanıcı bulunamadı.");

            var list = _mapper.Map<AdminPanelUserGetDto>(adminUser);

            return ApiResponse<AdminPanelUserGetDto>.Success(StatusCodes.Status200OK, list);
        }
    }
}
