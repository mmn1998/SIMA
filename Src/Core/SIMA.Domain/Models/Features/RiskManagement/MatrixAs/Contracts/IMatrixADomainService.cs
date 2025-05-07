using SIMA.Domain.Models.Features.RiskManagement.MatrixAs.ValueObjects;
using SIMA.Framework.Core.Domain;

namespace SIMA.Domain.Models.Features.RiskManagement.MatrixAs.Contracts;

public interface IMatrixADomainService : IDomainService
{
    Task<bool> IsCodeUnique(string code, MatrixAId? id = null);
}