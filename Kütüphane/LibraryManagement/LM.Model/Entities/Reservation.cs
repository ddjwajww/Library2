using Infrastructure.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LM.Model.Entities
{
    public class Reservation : IEntity
    {
        public int ReservationId { get; set; }
        public int? UserId { get; set; }
        public int? BookId { get; set; }
        public DateTime? ReservationDate { get; set; }
        public DateTime? ExpirationDate { get; set; }
        public bool Active { get; set; }
        public bool IsDeleted { get; set; }
        public User? User { get; set; }
        public Book? Book { get; set; }
    }
}
