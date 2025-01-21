using SIMA.Domain.Models.Features.TrustyDrafts.DraftOrigins.Entities;
using SIMA.Domain.Models.Features.TrustyDrafts.DraftOrigins.ValueObjects;
using SIMA.Framework.Core.Repository;

namespace SIMA.Domain.Models.Features.TrustyDrafts.DraftOrigins.Contracts;

public interface IWageDeductionMethodRepository : IRepository<WageDeductionMethod>
{
    Task<WageDeductionMethod> GetById(WageDeductionMethodId id);
}