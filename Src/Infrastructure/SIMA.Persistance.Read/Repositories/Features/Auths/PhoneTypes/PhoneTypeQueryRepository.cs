using ArmanIT.Investigation.Dapper.QueryBuilder;
using Dapper;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using SIMA.Application.Query.Contract.Features.Auths.PhoneTypes;
using SIMA.Application.Query.Contract.Features.Auths.Positions;
using SIMA.Application.Query.Contract.Features.BranchManagement.Branches;
using SIMA.Application.Query.Contract.Features.WorkFlowEngine.WorkFlow.Step;
using SIMA.Framework.Common.Helper;
using SIMA.Framework.Common.Request;
using SIMA.Framework.Common.Response;

namespace SIMA.Persistance.Read.Repositories.Features.Auths.PhoneTypes;

public class PhoneTypeQueryRepository : IPhoneTypeQueryRepository
{
    private readonly string _connectionString;

    public PhoneTypeQueryRepository(IConfiguration configuration)
    {
        _connectionString = configuration.GetConnectionString();
    }

    public async Task<GetPhoneTypeQueryResult> FindById(long id)
    {
        using (var connection = new SqlConnection(_connectionString))
        {
            await connection.OpenAsync();
            string query = $@"
                SELECT DISTINCT P.[ID] as Id
                  ,P.[Name]
                  ,P.[Code]
                  ,P.[ActiveStatusID]
                  ,A.[Name] as ActiveStatus 
              FROM [Basic].[PhonType] P
              join [Basic].[ActiveStatus] A on A.Id = P.ActiveStatusID
              WHERE [ActiveStatusID] <> 3 AND P.Id = @Id";
            var result = await connection.QueryFirstOrDefaultAsync<GetPhoneTypeQueryResult>(query, new { Id = id });
            result.NullCheck();
            return result;
        }
    }

    public async Task<Result<IEnumerable<GetPhoneTypeQueryResult>>> GetAll(GetAllPhoneTypesQuery? request = null)
    {
        using (var connection = new SqlConnection(_connectionString))
        {
            await connection.OpenAsync();

            string queryCount = @"WITH Query as(
						  SELECT DISTINCT P.[ID] as Id
        ,P.[Name]
        ,P.[Code]
        ,P.[ActiveStatusID]
        ,A.[Name] as ActiveStatus 
        ,p.[CreatedAt]
FROM [Basic].[PhonType] P
join [Basic].[ActiveStatus] A on A.Id = P.ActiveStatusID
WHERE  P.ActiveStatusId != 3
							)
								SELECT Count(*) FROM Query
								 /**where**/
								 
								 ;";
            string query = $@"WITH Query as(
							SELECT DISTINCT P.[ID] as Id
        ,P.[Name]
        ,P.[Code]
        ,P.[ActiveStatusID]
        ,A.[Name] as ActiveStatus 
        ,p.[CreatedAt]
FROM [Basic].[PhonType] P
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
                var response = await multi.ReadAsync<GetPhoneTypeQueryResult>();
                return Result.Ok(response, request, count);
            }
        }
    }
}
