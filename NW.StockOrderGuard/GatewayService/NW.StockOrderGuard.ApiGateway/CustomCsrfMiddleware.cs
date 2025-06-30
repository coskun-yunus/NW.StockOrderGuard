using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;

namespace NW.StockOrderGuard.ApiGateway
{
    public class CustomCsrfMiddleware
    {
        private readonly RequestDelegate _next;
        private const string CsrfHeader = "X-XSRF-TOKEN";
        private const string CsrfSecret = "gizli-deger"; 

        public CustomCsrfMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            if (context.Request.Method == "POST" || context.Request.Method == "PUT" || context.Request.Method == "DELETE")
            {
                if (!context.Request.Headers.TryGetValue(CsrfHeader, out var xsrfToken) || xsrfToken != CsrfSecret)
                {
                    context.Response.StatusCode = 403;
                    await context.Response.WriteAsync("CSRF koruması: Token eksik veya geçersiz.");
                    return;
                }
            }
            await _next(context);
        }
    }
} 