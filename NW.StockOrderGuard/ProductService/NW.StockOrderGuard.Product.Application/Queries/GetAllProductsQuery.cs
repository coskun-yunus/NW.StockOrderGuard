using MediatR;
using NW.StockOrderGuard.Product.Application.Dto;
using NW.StockOrderGuard.Product.Domain.Entities;
using System.Collections.Generic;

namespace NW.StockOrderGuard.Product.Application.Queries
{
    public class GetAllProductsQuery : IRequest<IEnumerable<ProductDto>> { }
} 