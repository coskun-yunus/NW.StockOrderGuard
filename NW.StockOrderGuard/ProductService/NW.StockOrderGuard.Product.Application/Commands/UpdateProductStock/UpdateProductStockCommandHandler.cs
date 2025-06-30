using MediatR;
using NW.StockOrderGuard.Product.Application.Contracts;
using NW.StockOrderGuard.Product.Application.UseCases;
using NW.StockOrderGuard.Product.Domain.Entities;
using NW.StockOrderGuard.SharedKernel;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace NW.StockOrderGuard.Product.Application.Commands.UpdateProductStock
{
    public class UpdateProductStockCommandHandler : IRequestHandler<UpdateProductStockCommand, Result>
    {
        private readonly UpdateProductStockUseCase _useCase;
        public UpdateProductStockCommandHandler(UpdateProductStockUseCase useCase)
        {
            _useCase = useCase;
        }

        public async Task<Result> Handle(UpdateProductStockCommand command, CancellationToken cancellationToken)
        {
           return await _useCase.HandleAsync(command);
           
        }
    }
} 