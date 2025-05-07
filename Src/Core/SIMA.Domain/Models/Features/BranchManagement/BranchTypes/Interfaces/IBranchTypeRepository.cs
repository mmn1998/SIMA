using SIMA.Domain.Models.Features.BranchManagement.BranchTypes.Entities;
using SIMA.Framework.Core.Repository;

namespace SIMA.Domain.Models.Features.BranchManagement.BranchTypes.Interfaces
{
    public interface IBranchTypeRepository : IRepository<BranchType>
    {
        Task<BranchType> GetById(long id);
    }
}
