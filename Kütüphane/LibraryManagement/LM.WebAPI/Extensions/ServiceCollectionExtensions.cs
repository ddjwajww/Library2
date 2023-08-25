using Infrastructure.Utilities.Security.JWT;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Text;
using System.Text.Json.Serialization;

namespace LM.WebAPI.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static void AddAPIServices(this IServiceCollection services, IConfiguration config)
        {
            services.AddControllers()
                .AddJsonOptions(opt => opt.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles);

            // Add services to the container.

            var tokenOptions = config.GetSection("TokenOptions").Get<TokenOptions>();

            services.AddAuthentication(opt =>
            {
                opt.DefaultAuthenticateScheme = "Bearer";
                opt.DefaultChallengeScheme = "Bearer";
            }).AddJwtBearer(opt =>
            {
                opt.TokenValidationParameters = new TokenValidationParameters()
                {
                    ValidateIssuer = true,
                    ValidIssuer = tokenOptions.Issuer,
                    ValidateAudience = true,
                    ValidAudience = tokenOptions.Audience,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(tokenOptions.SecurityKey)),
                    ClockSkew = TimeSpan.Zero
                };
            });




            //builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            services.AddEndpointsApiExplorer();
            services.AddSwaggerGen(c =>
            {
                c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme()
                {
                    Description = "JWT Auhorization",
                    Name = "Authorization",
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.Http,
                    Scheme = "Bearer",
                    BearerFormat = "JWT"
                });

                c.AddSecurityRequirement(new OpenApiSecurityRequirement()
                {
                    {
                        new OpenApiSecurityScheme
                        {
                                Reference = new OpenApiReference
                                {
                                    Type = ReferenceType.SecurityScheme,
                                    Id="Bearer"
                                }
                        },
                                 new List<string>()
                    }
                });
            });
        }
    }
}
