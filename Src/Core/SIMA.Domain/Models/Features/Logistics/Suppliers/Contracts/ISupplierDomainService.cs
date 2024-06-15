using SIMA.Domain.Models.Features.Logistics.Suppliers.ValueObjects;
using SIMA.Framework.Core.Domain;

namespace SIMA.Domain.Models.Features.Logistics.Suppliers.Contracts;

public interface ISupplierDomainService : IDomainService
{
    Task<bool> IsCodeUnique(string code, SupplierId? id = null);
}
