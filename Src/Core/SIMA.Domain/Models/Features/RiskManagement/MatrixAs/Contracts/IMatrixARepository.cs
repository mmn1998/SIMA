using SIMA.Domain.Models.Features.RiskManagement.MatrixAs.Entities;
using SIMA.Domain.Models.Features.RiskManagement.MatrixAs.ValueObjects;
using SIMA.Framework.Core.Repository;

namespace SIMA.Domain.Models.Features.RiskManagement.MatrixAs.Contracts;

public interface IMatrixARepository : IRepository<MatrixA>
{
    Task<MatrixA> GetById(MatrixAId id);
}