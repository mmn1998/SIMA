using SIMA.Domain.Models.Features.Auths.CustomeFieldTypes.ValueObjects;
using SIMA.Framework.Core.Domain;

namespace SIMA.Domain.Models.Features.Auths.CustomeFieldTypes.Contracts;

public interface ICustomeFieldTypeDomainService : IDomainService
{
    Task<bool> IsCodeUnique(string code, CustomeFieldTypeId? id = null);
}
