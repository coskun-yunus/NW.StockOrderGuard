using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using NW.StockOrderGuard.Product.Application.Contracts;
using NW.StockOrderGuard.Product.Application.Dto;
using AutoMapper;

namespace NW.StockOrderGuard.Product.Application.Queries
{
    public class GetProductByNameQueryHandler : IRequestHandler<GetProductByNameQuery, ProductDto>
    {
        private readonly IProductRepository _productRepository;
        private readonly IMapper _mapper;

        public GetProductByNameQueryHandler(IProductRepository productRepository, IMapper mapper)
        {
            _productRepository = productRepository;
            _mapper = mapper;
        }

        public async Task<ProductDto> Handle(GetProductByNameQuery query, CancellationToken cancellationToken)
        {
            var allProducts = await _productRepository.GetAllAsync();
            var p = allProducts.FirstOrDefault(p => p.Title == query.ProductName);
            if (p == null) return null;
            return _mapper.Map<ProductDto>(p);
        }
    }
} 