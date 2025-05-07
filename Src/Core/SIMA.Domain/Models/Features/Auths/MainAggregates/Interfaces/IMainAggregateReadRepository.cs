using SIMA.Application.Query.Contract.Features.Auths.MainAggregates;
using SIMA.Framework.Common.Response;
using SIMA.Framework.Core.Repository;

namespace SIMA.Domain.Models.Features.Auths.MainAggregates.Interfaces;

public interface IMainAggregateReadRepository : IQueryRepository
{
    Task<Result<IEnumerable<GetMainAggregateQueryResult>>> GetAll(GetAllMainAggregateQuery request);
}
