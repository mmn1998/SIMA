using SIMA.Domain.Models.Features.BranchManagement.Brokers.Entities;
using SIMA.Domain.Models.Features.BranchManagement.Brokers.ValueObjects;
using SIMA.Framework.Core.Repository;

namespace SIMA.Domain.Models.Features.BranchManagement.Brokers.Interfaces;

public interface IBrokerRepository : IRepository<Broker>
{
    Task<Broker> GetById(BrokerId id);
}
