using SIMA.Framework.Core.Domain;

namespace SIMA.Domain.Models.Features.Auths.ResponsibleTypes.Interfaces;

public interface IResponsibleTypeDomainService : IDomainService
{
    Task<bool> IsCodeUnique(string code, long id);
}
