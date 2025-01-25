using SIMA.Domain.Models.Features.RiskManagement.MatrixAValues.ValueObjects;
using SIMA.Framework.Core.Domain;

namespace SIMA.Domain.Models.Features.RiskManagement.MatrixAValues.Contracts;

public interface IMatrixAValueDomainService : IDomainService
{
    Task<bool> IsCodeUnique(string code, MatrixAValueId? id = null);
}