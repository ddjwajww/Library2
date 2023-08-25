using LM.Business.Implementations;
using LM.Business.Interfaces;
using LM.Business.Mappers.Automapper;
using LM.DataAccess.Implementations.EF.Repositories;
using LM.DataAccess.Interfaces;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LM.Business.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static void AddBusinessServices(this IServiceCollection services)
        {           

            services.AddAutoMapper(typeof(BookProfile));
            //-------------------------------------------

            services.AddScoped<IBookBs, BookBs>();
            services.AddScoped<IBookRepository, BookRepository>();

            services.AddScoped<IAuthorBs, AuthorBs>();
            services.AddScoped<IAuthorRepository, AuthorRepository>();

            services.AddScoped<ICategoryBs, CategoryBs>();
            services.AddScoped<ICategoryRepository, CategoryRepository>();

            services.AddScoped<IBorrowedBookBs, BorrowedBookBs>();
            services.AddScoped<IBorrowedBookRepository, BorrowedBookRepository>();

            services.AddScoped<IReservationBs, ReservationBs>();
            services.AddScoped<IReservationRepository, ReservationRepository>();

            services.AddScoped<IUserBs, UserBs>();
            services.AddScoped<IUserRepository, UserRepository>();

            services.AddScoped<IAdminPanelUserBs, AdminPanelUserBs>();
            services.AddScoped<IAdminPanelUserRepository, AdminPanelUserRepository>();
        }
    }
}
