using Microsoft.AspNetCore.Http;

namespace ECommerseApp.Errors
{
    public class ApiErrorResponse
    {
        public int StatusCode { get; set; }
        public string Message { get; set; }
        public string? Details { get; set; }

        public ApiErrorResponse(int statusCode, string message, string? details)
        {
            this.StatusCode = statusCode;
            this.Message = message;
            this.Details = details;
        }
    }
}
