using ArmanIT.Investigation.Dapper.QueryBuilder;
using Dapper;
using Microsoft.Extensions.Configuration;
using SIMA.Application.Query.Contract.Features.RiskManagement.CobitScenarios;
using SIMA.Framework.Common.Exceptions;
using SIMA.Framework.Common.Helper;
using SIMA.Framework.Common.Response;
using System.Data.SqlClient;

namespace SIMA.Persistance.Read.Repositories.Features.RiskManagement.CobitScenarios;

public class CobitScenarioQueryRepository : ICobitScenarioQueryRepository
{
    private readonly string _connectionString;
    private readonly string _mainQuery;
    public CobitScenarioQueryRepository(IConfiguration configuration)
    {
        _connectionString = configuration.GetConnectionString();
        _mainQuery = @"
SELECT
	CS.[Id]
    ,CS.Description
	,CS.Name
	,CS.CobitRiskCategoryId
	,CS.CobitIdentifier
	,CC.Name CobitScenarioCategoryName
	,A.[Name] ActiveStatus
    ,CS.CreatedAt
FROM [RiskManagement].[CobitScenario] CS
INNER JOIN [Basic].[ActiveStatus] A ON CS.ActiveStatusId = A.ID
INNER JOIN RiskManagement.CobitRiskCategory CC on CC.Id = CS.CobitRiskCategoryId AND CC.ActiveStatusId<>3
WHERE CS.ActiveStatusId <> 3
";
    }

    public async Task<Result<IEnumerable<GetCobitScenarioQueryResult>>> GetAll(GetAllCobitScenariosQuery request)
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
        var dynaimcParameters = (queryCount + query).GenerateQuery(request);
        using var multi = await connection.QueryMultipleAsync(dynaimcParameters.Item1.RawSql, dynaimcParameters.Item2);
        var count = await multi.ReadFirstAsync<int>();
        var response = await multi.ReadAsync<GetCobitScenarioQueryResult>();
        return Result.Ok(response, request, count);
    }

    public async Task<IEnumerable<GetCobitScenarioQueryResult>> GetAllByCategory(GetCobitScenariosByCategoryQuery request)
    {
        var query = $@"
          {_mainQuery} AND CS.CobitRiskCategoryId = @CategoryId
";
        using var connection = new SqlConnection(_connectionString);
        await connection.OpenAsync();
        var result = await connection.QueryAsync<GetCobitScenarioQueryResult>(query, new { request.CategoryId });
        result.NullCheck();
        return result ?? throw SimaResultException.NotFound;

    }

    public async Task<GetCobitScenarioQueryResult> GetById(GetCobitScenarioQuery request)
    {
        var query = $@"
          {_mainQuery} AND CS.Id = @Id
";
        using var connection = new SqlConnection(_connectionString);
        await connection.OpenAsync();
        var result = await connection.QueryFirstAsync<GetCobitScenarioQueryResult>(query, new { request.Id });
        result.NullCheck();
        return result ?? throw SimaResultException.NotFound;
    }
}