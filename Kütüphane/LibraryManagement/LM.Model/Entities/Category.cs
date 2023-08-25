using Infrastructure.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LM.Model.Entities
{
    public class Category :IEntity
    {
        public int CategoryId { get; set; }
        public string? CategoryName { get; set; }
        public string? Description { get; set; }
        public string? PicturePath { get; set; }
        public bool IsDeleted { get; set; }
        public List<Book>? Books { get; set; }
       

    }
}
