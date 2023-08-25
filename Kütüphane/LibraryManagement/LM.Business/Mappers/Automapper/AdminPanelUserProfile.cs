using AutoMapper;
using LM.Model.Dtos.AdminPanelUser;
using LM.Model.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LM.Business.Mappers.Automapper
{
    public class AdminPanelUserProfile :Profile
    {
        public AdminPanelUserProfile()
        {
            CreateMap<AdminPanelUser, AdminPanelUserGetDto>();
        }
    }
}
