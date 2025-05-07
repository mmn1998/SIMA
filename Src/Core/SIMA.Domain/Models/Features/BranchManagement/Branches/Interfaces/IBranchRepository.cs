using SIMA.Domain.Models.Features.BranchManagement.Branches.Entities;
using SIMA.Framework.Core.Repository;

namespace SIMA.Domain.Models.Features.BranchManagement.Branches.Interfaces;

public interface IBranchRepository : IRepository<Entities.Branch>
{
    Task<Entities.Branch> GetById(long id);
    Task<Branch> GetByCode(string code);
}
