using SIMA.Application.Query.Contract.Features.Auths.Staffs;
using SIMA.Framework.Common.Response;
using SIMA.Framework.Core.Repository;

namespace SIMA.Persistance.Read.Repositories.Features.Auths.Staffs;

public interface IStaffQueryRepository : IQueryRepository
{
    Task<bool> IsStaffSatisfied(long profileId, long positionId);
    Task<GetStaffQueryResult> FindById(long id);
    Task<GetStaffByStaffNumberQueryResult> FindByStaffNumber(string staffNumber);
    Task<Result<IEnumerable<GetStaffQueryResult>>> GetAll(GetAllStaffQuery? request = null);
    Task<IEnumerable<GetStaffQueryResult>> GetAllByDepartmentId(long? departmentId);
    Task<long> GetStaffIdByUserId(long userId);
}
