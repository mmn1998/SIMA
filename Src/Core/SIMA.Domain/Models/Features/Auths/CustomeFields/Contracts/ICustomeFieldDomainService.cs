using SIMA.Domain.Models.Features.Auths.CustomeFields.ValueObjects;
using SIMA.Framework.Core.Domain;

namespace SIMA.Domain.Models.Features.Auths.CustomeFields.Contracts;

public interface ICustomeFieldDomainService : IDomainService
{
    Task<bool> IsCodeUnique(string code, CustomeFieldId? id = null);
}
