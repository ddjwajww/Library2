using LM.Business.Extensions;
using LM.Business.Implementations;
using LM.Business.Interfaces;
using LM.Business.Mappers.Automapper;
using LM.DataAccess.Implementations.EF.Repositories;
using LM.DataAccess.Interfaces;
using LM.WebAPI.Extensions;
using LM.WebAPI.Middlewares;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddAPIServices(builder.Configuration);
builder.Services.AddBusinessServices();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.UseCustomException();

app.Run();
