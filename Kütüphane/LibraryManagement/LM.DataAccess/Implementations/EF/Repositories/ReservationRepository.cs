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
    public class ReservationRepository : BaseRepository<Reservation, LibraryManagementDBContext>, IReservationRepository
    {
        public async Task<List<Reservation>> GetByActiveAsync(bool isActive, params string[] includeList)
        {
            return await GetAllAsync(r => r.Active == isActive && r.IsDeleted == false, includeList);
        }

        public async Task<List<Reservation>> GetByBookIdAsync(int bookId, params string[] includeList)
        {
            return await GetAllAsync(r => r.BookId == bookId && r.IsDeleted == false, includeList);
        }

        public async Task<List<Reservation>> GetByExpirationDateAsync(DateTime expirationDate, params string[] includeList)
        {
            return await GetAllAsync(r => r.ExpirationDate == expirationDate && r.IsDeleted == false, includeList);
        }

        public async Task<Reservation> GetByIdAsync(int reservationId, params string[] includeList)
        {
            return await GetAsync(r => r.ReservationId == reservationId && r.IsDeleted == false, includeList);
        }

        public async Task<List<Reservation>> GetByReservationDateAsync(DateTime reservationDate, params string[] includeList)
        {
            return await GetAllAsync(r => r.ReservationDate == reservationDate && r.IsDeleted == false, includeList);
        }

        public async Task<List<Reservation>> GetByUserIdAsync(int userId, params string[] includeList)
        {
            return await GetAllAsync(r => r.UserId == userId && r.IsDeleted == false, includeList);
        }
    }
}
