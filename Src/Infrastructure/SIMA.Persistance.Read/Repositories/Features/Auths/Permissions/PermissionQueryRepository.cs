using Dapper;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using SIMA.Application.Query.Contract.Features.Auths.Permission;
using SIMA.Framework.Common.Helper;

namespace SIMA.Persistance.Read.Repositories.Features.Auths.Permissions;

public class PermissionQueryRepository : IPermissionQueryRepository
{
    private readonly string _connectionString;

    public PermissionQueryRepository(IConfiguration configuration)
    {
        _connectionString = configuration.GetConnectionString();
    }

    public async Task<GetPermissionQueryResult> FindById(long id)
    {
        using (var connection = new SqlConnection(_connectionString))
        {
            await connection.OpenAsync();
            string query = @"
                SELECT DISTINCT P.[Id] 
                      ,P.[Name]
                      ,P.[Code]
                      ,P.[ActiveStatusID]
                      ,A.[Name] as ActiveStatus 
                  FROM [Authentication].[Permission] P 
                  join [Basic].[ActiveStatus] A on A.Id = P.ActiveStatusID
                  WHERE P.Id = @Id and P.[ActiveStatusID] != 3 ";
            var result = await connection.QueryFirstOrDefaultAsync<GetPermissionQueryResult>(query, new { Id = id });
            result.NullCheck();
            return result;
        }
    }

    public async Task<List<GetPermissionQueryResult>> GetAll()
    {

        try
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();
                string query = @"
                SELECT DISTINCT P.[Id] 
                      ,P.[Name]
                      ,P.[Code]
                      ,P.[ActiveStatusID]
                      ,A.[Name] as ActiveStatus 
,p.[CreatedAt]
                  FROM [Authentication].[Permission] P 
                  join [Basic].[ActiveStatus] A on A.Id = P.ActiveStatusID
                  WHERE  P.[ActiveStatusID] != 3 
Order By p.[CreatedAt] desc";
                var result = await connection.QueryAsync<GetPermissionQueryResult>(query);
                result.NullCheck();
                return result.ToList();
            }
        }
        catch (Exception e)
        {

            throw;
        }
    }

    public async Task<List<GetPermissionQueryResult>> GetAll(long domainId)
    {

        using (var connection = new SqlConnection(_connectionString))
        {
            await connection.OpenAsync();
            string query = @"
                SELECT DISTINCT P.[Id] 
                      ,P.[Name]
                      ,P.[Code]
                      ,P.[ActiveStatusID]
                      ,A.[Name] as ActiveStatus 
,p.[CreatedAt]
                  FROM [Authentication].[Permission] P 
                  join [Basic].[ActiveStatus] A on A.Id = P.ActiveStatusID
                  WHERE  P.[ActiveStatusID] != 3 and P.[DomainId] = @DomainId 
Order By p.[CreatedAt] desc";
            var result = await connection.QueryAsync<GetPermissionQueryResult>(query, new { DomainId = domainId });
            result.NullCheck();
            return result.ToList();
        }
    }


}
