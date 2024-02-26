using SIMA.Domain.Models.Features.Auths.Staffs.Entities;
using SIMA.Framework.Core.Repository;

namespace SIMA.Domain.Models.Features.Auths.Staffs.Interfaces;

public interface IStaffRepository : IRepository<Staff>
{
    Task<Staff> GetById(long id);
}
