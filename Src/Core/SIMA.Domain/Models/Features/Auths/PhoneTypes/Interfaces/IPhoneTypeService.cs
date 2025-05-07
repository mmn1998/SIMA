using SIMA.Framework.Core.Domain;

namespace SIMA.Domain.Models.Features.Auths.PhoneTypes.Interfaces;

public interface IPhoneTypeService : IDomainService
{
    Task<bool> IsCodeUnique(string code, long id);
}
