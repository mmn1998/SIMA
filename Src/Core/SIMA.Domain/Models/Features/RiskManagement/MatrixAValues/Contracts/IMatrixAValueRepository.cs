using SIMA.Domain.Models.Features.RiskManagement.MatrixAValues.Entities;
using SIMA.Domain.Models.Features.RiskManagement.MatrixAValues.ValueObjects;
using SIMA.Framework.Core.Repository;

namespace SIMA.Domain.Models.Features.RiskManagement.MatrixAValues.Contracts;

public interface IMatrixAValueRepository : IRepository<MatrixAValue>
{
    Task<MatrixAValue> GetById(MatrixAValueId id);
}