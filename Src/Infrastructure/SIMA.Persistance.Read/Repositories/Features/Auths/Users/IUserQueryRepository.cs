using SIMA.Application.Query.Contract.Features.Auths.Users;
using SIMA.Framework.Common.Response;
using SIMA.Framework.Core.Repository;

namespace SIMA.Persistance.Read.Repositories.Features.Auths.Users;

public interface IUserQueryRepository : IQueryRepository
{
    Task<GetUserQueryResult> FindByIdQuery(long id);
    Task<Result<IEnumerable<GetUserQueryResult>>> GetAll(GetAllUserQuery? request = null);
    Task<bool> IsUsernameUnique(string username, long userId);
    Task<bool> IsUsrConfigSatisfied(long configurationId, long userId);
    Task<LoginUserQueryResult> GetPermissions(long userId);
    Task<GetUserQueryResult> FindById(long id);
    Task<GetInfoByUserIdQueryResult> GetInfoByUserId(long userId);
    Task<GetProfileByProfileIdQueryResult> GetProfileByProfileId(long profileId);
    Task<long> GetProfileIdyUserId(long userId);
    Task<GetUserRoleQueryResult> GetUserRole(long userRoleId);
    Task<Result<List<GetUserPermissionQueryResult>>> GetUserPermission(long userId, long formId);
    Task<GetUserLocationQueryResult> GetUserLocation(long userLocationId);
    Task<GetUserAggregateQueryResult> GetUserAggreagate(long userId);
    Task<bool> IsCompanyMatchPersonCompany(long companyId, long profileId);
    Task<string> GetUserMobileNumber(long userId);
    Task<List<long>> GetUserPermissonByFormId(long formId, long userId);

}
