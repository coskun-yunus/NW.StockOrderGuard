using NW.StockOrderGuard.Product.Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace NW.StockOrderGuard.Product.Application.Contracts
{
    public interface IExternalProductCatalog
    {
        Task<IEnumerable<Domain.Entities.Product>> FetchCurrentProductsAsync();
    }
} 