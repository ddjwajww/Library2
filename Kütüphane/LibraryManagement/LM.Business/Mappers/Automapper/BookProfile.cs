using AutoMapper;
using LM.Model.Dtos.Book;
using LM.Model.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LM.Business.Mappers.Automapper
{
    public class BookProfile :Profile
    {   
        public BookProfile()
        {
            CreateMap<Book, BookGetDto>()
                .ForMember(dest => dest.AvailableCopies,
                                    opt => opt.MapFrom(src => src.AvailableCopies == null
                                    ? 0
                                    : src.AvailableCopies))
                .ForMember(dest => dest.Title,
                                    opt => opt.MapFrom(src => src.Title == null
                                    ? ""
                                    : src.Title))                
                .ForMember(dest => dest.CategoryName,
                                    opt => opt.MapFrom(src => src.Category.CategoryName == null
                                    ? ""
                                    : src.Category.CategoryName))
                .ForMember(dest => dest.AuthorName,
                                    opt => opt.MapFrom(src => src.Author.AuthorName == null
                                    ? ""
                                    : src.Author.AuthorName))
                .ForMember(dest => dest.PublishDate,
                                    opt => opt.MapFrom(src => src.PublishDate == null
                                    ? DateTime.MinValue
                                    : src.PublishDate))
                .ReverseMap();         
                
            CreateMap<BookPostDto, Book>();
            CreateMap<BookPutDto, Book>();
        }
    }
}
