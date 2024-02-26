using SIMA.Application.Query.Contract.Features.Auths.Staffs;
using SIMA.Framework.Common.Request;
using SIMA.Framework.Common.Response;
using SIMA.Framework.Core.Repository;

namespace SIMA.Persistance.Read.Repositories.Features.Auths.Staffs;

public interface IStaffQueryRepository : IQueryRepository
{
    //Task<Department> GetDepartment(long staffId);
    //Task<Profile> GetManager(long managerId);
    //Task<Profile> GetProfile(long profileId);
    //Task<Position> GetPosition(long positionId);
    Task<bool> IsStaffSatisfied(long profileId, long positionId);
    Task<GetStaffQueryResult> FindById(long id);
    Task<Result<List<GetStaffQueryResult>>> GetAll(BaseRequest? baseRequest = null);
}
