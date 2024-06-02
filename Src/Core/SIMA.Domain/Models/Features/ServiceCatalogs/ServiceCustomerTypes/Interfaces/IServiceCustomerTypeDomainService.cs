using SIMA.Framework.Core.Domain;

namespace SIMA.Domain.Models.Features.ServiceCatalogs.ServiceCustomerTypes.Interfaces;

public interface IServiceCustomerTypeDomainService : IDomainService
{
    Task<bool> IsCodeUnique(string code, ServiceCustomerTypeId? Id = null);
}