using SIMA.Framework.Core.Domain;

namespace SIMA.Domain.Models.Features.Auths.Genders.Interfaces;

public interface IGenderService : IDomainService
{
    Task<bool> IsCodeUnique(string code, long id);
}
