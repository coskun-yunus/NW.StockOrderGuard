using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using NW.StockOrderGuard.Product.Application.IntegrationEvents;
using Microsoft.Extensions.Options;

namespace NW.StockOrderGuard.Product.Infrastructure.IntegrationEvents
{
    public class HttpEventPublisher : IEventPublisher
    {
        private readonly HttpClient _httpClient;
        private readonly ILogger<HttpEventPublisher> _logger;
        private readonly StockServiceOptions _options;

        public HttpEventPublisher(HttpClient httpClient, ILogger<HttpEventPublisher> logger, IOptions<StockServiceOptions> options)
        {
            _httpClient = httpClient;
            _logger = logger;
            _options = options.Value;
        }

        public async Task PublishProductSyncedAsync(ProductSyncedEvent @event)
        {
            var url = $"{_options.BaseUrl}{_options.SyncEndpoint}";
            var json = JsonSerializer.Serialize(@event);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            try
            {
                var response = await _httpClient.PostAsync(url, content);
                response.EnsureSuccessStatusCode();
                _logger.LogInformation("ProductSyncedEvent HTTP ile StockService'e gönderildi.");
            }
            catch (HttpRequestException ex)
            {
                _logger.LogError(ex, "ProductSyncedEvent HTTP ile gönderilemedi.");
            }
        }
    }
} 