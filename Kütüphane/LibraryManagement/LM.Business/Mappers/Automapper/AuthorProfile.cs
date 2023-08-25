using AutoMapper;
using LM.Model.Dtos.Author;
using LM.Model.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LM.Business.Mappers.Automapper
{
    public class AuthorProfile :Profile
    {
        public AuthorProfile()
        {
            CreateMap<Author,AuthorGetDto>()
                .ForMember(dest => dest.AuthorName,
                                    opt => opt.MapFrom(src => src.AuthorName == null
                                    ? ""
                                    : src.AuthorName.ToUpper()))                
                .ForMember(dest => dest.BirthDate,
                                    opt => opt.MapFrom(src => src.BirthDate == null
                                    ? DateTime.MinValue
                                    : src.BirthDate)).ReverseMap();

            CreateMap<AuthorPostDto, Author>();
            CreateMap<AuthorPutDto, Author>();
        }
    }
}
