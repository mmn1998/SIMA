using ArmanIT.Investigation.Dapper.QueryBuilder;
using Dapper;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using SIMA.Application.Query.Contract.Features.Auths.Permission;
using SIMA.Application.Query.Contract.Features.Auths.Positions;
using SIMA.Application.Query.Contract.Features.BranchManagement.Branches;
using SIMA.Application.Query.Contract.Features.WorkFlowEngine.WorkFlow.Step;
using SIMA.Domain.Models.Features.Auths.Domains.ValueObjects;
using SIMA.Framework.Common.Helper;
using SIMA.Framework.Common.Request;
using SIMA.Framework.Common.Response;

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

    public async Task<Result<IEnumerable<GetPermissionQueryResult>>> GetAll(GetAllPermissionsByDomainIdQuery request)
    {
        using (var connection = new SqlConnection(_connectionString))
        {
            await connection.OpenAsync();

            string queryCount = @"WITH Query as(
						  SELECT DISTINCT P.[Id] 
      ,P.[Name]
      ,P.[Code]
      ,P.[ActiveStatusID]
      ,A.[Name] as ActiveStatus 
      ,p.[CreatedAt]
FROM [Authentication].[Permission] P 
join [Basic].[ActiveStatus] A on A.Id = P.ActiveStatusID
WHERE  P.ActiveStatusId != 3
							)
								SELECT Count(*) FROM Query
								 /**where**/
								 
								 ;";
            string query = $@"WITH Query as(
							SELECT DISTINCT P.[Id] 
      ,P.[Name]
      ,P.[Code]
      ,P.[ActiveStatusID]
      ,A.[Name] as ActiveStatus 
      ,p.[CreatedAt]
FROM [Authentication].[Permission] P 
join [Basic].[ActiveStatus] A on A.Id = P.ActiveStatusID
WHERE  P.ActiveStatusId != 3
							)
								SELECT * FROM Query
								 /**where**/
								 /**orderby**/
                                    OFFSET @Skip rows FETCH NEXT @PageSize rows only;";
            var dynaimcParameters = DapperHelperExtention.GenerateQuery(queryCount + query, request);
            using (var multi = await connection.QueryMultipleAsync(dynaimcParameters.Item1.RawSql, dynaimcParameters.Item2))
            {
                var count = await multi.ReadFirstAsync<int>();
                var response = await multi.ReadAsync<GetPermissionQueryResult>();
                return Result.Ok(response, request, count);
            }

        }
    }

    public async Task<Result<IEnumerable<GetPermissionQueryResult>>> GetAll(GetAllPermissionsByDomainIdQuery request, long domainId)
    {
        using (var connection = new SqlConnection(_connectionString))
        {

            string queryCount = @"WITH Query as(
						  SELECT DISTINCT P.[Id] 
      ,P.[Name]
      ,P.[Code]
      ,P.[ActiveStatusID]
      ,A.[Name] as ActiveStatus 
      ,p.[CreatedAt]
FROM [Authentication].[Permission] P 
join [Basic].[ActiveStatus] A on A.Id = P.ActiveStatusID
 WHERE  P.[ActiveStatusID] != 3 and P.[DomainId] = @DomainId
							)
								SELECT Count(*) FROM Query
								 /**where**/
								 
								 ;";
            await connection.OpenAsync();
            string query = $@"WITH Query as(
							SELECT DISTINCT P.[Id] 
      ,P.[Name]
      ,P.[Code]
      ,P.[ActiveStatusID]
      ,A.[Name] as ActiveStatus 
      ,p.[CreatedAt]
FROM [Authentication].[Permission] P 
join [Basic].[ActiveStatus] A on A.Id = P.ActiveStatusID
 WHERE  P.[ActiveStatusID] != 3 and P.[DomainId] = @DomainId
							)
								SELECT * FROM Query
								 /**where**/
								 /**orderby**/
                                    OFFSET @Skip rows FETCH NEXT @PageSize rows only;";

            var dynaimcParameters = DapperHelperExtention.GenerateQuery(queryCount + query, request);

            dynaimcParameters.Item2.Add("DomainId", request.DomainId);
            using (var multi = await connection.QueryMultipleAsync(dynaimcParameters.Item1.RawSql, dynaimcParameters.Item2))
            {
                var count = await multi.ReadFirstAsync<int>();
                var response = await multi.ReadAsync<GetPermissionQueryResult>();
                return Result.Ok(response, request, count);
            }

        }
    }
}