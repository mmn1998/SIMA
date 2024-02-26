using SIMA.Application.Query.Contract.Features.BranchManagement.BranchTypes;
using SIMA.Framework.Common.Request;
using SIMA.Framework.Core.Repository;

namespace SIMA.Domain.Models.Features.BranchManagement.BranchTypes.Interfaces;

public interface IBranchTypeReadRepository : IQueryRepository
{
    Task<GetBranchTypeQueryResult> GetById(long id);
    Task<List<GetBranchTypeQueryResult>> GetAll(BaseRequest request);
}
