using SIMA.Domain.Models.Features.ServiceCatalogs.Products.Entities;
using SIMA.Framework.Core.Repository;

namespace SIMA.Domain.Models.Features.ServiceCatalogs.Products.Contracts;

public interface IProductRepository : IRepository<Product>
{
    Task<Product> GetById(ProductId id);
}