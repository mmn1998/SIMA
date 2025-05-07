using SIMA.Domain.Models.Features.Auths.UIInputElements.ValueObjects;
using SIMA.Framework.Core.Domain;

namespace SIMA.Domain.Models.Features.Auths.UIInputElements.Contracts;

public interface IUIInputElementDomainService : IDomainService
{
    Task<bool> IsCodeUnique(string code, UIInputElementId? id = null);
}