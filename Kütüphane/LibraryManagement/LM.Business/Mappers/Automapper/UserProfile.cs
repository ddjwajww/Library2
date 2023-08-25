using AutoMapper;
using LM.Model.Dtos.User;
using LM.Model.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LM.Business.Mappers.Automapper
{
    public class UserProfile :Profile
    {
        public UserProfile()
        {
            CreateMap<User,UserGetDto>()
                .ForMember(dest => dest.FullName,
                                    opt => opt.MapFrom(src => src.FullName == null 
                                    ? ""
                                    : src.FullName))                
                .ReverseMap();

            CreateMap<UserPostDto, User>();
            CreateMap<UserPutDto, User>();
        }
    }
}
