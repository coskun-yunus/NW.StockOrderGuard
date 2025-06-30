using MediatR;
using NW.StockOrderGuard.Product.Application.Dto;

namespace NW.StockOrderGuard.Product.Application.Queries
{
    public class GetProductByNameQuery : IRequest<ProductDto>
    {
        public string ProductName { get; set; }
        public GetProductByNameQuery(string productName)
        {
            ProductName = productName;
        }
    }
} 