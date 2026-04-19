using Application.Exceptions;
using Domain.Exceptions;
using Infrastructure.Exceptions;
using System.Net;
using System.Text.Json;

namespace API.Middlewares
{
    public class ExceptionMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly IWebHostEnvironment _env;

        public ExceptionMiddleware(RequestDelegate next, IWebHostEnvironment env) {
            _next = next;
            _env = env;
        }

        public async Task InvokeAsync(HttpContext context) {
            try {
                await _next(context);
            } catch (Exception ex) {
                await HandleExceptionAsync(context, ex, _env);
            }
        }

        private static Task HandleExceptionAsync(HttpContext context, Exception ex, IWebHostEnvironment env) {
            HttpStatusCode statusCode = HttpStatusCode.InternalServerError;
            string message = ex.Message;

            switch (ex) {
                case NotFoundException:
                statusCode = HttpStatusCode.NotFound;
                break;

                case ValidationException:
                statusCode = HttpStatusCode.UnprocessableEntity;
                break;

                case DomainValidationException:
                statusCode = HttpStatusCode.BadRequest;
                break;

                case UnauthorizedException:
                statusCode = HttpStatusCode.Unauthorized;
                break;

                case InfrastructureException:
                statusCode = HttpStatusCode.InternalServerError;
                break;
            }

            var response = new {
                error = message,
                stackTrace = env.IsDevelopment() ? ex.StackTrace : null
            };

            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)statusCode;

            return context.Response.WriteAsync(JsonSerializer.Serialize(response));
        }
    }
}
