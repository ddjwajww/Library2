using Infrastructure.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LM.Model.Dtos.User
{
    public class UserGetDto :IDto
    {
        public int UserId { get; set; }
        public string? FullName { get; set; }
        public string? UserName { get; set; }
        public string? Email { get; set; }        
        public string? Password { get; set; }
    }
}
