using ArmanIT.Investigation.Dapper.QueryBuilder;
using Dapper;
using Microsoft.Extensions.Configuration;
using SIMA.Application.Query.Contract.Features.BCP.SolutionPriorities;
using SIMA.Framework.Common.Exceptions;
using SIMA.Framework.Common.Response;
using System.Data.SqlClient;

namespace SIMA.Persistance.Read.Repositories.Features.BCP.SolutionPeriorities;

public class SolutionPriorityQueryRepository : ISolutionPriorityQueryRepository
{
    private readonly string _connectionString;
    private readonly string _mainQuery;
    public SolutionPriorityQueryRepository(IConfiguration configuration)
    {
        _connectionString = configuration.GetConnectionString();
        _mainQuery = @"
 Select 
	 SP.[Id]
	,SP.[Name]
	,SP.[Code]
    ,SP.CreatedAt
    ,SP.[Priority]
	,A.[Name] ActiveStatus
From [BCP].SolutionPriority SP
join Basic.ActiveStatus A on SP.ActiveStatusId = A.Id
WHERE SP.[ActiveStatusID] <> 3
";
    }

    public async Task<Result<IEnumerable<GetSolutionPriorityQueryResult>>> GetAll(GetAllSolutionPrioritiesQuery request)
    {
        using var connection = new SqlConnection(_connectionString);
        await connection.OpenAsync();
        var queryCount = $@"
                             WITH Query as(	{_mainQuery}	)
								SELECT Count(*) FROM Query
								 /**where**/
								 ;";

        string query = $@"WITH Query as(
							 {_mainQuery}
							)
								SELECT * FROM Query
								 /**where**/
								 /**orderby**/
                                    OFFSET @Skip rows FETCH NEXT @PageSize rows only;";
        var dynaimcParameters = (queryCount + query).GenerateQuery(request);
        using var multi = await connection.QueryMultipleAsync(dynaimcParameters.Item1.RawSql, dynaimcParameters.Item2);
        var count = await multi.ReadFirstAsync<int>();
        var response = await multi.ReadAsync<GetSolutionPriorityQueryResult>();
        return Result.Ok(response, request, count);
    }

    public async Task<GetSolutionPriorityQueryResult> GetById(GetSolutionPriorityQuery request)
    {
        var query = $@"
          {_mainQuery}
          AND SP.[Id] = @Id";
        using var connection = new SqlConnection(_connectionString);
        await connection.OpenAsync();
        var result = await connection.QueryFirstAsync<GetSolutionPriorityQueryResult>(query, new { request.Id });
        return result ?? throw SimaResultException.NotFound;
    }
}