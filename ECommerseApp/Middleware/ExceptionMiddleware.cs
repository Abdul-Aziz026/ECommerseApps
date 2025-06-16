using System.Net;
using System.Text.Json;
using ECommerseApp.Errors;

namespace ECommerseApp.Middleware
{
    public class ExceptionMiddleware
    {

        private readonly RequestDelegate next;
        private readonly IHostEnvironment env;

        public ExceptionMiddleware(IHostEnvironment _env, RequestDelegate _next)
        {
            next = _next;
            env = _env;
        }
        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await next(context);
            }
            catch (Exception ex)
            {
                await HandleExceptionAsync(context, ex, env);
            }
        }

        private Task HandleExceptionAsync(HttpContext context, Exception ex, IHostEnvironment env)
        {
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = StatusCodes.Status500InternalServerError;
            var response = env.IsDevelopment()
                ? new ApiErrorResponse(context.Response.StatusCode, ex.Message, ex.StackTrace)
                : new ApiErrorResponse(context.Response.StatusCode, ex.Message, "Internal Server Error");

            var options = new JsonSerializerOptions
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase // Use camel case to match the JSON
            };
            var jsonResponse = JsonSerializer.Serialize(response, options);
            
            return context.Response.WriteAsync(jsonResponse);
        }
    }
}
