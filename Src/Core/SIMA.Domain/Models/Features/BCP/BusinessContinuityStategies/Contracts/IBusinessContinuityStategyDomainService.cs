using SIMA.Domain.Models.Features.BCP.BusinessContinuityStategies.ValueObjects;
using SIMA.Framework.Core.Domain;

namespace SIMA.Domain.Models.Features.BCP.BusinessContinuityStategies.Contracts;

public interface IBusinessContinuityStategyDomainService : IDomainService
{
    //Task<bool> IsCodeUnique(string code, BusinessContinuityStrategyId? id = null);
    Task<bool> IsSoloutionCodeUnique(string code, BusinessContinuityStratgySolutionId? id = null);
    Task<bool> IsObjectiveCodeUnique(string code, BusinessContinuityStrategyObjectiveId? id = null);
}