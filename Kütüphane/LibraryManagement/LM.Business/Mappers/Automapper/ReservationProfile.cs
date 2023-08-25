using AutoMapper;
using LM.Model.Dtos.Reservation;
using LM.Model.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LM.Business.Mappers.Automapper
{
    public class ReservationProfile :Profile
    {
        public ReservationProfile()
        {
            CreateMap<Reservation,ReservationGetDto>()
                .ForMember(dest => dest.ReservationDate,
                                    opt => opt.MapFrom(src => src.ReservationDate == null
                                    ? DateTime.MinValue
                                    : src.ReservationDate))
                .ForMember(dest => dest.ExpirationDate,
                                    opt => opt.MapFrom(src => src.ExpirationDate == null
                                    ? DateTime.MinValue
                                    : src.ExpirationDate))
                .ForMember(dest => dest.UserId,
                                    opt => opt.MapFrom(src => src.UserId == null
                                    ? 0
                                    : src.UserId))
                .ForMember(dest => dest.BookId,
                                    opt => opt.MapFrom(src => src.Book == null
                                    ? 0
                                    : src.BookId))
                .ForMember(dest => dest.Active,
                                    opt => opt.MapFrom(src => src.Active == null
                                    ? false
                                    : src.Active))
                .ReverseMap();

            CreateMap<ReservationPostDto, Reservation>();
            CreateMap<ReservationPutDto, Reservation>();
        }
    }
}
