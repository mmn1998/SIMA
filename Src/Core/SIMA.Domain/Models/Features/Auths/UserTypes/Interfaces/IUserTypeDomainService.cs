using SIMA.Framework.Core.Domain;

namespace SIMA.Domain.Models.Features.Auths.UserTypes.Interfaces;

public interface IUserTypeDomainService : IDomainService
{
    Task<bool> IsCodeUnique(string code, UserTypeId? Id = null);
}