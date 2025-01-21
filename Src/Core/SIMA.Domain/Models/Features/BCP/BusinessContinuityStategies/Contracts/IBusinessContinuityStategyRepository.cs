using SIMA.Domain.Models.Features.BCP.BusinessContinuityStategies.Entities;
using SIMA.Domain.Models.Features.BCP.BusinessContinuityStategies.ValueObjects;
using SIMA.Framework.Core.Repository;

namespace SIMA.Domain.Models.Features.BCP.BusinessContinuityStategies.Contracts;

public interface IBusinessContinuityStategyRepository : IRepository<BusinessContinuityStrategy>
{
    Task<BusinessContinuityStrategy> GetById(BusinessContinuityStrategyId id);
    Task<BusinessContinuityStrategy?> GetLast();
}
