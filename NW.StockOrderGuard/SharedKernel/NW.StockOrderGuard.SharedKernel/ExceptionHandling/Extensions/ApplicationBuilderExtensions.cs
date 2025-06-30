using Microsoft.AspNetCore.Builder;

namespace NW.StockOrderGuard.SharedKernel.ExceptionHandling.Extensions
{
    public static class ApplicationBuilderExtensions
    {
        public static IApplicationBuilder UseExceptionHandling(this IApplicationBuilder app)
        {
            return app.UseMiddleware<Middleware.ExceptionHandlingMiddleware>();
        }
    }
} 