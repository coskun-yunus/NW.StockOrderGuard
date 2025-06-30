using System.Threading;
using System.Threading.Tasks;
using MediatR;
using NW.StockOrderGuard.Product.Application.UseCases;
using NW.StockOrderGuard.SharedKernel;

namespace NW.StockOrderGuard.Product.Application.Commands.SyncProducts
{
    public class SyncProductsCommandHandler : IRequestHandler<SyncProductsCommand, Result>
    {
        private readonly TrackProductCatalogUseCase _useCase;

        public SyncProductsCommandHandler(TrackProductCatalogUseCase useCase)
        {
            _useCase = useCase;
        }

        public async Task<Result> Handle(SyncProductsCommand command, CancellationToken cancellationToken)
        {
            await _useCase.SyncProductsAsync();
            return Result.Success();
        }
    }
} 