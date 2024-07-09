using SIMA.Application.Query.Contract.Features.Logistics.Cartables;
using SIMA.Application.Query.Contract.Features.Logistics.LogisticsRequests;
using SIMA.Framework.Common.Response;
using SIMA.Framework.Core.Repository;

namespace SIMA.Persistance.Read.Repositories.Features.Logistics.LogisticsRequest;

public interface ILogisticRequestQueryRepository : IQueryRepository
{
    Task<GetLogisticRequestsQueryResult> GetById(GetLogisticRequestsQuery request);
    Task<Result<IEnumerable<GetLogisticRequestsQueryResult>>> GetAll(GetAllLogisticsRequestsQuery request);
    Task<Result<IEnumerable<LogisticCartablesGetAllQueryResult>>> GetLogesticCartables(LogisticCartableGetAllQuery request);
    Task<Result<LogisticCartableGetQueryResult>> GetLogesticCartableDetail(long logesticId, long issueId);
}
