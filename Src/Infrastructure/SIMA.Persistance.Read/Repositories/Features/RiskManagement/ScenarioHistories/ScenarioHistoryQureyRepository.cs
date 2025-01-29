using ArmanIT.Investigation.Dapper.QueryBuilder;
using Dapper;
using Microsoft.Extensions.Configuration;
using SIMA.Application.Query.Contract.Features.RiskManagement.ScenarioHistories;
using SIMA.Framework.Common.Exceptions;
using SIMA.Framework.Common.Helper;
using SIMA.Framework.Common.Response;
using System.Data.SqlClient;

namespace SIMA.Persistance.Read.Repositories.Features.RiskManagement.ScenarioHistories;

public class ScenarioHistoryQureyRepository : IScenarioHistoryQureyRepository
{
    private readonly string _connectionString;
    private readonly string _mainQuery;

    public ScenarioHistoryQureyRepository(IConfiguration configuration)
    {
        _connectionString = configuration.GetConnectionString();
        _mainQuery = @"
SELECT
	 SH.[Id]
    ,SH.[Name]
    ,SH.[Code]
    ,SH.[ActiveStatusId]
    ,SH.[CreatedAt]
	,SH.NumericValue
	,SH.ValueTitle
	,A.[Name] as ActiveStatus
FROM [RiskManagement].[ScenarioHistory] SH
INNER JOIN Basic.ActiveStatus A ON A.ID = SH.ActiveStatusId
WHERE SH.ActiveStatusId != 3
";
    }
    public async Task<Result<IEnumerable<GetScenarioHistoryQueryResult>>> GetAll(GetAllScenarioHistoryQuery request)
    {
        using var connection = new SqlConnection(_connectionString);
        await connection.OpenAsync();

        string queryCount = $@" WITH Query as(
						                    {_mainQuery}
							)
								SELECT Count(*) FROM Query
								 /**where**/
								 
								 ; ";


        string query = $@" WITH Query as(
							                  {_mainQuery}
							)
								SELECT * FROM Query
								 /**where**/
								 /**orderby**/
                                    OFFSET @Skip rows FETCH NEXT @PageSize rows only; ";
        var dynaimcParameters = DapperHelperExtention.GenerateQuery(queryCount + query, request);
        using var multi = await connection.QueryMultipleAsync(dynaimcParameters.Item1.RawSql, dynaimcParameters.Item2);
        var count = await multi.ReadFirstAsync<int>();
        var response = await multi.ReadAsync<GetScenarioHistoryQueryResult>();
        return Result.Ok(response, request, count);
    }

    public async Task<GetScenarioHistoryQueryResult> GetById(long id)
    {
        using var connection = new SqlConnection(_connectionString);
        await connection.OpenAsync();
        string query = $@"{_mainQuery} AND SH.Id = @Id";
        var result = await connection.QueryFirstOrDefaultAsync<GetScenarioHistoryQueryResult>(query, new { Id = id });
        result.NullCheck();
        return result ?? throw SimaResultException.NotFound;
    }
}