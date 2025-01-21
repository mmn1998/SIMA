using SIMA.Application.Query.Contract.Features.Logistics.Cartables;
using SIMA.Application.Query.Contract.Features.Logistics.LogisticsRequests;
using SIMA.Framework.Common.Response;
using SIMA.Framework.Core.Repository;

namespace SIMA.Persistance.Read.Repositories.Features.Logistics.LogisticsRequest;

public interface ILogisticRequestQueryRepository : IQueryRepository
{
    Task<GetLogisticRequestsQueryResult> GetById(GetLogisticRequestsQuery request);
    Task<Result<IEnumerable<LogisticCartablesGetAllQueryResult>>> GetAll(GetAllLogisticsRequestsQuery request);
    Task<Result<IEnumerable<LogisticCartablesGetAllQueryResult>>> GetLogesticCartables(LogisticCartableGetAllQuery request);
    Task<LogisticCartableGetQueryResult> GetLogesticCartableDetail(long logesticId, long issueId);
    Task<Result<IEnumerable<GetLogesticRequestGoodsQueryResult>>> GetLogesticRequestGoods(GetLogesticRequestGoodsQuery request);
    Task<bool> IsTechnological(List<long> goodsId);
    Task<bool> IsGoods(List<long> goodsId);
    Task<bool> IsHardware(List<long> goodsId);
    Task<Result<List<GetLogisticsRequestCodeQueryResult>>> GetLogisticsRequestCode();
    Task<Result<IEnumerable<GetLogisticsRequestGoodsQueryResult>>> GetLogisticsRequestGoodsFiltered(long logisticsRequestId);
}
