using SIMA.Application.Query.Contract.Features.Logistics.LogisticsSupplies;
using SIMA.Framework.Common.Response;
using SIMA.Framework.Core.Repository;

namespace SIMA.Persistance.Read.Repositories.Features.Logistics.LogisticsSupplies;

public interface ILogisticsSupplyQueryRepository : IQueryRepository
{
    Task<GetLogisticsSupplyDeatilQueryResult> GetDetail(long id, long logisticsRequestId);
    Task<Result<IEnumerable<GetLogisticsSupplyQueryResult>>> GetAll(GetAllLogisticsSuppliesQuery request);
    Task<Result<IEnumerable<GetLogisticsSupplyQueryResult>>> GetAllMy(GetAllMyLogisticsSuppliesQuery request);
    Task<Result<IEnumerable<GetLogisticsSupplyGoodsCategoryQueryResult>>> GetGoodsCategoryBySupplyId(long LogisticsSupplyId);
    Task<IEnumerable<GetOrderingByLogisticsSupplyIdQueryResult>> GetOrderingByLogisticsSupplyId(long LogisticsSupplyId);
    Task<IEnumerable<GetOrderingByLogisticsSupplyIdQueryResult>> GetPaymentCommandByLogisticsSupplyId(long LogisticsSupplyId);
    Task<IEnumerable<GetOrderingByLogisticsSupplyIdQueryResult>> GetPrePaymentCommandByLogisticsSupplyId(long LogisticsSupplyId);
}