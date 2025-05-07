using SIMA.Domain.Models.Features.Logistics.LogisticsRequests.Entities;
using SIMA.Framework.Core.Repository;

namespace SIMA.Domain.Models.Features.Logistics.LogisticsRequests.Contracts;

public interface ILogisticsRequestRepository : IRepository<LogisticsRequest>
{
    Task<LogisticsRequest> GetById(long id);
    Task<LogisticsRequest> GetLastLogisticsRequest();
    Task<LogisticsRequest> GetByLogisticsRequestGoodsId(long requestGoodsId);
}
