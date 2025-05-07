using SIMA.Application.Query.Contract.Features.BranchManagement.Branches;
using SIMA.Framework.Common.Response;
using SIMA.Framework.Core.Repository;

namespace SIMA.Domain.Models.Features.BranchManagement.Branches.Interfaces;

public interface IBranchQueryRepository : IQueryRepository
{
    Task<Result<IEnumerable<GetBranchQueryResult>>> GetAll(GetAllBranchQuery request);
    Task<Result<IEnumerable<GetBranchQueryResult>>> GetAllForTrusty(GetAllTrustyBranchesQuery request);
    Task<GetBranchQueryResult> GetById(long id);
}
