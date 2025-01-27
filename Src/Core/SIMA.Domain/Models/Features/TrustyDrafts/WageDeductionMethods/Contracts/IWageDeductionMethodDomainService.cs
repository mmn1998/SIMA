using SIMA.Domain.Models.Features.TrustyDrafts.WageDeductionMethods.ValueObjects;
using SIMA.Framework.Core.Domain;

namespace SIMA.Domain.Models.Features.TrustyDrafts.WageDeductionMethods.Contracts;

public interface IWageDeductionMethodDomainService : IDomainService
{
    Task<bool> IsCodeUnique(string code, WageDeductionMethodId? id = null);
}