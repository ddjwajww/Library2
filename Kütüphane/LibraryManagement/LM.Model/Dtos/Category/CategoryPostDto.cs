using Infrastructure.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LM.Model.Dtos.Category
{
    public class CategoryPostDto :IDto
    {
        public string? CategoryName { get; set; }
        public string? Description { get; set; }
        public string? PicturePath { get; set; }


    }
}
