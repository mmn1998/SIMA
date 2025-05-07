using SIMA.Domain.Models.Features.RiskManagement.SeverityValues.ValueObjects;
using SIMA.Framework.Core.Domain;

namespace SIMA.Domain.Models.Features.RiskManagement.SeverityValues.Contracts;

public interface ISeverityValueDomainService : IDomainService
{
    Task<bool> IsCodeUnique(string code, SeverityValueId? id = null);
    Task<bool> IsNumericUnique(float value, SeverityValueId? id = null);
}