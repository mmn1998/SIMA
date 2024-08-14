using SIMA.Domain.Models.Features.Auths.ApiMethodActions.ValueObjects;
using SIMA.Framework.Core.Domain;

namespace SIMA.Domain.Models.Features.Auths.ApiMethodActions.Contracts;

public interface IApiMethodActionDomainService : IDomainService
{
    Task<bool> IsCodeUnique(string code, ApiMethodActionId? id = null);
}