using Infrastructure.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LM.Model.Entities
{
    public class Book : IEntity
    {
        public int BookId { get; set; }
        public string? Title { get; set; }
        public int? AuthorId { get; set; }
        public int? CategoryId { get; set; }
        public DateTime? PublishDate { get; set; }
        public int? AvailableCopies { get; set; }
        public int? TotalCopies { get; set; }        
        public string? PicturePath { get; set; }
        public bool IsDeleted { get; set; }
        public Author? Author { get; set; }
        public Category? Category { get; set; }
    }
}
