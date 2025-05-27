using ArmanIT.Investigation.Dapper.QueryBuilder;
using Dapper;
using Microsoft.Extensions.Configuration;
using SIMA.Application.Query.Contract.Features.RiskManagement.RiskValues;
using SIMA.Framework.Common.Exceptions;
using SIMA.Framework.Common.Helper;
using SIMA.Framework.Common.Response;
using System.Data.SqlClient;

namespace SIMA.Persistance.Read.Repositories.Features.RiskManagement.RiskValues;

public class RiskValueQueryRepository : IRiskValueQueryRepository
{
    private readonly string _connectionString;
    private readonly string _mainQuery;
    public RiskValueQueryRepository(IConfiguration configuration)
    {
        _connectionString = configuration.GetConnectionString();
        _mainQuery = @"
SELECT   RV.[Id]
		,RV.[Name]
		,RV.[Code]
		,RV.[Condition]
		,RV.NumericValue
		,RV.Color
		,A.[Name] ActiveStatus
		,RV.CreatedAt
FROM [RiskManagement].[RiskValue] RV
INNER JOIN [Basic].[ActiveStatus] A ON RV.ActiveStatusId = A.ID
WHERE RV.ActiveStatusId <> 3
";
    }

    public async Task<Result<IEnumerable<GetRiskValueQueryResult>>> GetAll(GetAllRiskValuesQuery request)
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
        var response = await multi.ReadAsync<GetRiskValueQueryResult>();
        return Result.Ok(response, request, count);
    }

    public async Task<GetRiskValueQueryResult> GetById(GetRiskValueQuery request)
    {
        var query = $@"
          {_mainQuery} AND RV.Id = @Id
";
        using var connection = new SqlConnection(_connectionString);
        await connection.OpenAsync();
        var result = await connection.QueryFirstAsync<GetRiskValueQueryResult>(query, new { request.Id });
        result.NullCheck();
        return result ?? throw SimaResultException.NotFound;
    }
}