using SIMA.Domain.Models.Features.Logistics.Suppliers.Entities;
using SIMA.Domain.Models.Features.Logistics.Suppliers.ValueObjects;
using SIMA.Framework.Core.Repository;

namespace SIMA.Domain.Models.Features.Logistics.Suppliers.Contracts;

public interface ISupplierRepository : IRepository<Supplier>
{
    Task<Supplier> GetById(SupplierId id);
}