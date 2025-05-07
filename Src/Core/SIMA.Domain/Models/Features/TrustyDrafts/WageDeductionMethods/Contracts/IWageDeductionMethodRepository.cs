using SIMA.Domain.Models.Features.TrustyDrafts.WageDeductionMethods.Entities;
using SIMA.Domain.Models.Features.TrustyDrafts.WageDeductionMethods.ValueObjects;
using SIMA.Framework.Core.Repository;

namespace SIMA.Domain.Models.Features.TrustyDrafts.WageDeductionMethods.Contracts;

public interface IWageDeductionMethodRepository : IRepository<WageDeductionMethod>
{
    Task<WageDeductionMethod> GetById(WageDeductionMethodId id);
}