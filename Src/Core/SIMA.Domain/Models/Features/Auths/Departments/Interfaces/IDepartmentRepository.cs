using SIMA.Domain.Models.Features.Auths.Departments.Entities;
using SIMA.Framework.Core.Repository;

namespace SIMA.Domain.Models.Features.Auths.Departments.Interfaces;

public interface IDepartmentRepository : IRepository<Department>
{
    Task<Department> GetById(long id);
}
