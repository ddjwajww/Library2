using Infrastructure.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LM.Model.Dtos.Reservation
{
    public class ReservationPostDto : IDto
    {
        public int UserId { get; set; }
        public int BookId { get; set; }
        public DateTime ReservationDate { get; set; }
        public DateTime ExpirationDate { get; set; }
        public bool Active { get; set; }

    }
}
