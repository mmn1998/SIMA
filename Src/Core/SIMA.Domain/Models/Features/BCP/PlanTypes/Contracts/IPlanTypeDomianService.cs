using SIMA.Domain.Models.Features.BCP.PlanTypes.ValueObjects;
using SIMA.Framework.Core.Domain;

namespace SIMA.Domain.Models.Features.BCP.PlanTypes.Contracts;

public interface IPlanTypeDomianService : IDomainService
{
    Task<bool> IsCodeUnique(string code, PlanTypeId? id = null);
}
