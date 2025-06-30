using System.Collections.Generic;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using NW.StockOrderGuard.Product.Application.Contracts;
using NW.StockOrderGuard.Product.Domain.Entities;
using NW.StockOrderGuard.Product.Domain.ValueObjects;
using AutoMapper;
using Microsoft.Extensions.Options;

namespace NW.StockOrderGuard.Product.Infrastructure.Adapters
{
    public class FakeStoreAdapter : IExternalProductCatalog
    {
        private readonly HttpClient _httpClient;
        private readonly IMapper _mapper;
        private readonly FakeStoreApiOptions _options;

        public FakeStoreAdapter(HttpClient httpClient, IMapper mapper, IOptions<FakeStoreApiOptions> options)
        {
            _httpClient = httpClient;
            _mapper = mapper;
            _options = options.Value;
        }

        public async Task<IEnumerable<Domain.Entities.Product>> FetchCurrentProductsAsync()
        {
            var url = $"{_options.BaseUrl}{_options.ProductsEndpoint}";
            var response = await _httpClient.GetAsync(url);
            response.EnsureSuccessStatusCode();
            var json = await response.Content.ReadAsStringAsync();
            var dtos = JsonSerializer.Deserialize<List<FakeStoreProductDto>>(json, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
            return dtos != null ? _mapper.Map<List<Domain.Entities.Product>>(dtos) : new List<Domain.Entities.Product>();
        }
    }
} 