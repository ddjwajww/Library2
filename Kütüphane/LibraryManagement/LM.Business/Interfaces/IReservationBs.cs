using Infrastructure.Utilities.ApiResponses;
using LM.Model.Dtos.BorrowedBook;
using LM.Model.Dtos.Reservation;
using LM.Model.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LM.Business.Interfaces
{
    public interface IReservationBs
    {
        Task<ApiResponse<List<ReservationGetDto>>> GetReservationAsync(params string[] includeList);
        Task<ApiResponse<List<ReservationGetDto>>> GetReservationByUserIdAsync(int userId, params string[] includeList);
        Task<ApiResponse<List<ReservationGetDto>>> GetReservationByBookIdAsync(int bookId, params string[] includeList);
        Task<ApiResponse<List<ReservationGetDto>>> GetReservationByReservationDateAsync(DateTime reservationDate, params string[] includeList);
        Task<ApiResponse<List<ReservationGetDto>>> GetReservationByExpirationDateAsync(DateTime expirationDate, params string[] includeList);
        Task<ApiResponse<List<ReservationGetDto>>> GetReservationByActiveAsync(bool isActive, params string[] includeList);
        Task<ApiResponse<ReservationGetDto>> GetByIdAsync(int reservationId, params string[] includeList);
        Task<ApiResponse<Reservation>> InsertAsync(ReservationPostDto dto);
        Task<ApiResponse<NoData>> UpdateAsync(ReservationPutDto dto);
        Task<ApiResponse<NoData>> DeleteAsync(int id);
    }
}
