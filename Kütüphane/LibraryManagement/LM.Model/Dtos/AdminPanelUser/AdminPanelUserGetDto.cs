using Infrastructure.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LM.Model.Dtos.AdminPanelUser
{
    public class AdminPanelUserGetDto:IDto
    {
        public int Id { get; set; }
        public string FullName { get; set; }
        public string Email { get; set; }
    }
}
