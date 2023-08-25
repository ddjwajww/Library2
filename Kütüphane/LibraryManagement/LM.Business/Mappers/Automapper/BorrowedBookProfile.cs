using AutoMapper;
using LM.Model.Dtos.BorrowedBook;
using LM.Model.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LM.Business.Mappers.Automapper
{
    public class BorrowedBookProfile :Profile
    {
        public BorrowedBookProfile()
        {
            CreateMap<BorrowedBook, BorrowedBookGetDto>()
                .ForMember(dest => dest.UserId,
                                    opt => opt.MapFrom(src => src.UserId == null
                                    ? 0
                                    : src.UserId))
                .ForMember(dest => dest.BookId,
                                    opt => opt.MapFrom(src => src.BookId == null
                                    ? 0
                                    : src.BookId))
                .ForMember(dest => dest.BorrowDate,
                                    opt => opt.MapFrom(src => src.BorrowDate == null
                                    ? DateTime.MinValue
                                    : src.BorrowDate))
                .ForMember(dest => dest.ReturnDate,
                                    opt => opt.MapFrom(src => src.ReturnDate == null
                                    ? DateTime.MinValue
                                    : src.ReturnDate))
                .ForMember(dest => dest.Returned,
                                    opt => opt.MapFrom(src => src.Returned == null
                                    ? false
                                    : src.Returned))
                .ReverseMap();

            CreateMap<BorrowedBookPostDto, BorrowedBook>();
            CreateMap<BorrowedBookPutDto, BorrowedBook>();
        }
    }
}
