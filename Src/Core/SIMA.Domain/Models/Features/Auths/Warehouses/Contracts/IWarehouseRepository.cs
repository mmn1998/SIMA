using SIMA.Domain.Models.Features.Auths.Warehouses.Entities;
using SIMA.Domain.Models.Features.Auths.Warehouses.ValueObjects;
using SIMA.Framework.Core.Repository;

namespace SIMA.Domain.Models.Features.Auths.Warehouses.Contracts;

public interface IWarehouseRepository : IRepository<Warehouse>
{
    Task<Warehouse> GetById(WarehouseId Id);
}