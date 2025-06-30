using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using System;
using System.Net;
using System.Text.Json;
using System.Text;
using System.Threading.Tasks;
using NW.StockOrderGuard.SharedKernel.ExceptionHandling.Models;
using NW.StockOrderGuard.SharedKernel.ExceptionHandling.Exceptions;
using NW.StockOrderGuard.SharedKernel.ExceptionHandling.Enums;
using FluentValidation;
using System.Linq;

namespace NW.StockOrderGuard.SharedKernel.ExceptionHandling.Middleware
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

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Unhandled exception occurred.");
                ErrorResponse error;
                int statusCode;
                string? traceId = context.TraceIdentifier;

                if (ex is ValidationException validationEx)
                {
                    statusCode = (int)HttpStatusCode.BadRequest;
                    error = new ErrorResponse
                    {
                        StatusCode = statusCode,
                        Message = "Bir veya daha fazla doğrulama hatası oluştu.",
                        Errors = validationEx.Errors
                            .GroupBy(e => e.PropertyName)
                            .ToDictionary(g => g.Key, g => g.Select(e => e.ErrorMessage).ToArray()),
                        Path = context.Request.Path,
                        ExceptionType = ExceptionType.Domain.ToString(),
                        TraceId = traceId,
                        
                    };
                }
                else
                {
                    var (code, exceptionType) = ex switch
                    {
                        NotFoundException => ((int)HttpStatusCode.NotFound, ExceptionType.NotFound),
                        ConflictException => ((int)HttpStatusCode.Conflict, ExceptionType.Conflict),
                        DomainException => ((int)HttpStatusCode.BadRequest, ExceptionType.Domain),
                        _ => ((int)HttpStatusCode.InternalServerError, ExceptionType.Unknown)
                    };
                    statusCode = code;
                    error = new ErrorResponse
                    {
                        StatusCode = statusCode,
                        Message = ex.Message,
                        Detail = ex.InnerException?.Message,
                        Path = context.Request.Path,
                        ExceptionType = exceptionType.ToString(),
                        TraceId = traceId
                    };
                }

                context.Response.StatusCode = statusCode;
                context.Response.ContentType = "application/json";
                var options = new JsonSerializerOptions { PropertyNamingPolicy = JsonNamingPolicy.CamelCase };
                var json = JsonSerializer.Serialize(error, options);
                await context.Response.WriteAsync(json, Encoding.UTF8);
            }
        }
    }
} 