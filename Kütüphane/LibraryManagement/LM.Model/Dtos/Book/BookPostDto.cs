using Infrastructure.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LM.Model.Dtos.Book
{
    public class BookPostDto :IDto
    {
        public string Title { get; set; }
        public DateTime PublishDate { get; set; }
        public int AuthorId { get; set; }
        public int CategoryId { get; set; }
        public int TotalCopies { get; set; }
        public string? CategoryName { get; set; }
        public string? AuthorName { get; set; }
        public string? PicturePath { get; set; }
        public int AvailableCopies { get; set; }
    }
}
