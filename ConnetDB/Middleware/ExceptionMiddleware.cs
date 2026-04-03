using System.Net;
using System.Text.Json;
using Microsoft.Extensions.Logging;
using Microsoft.EntityFrameworkCore;

namespace ConnetDB.Middleware
{
    public class ExceptionMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<ExceptionMiddleware> _logger;

        public ExceptionMiddleware(RequestDelegate next, ILogger<ExceptionMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task Invoke(HttpContext context)
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

        private async Task HandleExceptionAsync(HttpContext context, Exception ex)
        {
            int statusCode = (int)HttpStatusCode.InternalServerError;
            string message = "Có lỗi xảy ra, vui lòng thử lại"; // Production-safe message

            // 🔥 Nếu là lỗi DB (FK, duplicate, ...)
            if (ex is DbUpdateException dbEx)
            {
                statusCode = 400;
                message = "Lỗi database: kiểm tra dữ liệu nhập";
            }

            // Log chi tiết để dev debug
            _logger.LogError(ex, "Lỗi xử lý request {Method} {Path}",
                context.Request.Method, context.Request.Path);

            context.Response.ContentType = "application/json";
            context.Response.StatusCode = statusCode;

            var result = JsonSerializer.Serialize(new
            {
                status = statusCode,
                message = message,
                path = context.Request.Path,
                timestamp = DateTime.UtcNow
            });

            await context.Response.WriteAsync(result);
        }
    }
}
