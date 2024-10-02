using SIMA.Domain.Models.Features.ServiceCatalogs.Products.Contracts;

namespace SIMA.DomainService.Features.ServiceCatalog.Products
{
    public class ProductDomainService : IProductDomainService
    {
        public Task<bool> IsCodeUnique(string code, ProductId? id = null)
        {
            throw new NotImplementedException();
        }
    }
}
