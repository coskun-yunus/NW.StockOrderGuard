using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using NW.StockOrderGuard.Product.Application.Contracts;
using NW.StockOrderGuard.Product.Application.Queries;
using NW.StockOrderGuard.Product.Application.Dto;
using AutoMapper;

namespace NW.StockOrderGuard.Product.Application.Queries
{
    public class GetAllProductsQueryHandler : IRequestHandler<GetAllProductsQuery, IEnumerable<ProductDto>>
    {
        private readonly IProductRepository _productRepository;
        private readonly IMapper _mapper;

        public GetAllProductsQueryHandler(IProductRepository productRepository, IMapper mapper)
        {
            _productRepository = productRepository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<ProductDto>> Handle(GetAllProductsQuery query, CancellationToken cancellationToken)
        {
            var products = await _productRepository.GetAllAsync();
            return _mapper.Map<IEnumerable<ProductDto>>(products);
        }
    }
} 