using AutoMapper;
using LM.Model.Dtos.Category;
using LM.Model.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LM.Business.Mappers.Automapper
{
    public class CategoryProfile :Profile
    {
        public CategoryProfile()
        {
            CreateMap<Category, CategoryGetDto>()
                .ForMember(dest => dest.CategoryName,
                                    opt => opt.MapFrom(src => src.CategoryName == null
                                    ? ""
                                    : src.CategoryName)).ReverseMap();

            CreateMap<CategoryPostDto, Category>();
            CreateMap<CategoryPutDto, Category>();

        }
    }
}
