using SIMA.Framework.Core.Domain;

namespace SIMA.Domain.Models.Features.ServiceCatalogs.Products.Contracts;

public interface IProductDomainService : IDomainService
{
    Task<bool> IsCodeUnique(string code, ProductId? id = null);
    Task<string?> GetLastCode();
}