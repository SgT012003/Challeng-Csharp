using System.Net;
using CarePlusApi.Exceptions;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;

namespace CarePlusApi
{
    public class ExceptionHandlingMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<ExceptionHandlingMiddleware> _logger;

        public ExceptionHandlingMiddleware(RequestDelegate next, ILogger<ExceptionHandlingMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task InvokeAsync(HttpContext httpContext)
        {
            try
            {
                await _next(httpContext);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Ocorreu uma exceção não tratada: {Message}", ex.Message);
                await HandleExceptionAsync(httpContext, ex);
            }
        }

        private static Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            context.Response.ContentType = "application/json";
            var statusCode = HttpStatusCode.InternalServerError;
            var message = "Ocorreu um erro interno no servidor.";

            if (exception is CustomException customException)
            {
                statusCode = (HttpStatusCode)customException.StatusCode;
                message = customException.Message;
            }
            else if (exception is ValidationException || exception is ArgumentException)
            {
                statusCode = HttpStatusCode.BadRequest;
                message = "Dados de entrada inválidos.";
            }
            // Adicionar outros tipos de exceção se necessário (ex: DbUpdateException)

            context.Response.StatusCode = (int)statusCode;

            return context.Response.WriteAsJsonAsync(new
            {
                message = message,
                details = exception.Message
            });
        }
    }

    public static class ExceptionHandlingMiddlewareExtensions
    {
        public static IApplicationBuilder UseExceptionHandlingMiddleware(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<ExceptionHandlingMiddleware>();
        }
    }
}
