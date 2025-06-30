using MediatR;
using NW.StockOrderGuard.SharedKernel;

namespace NW.StockOrderGuard.Product.Application.Commands.SyncProducts
{
    public class SyncProductsCommand : IRequest<Result> { }
} 