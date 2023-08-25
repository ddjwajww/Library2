using Infrastructure.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LM.Model.Entities
{
    public class Author :IEntity
    {
        public int AuthorId { get; set; }
        public string? AuthorName { get; set; }
        public DateTime BirthDate { get; set; }
        public string? PicturePath { get; set; }
        public bool IsDeleted { get; set; }
        public List<Book>? Books { get; set; }
    }
}
