using Infrastructure.Utilities.ApiResponses;
using LM.Business.CustomExceptions;
using Microsoft.AspNetCore.Diagnostics;
using System.Text.Json;

namespace LM.WebAPI.Middlewares
{
    public static class UseCustomExceptionHandler
    {
        public static void UseCustomException(this IApplicationBuilder app)
        {
            app.UseExceptionHandler(config =>
            {
                // Middlewarede run metodu response döndürmek için kullanılır.
                config.Run(async context =>
                {   // Bu iki satırla yakalanan exception tipindeki nesneyi alıyoruz
                    var exceptionFeature = context.Features.Get<IExceptionHandlerFeature>();
                    var exception = exceptionFeature.Error;

                    var statusCode = StatusCodes.Status500InternalServerError;

                    switch (exception)
                    {
                        case BadRequestException:
                            statusCode = StatusCodes.Status400BadRequest;
                            break;
                        default:
                            break;
                    }

                    //burada response'u hazırlıyoruz
                    context.Response.ContentType = "application/json";
                    context.Response.StatusCode = statusCode;

                    //response'u oluşturduk
                    var response = ApiResponse<NoData>.Fail(statusCode, exception.Message);

                    //bu nesneyide responsun içine jsona dönüştürüp yazdık
                    await context.Response.WriteAsync(JsonSerializer.Serialize(response));

                });           

            });
        }
    }
}
