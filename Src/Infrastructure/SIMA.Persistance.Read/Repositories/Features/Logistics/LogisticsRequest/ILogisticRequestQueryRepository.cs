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
    Task<Result<IEnumerable<GetLogesticRequestGoodsQueryResult>>> GetLogesticRequestGoods(long Id);
    /// <summary>
    /// دریافت اطلاعات یک درخواست دارکات و خرید برای ثبت کننده آن
    /// </summary>
    /// <param name="logesticId"></param>
    /// <param name="issueId"></param>
    /// <returns></returns>
    Task<GetMyLogisticCartableGetQueryResult> GetMyLogesticCartableDetail(long logesticId, long issueId);

    Task<bool> IsTechnological(List<long> goodsId);
    Task<bool> IsGoods(List<long> goodsId);
    Task<bool> IsHardware(List<long> goodsId);
}
