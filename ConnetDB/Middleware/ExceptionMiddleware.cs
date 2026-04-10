using System.Net;
using System.Text.Json;
using Microsoft.Extensions.Logging;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Hosting; // Add this for IWebHostEnvironment

namespace ConnetDB.Middleware
{
    public class ExceptionMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly IWebHostEnvironment _env; // Add field for environment

        public ExceptionMiddleware(RequestDelegate next, IWebHostEnvironment env) // Inject environment
        {
            _next = next;
            _env = env;
        }

        public async Task InvokeAsync(HttpContext httpContext)
        {
            try
            {
                await _next(httpContext);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"❌ Unhandled Exception: {ex.Message}");
                Console.WriteLine(ex.ToString());   // In đầy đủ stack trace

                httpContext.Response.ContentType = "application/json";
                httpContext.Response.StatusCode = StatusCodes.Status500InternalServerError;

                var response = new
                {
                    error = "Internal Server Error",
                    message = _env.IsDevelopment() ? ex.Message : "An error occurred",
                    detail = _env.IsDevelopment() ? ex.ToString() : null
                };

                await httpContext.Response.WriteAsJsonAsync(response);
            }
        }
    }
}