using AutoMapper;
using Infrastructure.Utilities.ApiResponses;
using LM.Business.CustomExceptions;
using LM.Business.Interfaces;
using LM.DataAccess.Interfaces;
using LM.Model.Dtos.BorrowedBook;
using LM.Model.Dtos.Reservation;
using LM.Model.Entities;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace LM.Business.Implementations
{
    public class ReservationBs : IReservationBs
    {
        private readonly IReservationRepository _repo;
        private readonly IMapper _mapper;

        public ReservationBs(IReservationRepository repo, IMapper mapper)
        {
            _repo  = repo;
            _mapper = mapper;
        }

        public async Task<ApiResponse<ReservationGetDto>> GetByIdAsync(int reservationId, params string[] includeList)
        {
            if (reservationId <= 0)
                throw new BadRequestException("Id değeri sıfırdan büyük olmalıdır.");

            var reservation = await _repo.GetByIdAsync(reservationId, includeList);

            if (reservation == null)
                throw new NotFoundException("Kaynak Bulunamadı");

            var dto = _mapper.Map<ReservationGetDto>(reservation);

            return ApiResponse<ReservationGetDto>.Success(StatusCodes.Status200OK, dto);
        }

        public async Task<ApiResponse<List<ReservationGetDto>>> GetReservationAsync(params string[] includeList)
        {
            var reservations = await _repo.GetAllAsync(c => !c.IsDeleted, includeList: includeList);

            if (reservations.Count <= 0)
                throw new NotFoundException("Kaynak Bulunamadı.");

            var returnList = _mapper.Map<List<ReservationGetDto>>(reservations);

            return ApiResponse<List<ReservationGetDto>>.Success(StatusCodes.Status200OK, returnList);
        }

        public async Task<ApiResponse<List<ReservationGetDto>>> GetReservationByActiveAsync(bool isActive, params string[] includeList)
        {
            var reservations = await _repo.GetByActiveAsync(isActive, includeList);

            if (reservations.Count <= 0)
                throw new NotFoundException("Kaynak Bulunamadı");

            var returnList = _mapper.Map<List<ReservationGetDto>>(reservations);

            return ApiResponse<List<ReservationGetDto>>.Success(StatusCodes.Status200OK, returnList);
        }

        public async Task<ApiResponse<List<ReservationGetDto>>> GetReservationByBookIdAsync(int bookId, params string[] includeList)
        {
            if (bookId <= 0)
                throw new BadRequestException("Id pozitif bir değer olmalıdır");

            var reservations = await _repo.GetByBookIdAsync(bookId, includeList);

            if (reservations.Count <= 0)
                throw new NotFoundException("Kaynak Bulunamadı.");

            var returnList = _mapper.Map<List<ReservationGetDto>>(reservations);

            return ApiResponse<List<ReservationGetDto>>.Success(StatusCodes.Status200OK, returnList);
        }

        public async Task<ApiResponse<List<ReservationGetDto>>> GetReservationByExpirationDateAsync(DateTime expirationDate, params string[] includeList)
        {
            var reservations = await _repo.GetByExpirationDateAsync(expirationDate, includeList);

            if (reservations.Count <= 0)
                throw new NotFoundException("Kaynak Bulunamadı.");

            var returnList = _mapper.Map<List<ReservationGetDto>>(reservations);

            return ApiResponse<List<ReservationGetDto>>.Success(StatusCodes.Status200OK, returnList);
        }

        public async Task<ApiResponse<List<ReservationGetDto>>> GetReservationByReservationDateAsync(DateTime reservationDate, params string[] includeList)
        {
            var reservations = await _repo.GetByReservationDateAsync(reservationDate, includeList);

            if (reservations.Count <= 0)
                throw new NotFoundException("Kaynak Bulunamadı.");

            var returnList = _mapper.Map<List<ReservationGetDto>>(reservations);

            return ApiResponse<List<ReservationGetDto>>.Success(StatusCodes.Status200OK, returnList);
        }

        public async Task<ApiResponse<List<ReservationGetDto>>> GetReservationByUserIdAsync(int userId, params string[] includeList)
        {
            if (userId <= 0)
                throw new BadRequestException("Id pozitif bir değer olmalıdır");

            var reservations = await _repo.GetByUserIdAsync(userId, includeList);

            if (reservations.Count <= 0)
                throw new NotFoundException("Kaynak Bulunamadı.");

            var returnList = _mapper.Map<List<ReservationGetDto>>(reservations);

            return ApiResponse<List<ReservationGetDto>>.Success(StatusCodes.Status200OK, returnList);
        }

        public async Task<ApiResponse<Reservation>> InsertAsync(ReservationPostDto dto)
        {
            if (dto.BookId <= 0)
                throw new BadRequestException("Kitap id pozitif bir değer olmalıdır");

            if (dto.UserId <= 0)
                throw new BadRequestException("Kullanıcı id pozitif bir değer olmalıdır");

            var entity = _mapper.Map<Reservation>(dto);
            var insertedReservation = await _repo.InsertAsync(entity);

            return ApiResponse<Reservation>.Success(StatusCodes.Status200OK, insertedReservation);
        }

        public async Task<ApiResponse<NoData>> UpdateAsync(ReservationPutDto dto)
        {
            if (dto.ReservationId <= 0)
                throw new BadRequestException("Rezervasyon id pozitif bir değer olmalıdır.");

            if (dto.BookId <= 0)
                throw new BadRequestException("Kitap id pozitif bir değer olmalıdır");

            if (dto.UserId <= 0)
                throw new BadRequestException("Kullanıcı id pozitif bir değer olmalıdır");

            var entity = _mapper.Map<Reservation>(dto);
            await _repo.UpdateAsync(entity);

            return ApiResponse<NoData>.Success(StatusCodes.Status200OK);
        }

        public async Task<ApiResponse<NoData>> DeleteAsync(int id)
        {
            if (id <= 0)
                throw new BadRequestException("id pozitif bir değer olmalıdır");

            var entity = await _repo.GetByIdAsync(id);
            entity.IsDeleted = true;
            await _repo.UpdateAsync(entity);

            return ApiResponse<NoData>.Success(StatusCodes.Status200OK);
        }
    }
}
