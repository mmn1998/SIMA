using SIMA.Framework.Core.Domain;

namespace SIMA.Domain.Models.Features.Auths.ServiceCustomerTypes.Interfaces;

public interface ICustomerTypeDomainService : IDomainService
{
    Task<bool> IsCodeUnique(string code, CustomerTypeId? Id = null);
}