using Microsoft.EntityFrameworkCore;
using SIMA.Domain.Models.Features.Logistics.LogisticsSupplies.Entities;
using SIMA.Domain.Models.Features.Logistics.LogisticsSupplies.ValueObjects;
using SIMA.Framework.Core.Repository;

namespace SIMA.Domain.Models.Features.Logistics.LogisticsSupplies.Contracts;

public interface ILogisticsSupplyRepository : IRepository<LogisticsSupply>
{
    Task<LogisticsSupply> GetById(LogisticsSupplyId id);
    Task<LogisticsSupply?> GetLastLogisticsSupply();
}
