using Dapper;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using SIMA.Application.Query.Contract.Features.Auths.Groups;
using SIMA.Application.Query.Contract.Features.IssueManagement.IssueLinkReasons;
using SIMA.Framework.Common.Exceptions;
using SIMA.Framework.Common.Helper;
using SIMA.Framework.Common.Request;
using SIMA.Framework.Common.Response;

namespace SIMA.Persistance.Read.Repositories.Features.Auths.Groups;

public class GroupQueryRepository : IGroupQueryRepository
{
    private readonly string _connectionString;

    public GroupQueryRepository(IConfiguration configuration)
    {
        _connectionString = configuration.GetConnectionString();
    }

    public async Task<GetGroupQueryResult> FindById(long id)
    {
        var query = @"SELECT
                           g.[ID] Id
                          ,g.[Name]
                          ,g.[Code]
                          ,a.ID ActiveStatusId
                          ,a.Name ActiveStatus
                      FROM [Authentication].[Groups] g
                            join Basic.ActiveStatus a
                            on g.ActiveStatusId = a.ID
                      left join [Authentication].[UserGroup] ug on ug.GroupID = g.ID
                      left join [Authentication].[GroupPermission] gp on gp.GroupID = g.ID
                      where g.ID = @Id";
        using var connection = new SqlConnection(_connectionString);
        connection.Open();
        var result = await connection.QueryFirstOrDefaultAsync<GetGroupQueryResult>(query, new { Id = id });
        result.NullCheck();
        return result;
    }

    public async Task<Result<IEnumerable<GetGroupQueryResult>>> GetAll(GetAllGroupQuery request)
    {
        
        using (var connection = new SqlConnection(_connectionString))
        {
            await connection.OpenAsync();
            var queryCount = @" SELECT  COUNT(*) Result
                               FROM [Authentication].[Groups] g
                              join Basic.ActiveStatus a on g.ActiveStatusId = a.ID
                              WHERE (@SearchValue is null OR (g.Name like @SearchValue OR g.Code like @SearchValue)) AND g.[ActiveStatusID] <> 3";

            string query = $@" select
                                   g.[ID] Id
                                  ,g.[Name]
                                  ,g.[Code]
                                  ,a.ID ActiveStatusId
                                  ,a.Name ActiveStatus
                                  ,g.[CreatedAt]
                            FROM [Authentication].[Groups] g
                            join Basic.ActiveStatus a
                            on g.ActiveStatusId = a.ID
                           WHERE (@SearchValue is null OR (g.Name like @SearchValue OR g.Code like @SearchValue)) AND g.[ActiveStatusID] <> 3
                          order by {request.Sort?.Replace(":", " ") ?? "CreatedAt desc"}
                          OFFSET @Skip rows FETCH NEXT @PageSize rows only;";

            using (var multi = await connection.QueryMultipleAsync(query + queryCount, new
            {
                SearchValue = request.Filter is null ? null : "%" + request.Filter + "%",
                request.Skip,
                request.PageSize
            }))
            {
                var response = await multi.ReadAsync<GetGroupQueryResult>();
                var count = await multi.ReadSingleAsync<int>();
                return Result.Ok(response, count, request.PageSize, request.Page);
            }
        }
    }

    public async Task<GetGroupAggregateResult> GetGroupAggregate(long groupId)
    {
        var response = new GetGroupAggregateResult();
        using (var connection = new SqlConnection(_connectionString))
        {
            var query = $@"
                       SELECT
                           G.[ID] Id
                          ,G.[Name]
                          ,G.[Code]
                          ,a.ID ActiveStatusId
                          ,a.Name ActiveStatus
                      FROM [Authentication].[Groups] G
                            join Basic.ActiveStatus a
                            on G.ActiveStatusId = a.ID
                            WHERE G.ID = @GroupId and G.[ActiveStatusID] <> 3
                            GROUP BY G.ID,G.Name,G.Code,a.ID ,a.Name 

                            SELECT DISTINCT D.Name as DomainName, D.ID as DomainId, P.Name as PermissionName, P.ID as PermissionId
                            FROM [Authentication].[Groups] G
                            INNER JOIN [Authentication].[GroupPermission] GP on GP.GroupID = G.ID
                            INNER JOIN [Authentication].[Permission] P on P.ID = GP.PermissionID
                            INNER JOIN [Authentication].[Domain] D on D.ID = P.DomainID
                            WHERE G.ID = @GroupId and G.[ActiveStatusID] <> 3
                            GROUP BY D.Name, D.ID, P.Name, P.ID;

                            SELECT DISTINCT U.ID as UserId, (P.FirstName + ' ' + p.LastName) as FullName, P.NationalID as NationalCode
                            FROM [Authentication].[Groups] G
                            INNER JOIN [Authentication].[UserGroup] UG on UG.GroupID = G.ID
                            INNER JOIN [Authentication].[Users] U on U.ID = UG.UserID
                            INNER JOIN [Authentication].[Profile] P on P.ID = U.ProfileID
                            WHERE G.ID = @GroupId and G.[ActiveStatusID] <> 3";
            using (var multi = await connection.QueryMultipleAsync(query, new { GroupId = groupId }))
            {
                response.Group = multi.ReadAsync<GetGroupResultForAggregate>().GetAwaiter().GetResult().Single();
                response.GroupPermissions = multi.ReadAsync<GetGroupPermissionResultForAggregate>().GetAwaiter().GetResult().ToList();
                response.UsrGroups = multi.ReadAsync<GetUserGroupResultForAggregate>().GetAwaiter().GetResult().ToList();
            }
        }
        return response;
    }
    public async Task<GetGroupPermissionQueryResult> GetGroupPermission(long groupPermissionId)
    {
        using (var connection = new SqlConnection(_connectionString))
        {
            await connection.OpenAsync();
            string query = @"
                           SELECT DISTINCT
                           	   g.[Id]
                              ,g.[GroupId]
                              ,g.[PermissionId]
                              ,a.ID ActiveStatusId
                              ,a.Name ActiveStatus
                             FROM [Authentication].[GroupPermission] g
                              join Basic.ActiveStatus a
                                 on g.ActiveStatusId = a.ID
                               WHERE  g.Id = @Id
";
            var result = await connection.QueryFirstOrDefaultAsync<GetGroupPermissionQueryResult>(query, new { Id = groupPermissionId });
            result.NullCheck();
            if (result.ActiveStatusId == 3) throw SimaResultException.GroupMemberShipIsExpiredError;
            if (result.ActiveStatusId == 2 || result.ActiveStatusId == 4) throw SimaResultException.GroupMemberShipIsDeactiveError;
            return result;
        }
    }
    public async Task<GetUserGroupQueryResult> GetGroupUser(long userGroupId)
    {
        using (var connection = new SqlConnection(_connectionString))
        {
            await connection.OpenAsync();
            string query = @"
                             SELECT DISTINCT 
                                    g.[Id]
                                   ,g.[UserId]
                                   ,g.[GroupId]
                                   ,a.ID ActiveStatusId
                                   ,a.Name ActiveStatus
                               FROM [Authentication].[UserGroup] g
                                      join Basic.ActiveStatus a
                                      on g.ActiveStatusId = a.ID
                               WHERE g.ActiveStatusId <> 3 AND g.Id = @Id
";
            var result = await connection.QueryFirstOrDefaultAsync<GetUserGroupQueryResult>(query, new { Id = userGroupId });
            result.NullCheck();
            return result;
        }
    }
}
