using System.Collections.Generic;

namespace NW.StockOrderGuard.SharedKernel.ExceptionHandling.Models
{
    public class ErrorResponse
    {
        public int StatusCode { get; set; }
        public string Message { get; set; }
        public string? Detail { get; set; }
        public string? Path { get; set; }
        public string? ExceptionType { get; set; }
        public Dictionary<string, string[]>? Errors { get; set; }
        public string? TraceId { get; set; }
    }
} 