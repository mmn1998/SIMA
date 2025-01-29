using SIMA.Domain.Models.Features.RiskManagement.RiskValues.ValueObjects;
using SIMA.Framework.Core.Domain;

namespace SIMA.Domain.Models.Features.RiskManagement.RiskValues.Contracts;

public interface IRiskValueDomainService : IDomainService
{
    Task<bool> IsCodeUnique(string code, RiskValueId? id = null);
    Task<bool> IsNumericUnique(float value, RiskValueId? id = null);
}