using Infrastructure.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LM.Model.Dtos.BorrowedBook
{
    public class BorrowedBookPostDto : IDto
    {
        public int BookId { get; set; }
        public int UserId { get; set; }
        public DateTime BorrowDate { get; set; }
        public DateTime ReturnDate { get; set; }
        public bool Returned { get; set; }
    }
}
