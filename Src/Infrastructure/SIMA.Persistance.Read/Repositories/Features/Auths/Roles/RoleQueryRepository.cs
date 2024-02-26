using Dapper;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using SIMA.Application.Query.Contract.Features.Auths.Roles;
using SIMA.Framework.Common.Helper;
using SIMA.Framework.Common.Request;
using SIMA.Framework.Common.Response;
using SIMA.Persistance.Persistence;

namespace SIMA.Persistance.Read.Repositories.Features.Auths.Roles;

public class RoleQueryRepository : IRoleQueryRepository
{
    private readonly SIMADBContext _context;
    private readonly string _connectionString;

    public RoleQueryRepository(SIMADBContext context, IConfiguration configuration)
    {
        _context = context;
        _connectionString = configuration.GetConnectionString();
    }

    public async Task<GetRoleQueryResult> FindById(long id)
    {
        using (var connection = new SqlConnection(_connectionString))
        {
            await connection.OpenAsync();
            string query = $@"
                SELECT DISTINCT R.[ID] as Id
                  ,R.[Name]
                  ,R.[Code]
                  ,R.[ActiveStatusId]
                  ,A.[Name] as ActiveStatus
              FROM [Authentication].[Role] R
              join [Basic].[ActiveStatus] A on A.Id = R.ActiveStatusID
              WHERE R.[ActiveStatusID] <> 3 AND R.Id = @Id";
            var result = await connection.QueryFirstOrDefaultAsync<GetRoleQueryResult>(query, new { Id = id });
            result.NullCheck();
            return result;
        }
    }

    public async Task<Result<List<GetRoleQueryResult>>> GetAll(BaseRequest? baseRequest = null)
    {
        var response = new List<GetRoleQueryResult>();
        int totalCount = 0;
        if (baseRequest != null && !string.IsNullOrEmpty(baseRequest.SearchValue))
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();
                string query = $@"
               SELECT DISTINCT R.[ID] as Id
                  ,R.[Name]
                  ,R.[Code]
                  ,R.[ActiveStatusId]
                  ,A.[Name] as ActiveStatus
,r.[CreatedAt]
              FROM [Authentication].[Role] R
              join [Basic].[ActiveStatus] A on A.Id = R.ActiveStatusID
              WHERE (Name like @SearchValue OR Code like @SearchValue) AND [ActiveStatusID] <> 3
Order By r.[CreatedAt] desc  
";
                var result = await connection.QueryAsync<GetRoleQueryResult>(query, new { SearchValue = "%" + baseRequest.SearchValue + "%" });
                totalCount = result.Count();
                response = result.Skip((baseRequest.Take - 1) * baseRequest.Skip).Take(baseRequest.Take).ToList();
            }
        }
        else if (baseRequest != null && string.IsNullOrEmpty(baseRequest.SearchValue))
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();
                string query = $@"
               SELECT DISTINCT R.[ID] as Id
                  ,R.[Name]
                  ,R.[Code]
                  ,R.[ActiveStatusId]
                  ,A.[Name] as ActiveStatus
,r.[CreatedAt]
              FROM [Authentication].[Role] R
              join [Basic].[ActiveStatus] A on A.Id = R.ActiveStatusID
              WHERE [ActiveStatusID] <> 3
Order By r.[CreatedAt] desc  
";
                var result = await connection.QueryAsync<GetRoleQueryResult>(query);
                totalCount = result.Count();
                response = result.Skip((baseRequest.Take - 1) * baseRequest.Skip).Take(baseRequest.Take).ToList();
            }
        }
        return Result.Ok(response, totalCount);
    }

    public async Task<GetRolePermissionQueryResult> GetRolePermission(long rolePermissionId)
    {
        using (var connection = new SqlConnection(_connectionString))
        {
            await connection.OpenAsync();
            string query = @"
                SELECT DISTINCT
                	   RP.[Id]
                      ,RP.[RoleId]
                      ,RP.[PermissionId]
                      ,RP.[ActiveStatusId]
                      ,A.[Name] as ActiveStatus
                  FROM [Authentication].[RolePermission] RP
                  join [Basic].[ActiveStatus] A on A.Id = RP.ActiveStatusID
                  WHERE RP.ActiveStatusId <> 3 AND RP.Id = @Id
";
            var result = await connection.QueryFirstOrDefaultAsync<GetRolePermissionQueryResult>(query, new { Id = rolePermissionId });
            result.NullCheck();
            return result;
        }
    }

    public async Task<bool> IsRoleSatisfied(string code, string englishKey)
    {
        return !await _context.Roles.AnyAsync(r => r.Code == code && r.EnglishKey == englishKey);
    }

    public async Task<GetRoleAggregateResult> GetRoleAggegate(long roleId)
    {
        var response = new GetRoleAggregateResult();
        using (var connection = new SqlConnection(_connectionString))
        {
            var query = $@"
                          SELECT DISTINCT R.[ID]
                          ,R.[Name]
                          ,R.[Code]
                          ,R.[ActiveStatusID]
                          ,A.[Name] as ActiveStatus
                          FROM [Authentication].[Role] R
                          join [Basic].[ActiveStatus] A on A.Id = R.ActiveStatusID
                          WHERE R.ID = @RoleId and R.[ActiveStatusID] <> 3
                          GROUP BY R.ID,R.Name,R.Code,R.ActiveStatusID,A.Name;


                          SELECT DISTINCT
                          D.Name as DomainName
                          ,D.ID as DomainId
                          ,P.Name as PermissionName
                          ,P.ID as PermissionId
                          ,R.[ActiveStatusID]
                          ,A.[Name] as ActiveStatus
,rp.[CreatedAt]
                          FROM [Authentication].[Role] R
                          INNER JOIN [Authentication].[RolePermission] RP on RP.RoleID = R.ID
                          INNER JOIN [Authentication].[Permission] P on P.ID = RP.PermissionID
                          INNER JOIN [Authentication].[Domain] D on D.ID = P.DomainID
                          join [Basic].[ActiveStatus] A on A.Id = R.ActiveStatusID
                          WHERE R.ID = @RoleId and R.[ActiveStatusID] <> 3
                          GROUP BY D.Name, D.ID, P.Name, P.ID ,R.ActiveStatusID,A.Name 
Order By rp.[CreatedAt] desc  ";
            using (var multi = await connection.QueryMultipleAsync(query, new { RoleId = roleId }))
            {
                response.Role = multi.ReadAsync<GetRoleQueryResultForAggregate>().GetAwaiter().GetResult().Single();
                response.RolePermissions = multi.ReadAsync<GetRolePermissionQueryResultForAggregate>().GetAwaiter().GetResult().ToList();
            }
        }
        return response;
    }
}
