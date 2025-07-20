using Emi.Common.Exceptions;
using Emi.Common.ResponseModel;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Emi.Common
{
    public class ExceptionMiddleware(RequestDelegate _next, IHostEnvironment _env)
    {
        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                await HandleExceptionAsync(context, ex);
            }
        }

        private async Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            int statusCode = exception switch
            {
                BadRequestException => StatusCodes.Status400BadRequest,
                UnauthorizedAccessException => StatusCodes.Status401Unauthorized,
                KeyNotFoundException => StatusCodes.Status404NotFound,
                _ => StatusCodes.Status500InternalServerError
            };

            var response = JsonSerializer.Deserialize<MessageResponse>(exception.Message);
            //{
            //    Status = statusCode,
            //    Message = "Something went wrong",
            //    Data = JsonSerializer.Deserialize<MessageResponse>(exception.Message)
            //};

            context.Response.ContentType = "application/json";
            context.Response.StatusCode = statusCode;

            var json = JsonSerializer.Serialize(response);
            await context.Response.WriteAsync(json);
        }

        private static string GetCustomMessage(int statusCode) => statusCode switch
        {
            StatusCodes.Status400BadRequest => "Error en los parametros.",
            StatusCodes.Status401Unauthorized => "No autorizado.",
            StatusCodes.Status404NotFound => "Recurso no encontrado.",
            StatusCodes.Status500InternalServerError => "Ha ocurrido un error interno.",
            _ => "Error inesperado."
        };
    }
}
