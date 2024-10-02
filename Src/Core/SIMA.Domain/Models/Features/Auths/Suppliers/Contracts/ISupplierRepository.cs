using SIMA.Domain.Models.Features.Auths.Suppliers.Entities;
using SIMA.Domain.Models.Features.Auths.Suppliers.ValueObjects;
using SIMA.Framework.Core.Repository;

namespace SIMA.Domain.Models.Features.Auths.Suppliers.Contracts;

public interface ISupplierRepository : IRepository<Supplier>
{
    Task<Supplier> GetById(SupplierId id);
}