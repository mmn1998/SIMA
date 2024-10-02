using ArmanIT.Investigation.Dapper.QueryBuilder;
using Dapper;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using SIMA.Application.Query.Contract.Features.Auths.Groups;
using SIMA.Framework.Common.Helper;
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

            var queryCount = @" WITH Query as(
						  select
                                   g.[ID] Id
                                  ,g.[Name]
                                  ,g.[Code]
                                  ,a.ID ActiveStatusId
                                  ,a.Name ActiveStatus
                                  ,g.[CreatedAt]
                            FROM [Authentication].[Groups] g
                            join Basic.ActiveStatus a
                            on g.ActiveStatusId = a.ID
                           WHERE  g.[ActiveStatusID] <> 3
							)
								SELECT Count(*) FROM Query
								 /**where**/
								 
								 ;";

            string query = $@"WITH Query as( select
                                   g.[ID] Id
                                  ,g.[Name]
                                  ,g.[Code]
                                  ,a.ID ActiveStatusId
                                  ,a.Name ActiveStatus
                                  ,g.[CreatedAt]
                            FROM [Authentication].[Groups] g
                            join Basic.ActiveStatus a
                            on g.ActiveStatusId = a.ID
                           WHERE  g.[ActiveStatusID] <> 3
							)
								SELECT * FROM Query
								 /**where**/
								 /**orderby**/
                                    OFFSET @Skip rows FETCH NEXT @PageSize rows only;";
            var dynaimcParameters = DapperHelperExtention.GenerateQuery(queryCount + query, request);
            using (var multi = await connection.QueryMultipleAsync(dynaimcParameters.Item1.RawSql, dynaimcParameters.Item2))
            {
                var count = await multi.ReadFirstAsync<int>();
                var response = await multi.ReadAsync<GetGroupQueryResult>();
                return Result.Ok(response, request, count);
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
                         GROUP BY G.ID,G.Name,G.Code,a.ID ,a.Name; 

                        Select 
                         F.Id  FormId
                        ,F.Name FormName 
                           ,F.Title FormTitle
                        ,D.Id DomainId
                        ,D.Name DomainName 
                        from Authentication.Groups G
                        join Authentication.FormGroup FG on FG.GroupId = G.Id
                        join Authentication.Form F on F.Id = FG.FormId 
                        join Authentication.DomainForms DF on F.Id  = DF.FormId
                        join Authentication.Domain D on DF.DomainId = D.Id
                     where G.ID = @GroupId and FG.ActiveStatusId <> 3

                         SELECT DISTINCT U.ID as UserId, (P.FirstName + ' ' + p.LastName) as FullName, P.NationalID as NationalCode
                         FROM [Authentication].[Groups] G
                         INNER JOIN [Authentication].[UserGroup] UG on UG.GroupID = G.ID
                         INNER JOIN [Authentication].[Users] U on U.ID = UG.UserID
                         INNER JOIN [Authentication].[Profile] P on P.ID = U.ProfileID
                         WHERE G.ID = @GroupId and G.[ActiveStatusID] <> 3 and UG.[ActiveStatusID] <> 3


                         Select 
                          G.Id GroupId
                         ,FP.FormId FormId 
                         ,P.Name PermissionName 
                         ,P.Id PermissionId
                         from Authentication.Groups G
                         join Authentication.GroupPermission GP on GP.GroupId = G.Id and GP.ActiveStatusId <>3
                         join Authentication.FormPermission FP on FP.PermissionId= GP.PermissionId and FP.ActiveStatusId <>3
                         join Authentication.Permission P on GP.PermissionId = P.Id and P.ActiveStatusId <>3
                         where GP.GroupId = @groupId ";

            using (var multi = await connection.QueryMultipleAsync(query, new { GroupId = groupId }))
            {
                response = multi.ReadAsync<GetGroupAggregateResult>().GetAwaiter().GetResult().Single();
                var formGroups = multi.ReadAsync<GetFormGroupQuery>().GetAwaiter().GetResult().ToList();
                response.UsrGroups = multi.ReadAsync<GetUserGroupResultForAggregate>().GetAwaiter().GetResult().ToList();
                var groupPermission = multi.ReadAsync<GetGroupPermissionQueryResult>().GetAwaiter().GetResult().ToList();


                var formPermission = formGroups.Select(form => new GetGroupFormPermissions
                {
                    Form = form,
                    Permissions = groupPermission.Where(p => p.FormId == form.FormId).ToList()
                }).ToList();

                response.FormPermissions = formPermission;

            }
        }
        return response;
    }
    public async Task<Result<List<GetGroupPermissionQueryResult>>> GetGroupPermission(long formId , long groupId)
    {
        using (var connection = new SqlConnection(_connectionString))
        {
            await connection.OpenAsync();
            string query = @"
                           Select 
                             G.Id GroupId
                            ,F.Id FormId 
                            ,F.Name FormName 
                            ,F.Title FormTitle
                            ,P.Name PermissionName 
                            ,P.Id PermissionId
                            from Authentication.Groups G
                            join Authentication.GroupPermission GP on GP.GroupId = G.Id and GP.ActiveStatusId <>3
                            join Authentication.FormPermission FP on FP.PermissionId= GP.PermissionId and FP.ActiveStatusId <>3
                            join Authentication.Permission P on GP.PermissionId = P.Id and P.ActiveStatusId <>3
                            join Authentication.Form F on FP.FormId = F.Id and F.ActiveStatusId <>3
                            where FP.FormId = @formId and GP.GroupId = @groupId 
";
            var result = await connection.QueryAsync<GetGroupPermissionQueryResult>(query, new { formId , groupId });
            return Result.Ok(result.ToList());
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
