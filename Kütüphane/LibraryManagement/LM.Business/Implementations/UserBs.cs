using AutoMapper;
using Infrastructure.Utilities.ApiResponses;
using LM.Business.CustomExceptions;
using LM.Business.Interfaces;
using LM.DataAccess.Interfaces;
using LM.Model.Dtos.AdminPanelUser;
using LM.Model.Dtos.Category;
using LM.Model.Dtos.User;
using LM.Model.Entities;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace LM.Business.Implementations
{
    public class UserBs : IUserBs
    {
        private readonly IUserRepository _repo;
        private readonly IMapper _mapper;
        public UserBs(IUserRepository repo, IMapper mapper)
        {
            _repo = repo;
            _mapper = mapper;
        }
        public async Task<ApiResponse<UserGetDto>> GetByIdAsync(int userId)
        {
            if (userId <= 0)
                throw new BadRequestException("Kullanıcı id pozitif bir değer olmalıdır.");

            var user = await _repo.GetByIdAsync(userId);

            if (user == null)
                throw new NotFoundException("Kaynak bulunamadı");

            var dto = _mapper.Map<UserGetDto>(user);

            return ApiResponse<UserGetDto>.Success(StatusCodes.Status200OK, dto);
        }

        public async Task<ApiResponse<List<UserGetDto>>> GetUsersAsync()
        {
            var users = await _repo.GetAllAsync(u => !u.IsDeleted);

            if (users.Count <= 0)
                throw new NotFoundException("Kaynak bulunamadı");

            var returnList = _mapper.Map<List<UserGetDto>>(users);

            return ApiResponse<List<UserGetDto>>.Success(StatusCodes.Status200OK, returnList);
        }

        public async Task<ApiResponse<List<UserGetDto>>> GetUsersByFullNameAsync(string fullName)
        {
            if (string.IsNullOrEmpty(fullName))
                throw new BadRequestException("İsim boş olamaz.");

            var users = await _repo.GetByFullNameAsync(fullName);
            var returnList = _mapper.Map<List<UserGetDto>>(users);

            return ApiResponse<List<UserGetDto>>.Success(StatusCodes.Status200OK, returnList);
        }

      

        public async Task<ApiResponse<User>> InsertAsync(UserPostDto dto)
        {
            if (dto.FullName.Length < 2)
                throw new BadRequestException("İsim en az 2 karakterden oluşmalıdır");
            

            var entity = _mapper.Map<User>(dto);
            var insertedUser = await _repo.InsertAsync(entity);

            return ApiResponse<User>.Success(StatusCodes.Status200OK, insertedUser);
        }

        public async Task<ApiResponse<NoData>> UpdateAsync(UserPutDto dto)
        {
            if (dto.UserId <= 0)
                throw new BadRequestException("Id değeri pozitif bir sayı olmalıdır.");

            if (dto.FullName.Length < 2)
                throw new BadRequestException("İsim en az 2 karakterden oluşmalıdır");
            
            var entity = _mapper.Map<User>(dto);
            await _repo.UpdateAsync(entity);

            return ApiResponse<NoData>.Success(StatusCodes.Status200OK);
        }

        public async Task<ApiResponse<NoData>> DeleteAsync(int id)
        {
            if (id <= 0)
                throw new BadRequestException("Id pozitif bir değer olmalıdır");

            var entity = await _repo.GetByIdAsync(id);
            entity.IsDeleted = true;
            await _repo.UpdateAsync(entity);

            return ApiResponse<NoData>.Success(StatusCodes.Status200OK);
        }

        public async Task<ApiResponse<UserGetDto>> LogInAsync(string userName, string password, params string[] includeList)
        {
            if (string.IsNullOrEmpty(userName))
                throw new BadRequestException("Kullanıcı adı boş bırakılamaz.");
            if (string.IsNullOrEmpty(password))
                throw new BadRequestException("Şifre boş bırakılamaz.");
            if (userName.Length <= 2)
                throw new BadRequestException("Kullanıcı adı en az 2 karakter olmalıdır.");

            var user = await _repo.GetByUserNameAndPasswordAsync(userName, password, includeList);

            if (user == null)
                throw new BadRequestException("Kullanıcı bulunamadı.");

            var list = _mapper.Map<UserGetDto>(user);

            return ApiResponse<UserGetDto>.Success(StatusCodes.Status200OK, list);
        }
    }
}
