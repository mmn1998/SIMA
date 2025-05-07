using SIMA.Domain.Models.Features.TrustyDrafts.ResponsibilityWageTypes.ValueObjects;
using SIMA.Framework.Core.Domain;

namespace SIMA.Domain.Models.Features.TrustyDrafts.ResponsibilityWageTypes.Contrcts;

public interface IResponsibilityWageTypeDomainService : IDomainService
{
    Task<bool> IsCodeUnique(string code, ResponsibilityWageTypeId? id = null);
}