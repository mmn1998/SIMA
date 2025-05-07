using SIMA.Framework.Core.Domain;

namespace SIMA.Domain.Models.Features.Auths.Departments.Interfaces;

public interface IDepartmentService : IDomainService
{
    Task<bool> IsCodeUnique(string code, long id);
}
