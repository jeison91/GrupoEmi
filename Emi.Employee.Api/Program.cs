using Emi.Common.Exceptions;
using Emi.Employee.Api.DI;
using Emi.Employees.Application.DTO;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Reflection;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

DependencyInjection.AddRegistration(builder.Services, builder.Configuration);

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddProblemDetails();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "Api Emi v1");
    });
}

app.UseHttpsRedirection();
app.UseCors("politica");
app.UseMiddleware(typeof(ExceptionMiddleware));

app.UseAuthorization();
app.UseAuthentication();

app.MapControllers();

app.Run();
