using ArmanIT.Investigation.Dapper.QueryBuilder;
using Dapper;
using Microsoft.Extensions.Configuration;
using SIMA.Application.Query.Contract.Features.RiskManagement.RiskLevels;
using SIMA.Framework.Common.Exceptions;
using SIMA.Framework.Common.Helper;
using SIMA.Framework.Common.Response;
using System.Data.SqlClient;

namespace SIMA.Persistance.Read.Repositories.Features.RiskManagement.RiskLevels;

public class RiskLevelQueryRepository : IRiskLevelQueryRepository
{
    private readonly string _connectionString;
    private readonly string _mainQuery;
    public RiskLevelQueryRepository(IConfiguration configuration)
    {
        _connectionString = configuration.GetConnectionString();
        _mainQuery = @"
SELECT DISTINCT T.[Id]
        ,T.[Code]
		,T.SeverityValueId
		,SV.Name SeverityValueName
		,T.RiskValueId
		,RV.Name RiskValueName
		,T.CurrentOccurrenceProbabilityValueId
		,COPV.Name CurrentOccurrenceProbabilityValueName
        ,T.[ActiveStatusId]
        ,T.[CreatedAt]
        , S.[Name] as ActiveStatus
    FROM [RiskManagement].[RiskLevel] T
    INNER JOIN [Basic].[ActiveStatus] S on S.ID = T.ActiveStatusId
	INNER JOIN RiskManagement.RiskValue RV ON RV.Id = T.RiskValueId and RV.ActiveStatusId<>3
	INNER JOIN RiskManagement.SeverityValue SV ON SV.Id = T.SeverityValueId and SV.ActiveStatusId<>3
	INNER JOIN RiskManagement.CurrentOccurrenceProbabilityValue COPV ON COPV.Id = T.CurrentOccurrenceProbabilityValueId and COPV.ActiveStatusId<>3
    WHERE T.ActiveStatusId != 3
";
    }

    public async Task<Result<IEnumerable<GetRiskLevelsQueryResult>>> GetAll(GetAllRiskLevelsQuery request)
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
        var response = await multi.ReadAsync<GetRiskLevelsQueryResult>();
        return Result.Ok(response, request, count);
    }

    public async Task<GetRiskLevelsQueryResult> GetById(long id)
    {
        using var connection = new SqlConnection(_connectionString);
        await connection.OpenAsync();
        string query = $@"{_mainQuery} AND T.Id = @Id 
              ";
        var result = await connection.QueryFirstOrDefaultAsync<GetRiskLevelsQueryResult>(query, new { Id = id });
        result.NullCheck();
        return result ?? throw SimaResultException.NotFound;
    }
}
