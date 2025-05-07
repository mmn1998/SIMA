using SIMA.Domain.Models.Features.Logistics.GoodsStatuses.Entities;
using SIMA.Domain.Models.Features.Logistics.GoodsStatuses.ValueObjects;
using SIMA.Framework.Core.Repository;

namespace SIMA.Domain.Models.Features.Logistics.GoodsStatuses.Contracts;

public interface IGoodsStatusRepository : IRepository<GoodsStatus>
{
    Task<GoodsStatus> GetById(GoodsStatusId id);
}