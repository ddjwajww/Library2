using Infrastructure.DataAccess;
using Infrastructure.DataAccess.EF;
using LM.Model.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LM.DataAccess.Interfaces
{
    public interface IReservationRepository : IBaseRepository<Reservation>
    {
        Task<List<Reservation>> GetByBookIdAsync(int bookId, params string[] includeList);
        Task<List<Reservation>> GetByUserIdAsync(int userId, params string[] includeList);
        Task<List<Reservation>> GetByReservationDateAsync(DateTime reservationDate, params string[] includeList);
        Task<List<Reservation>> GetByExpirationDateAsync(DateTime expirationDate, params string[] includeList);
        Task<List<Reservation>> GetByActiveAsync(bool isActive, params string[] includeList);
        Task<Reservation> GetByIdAsync(int reservationId, params string[] includeList);
    }
}
