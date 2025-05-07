using ArmanIT.Investigation.Dapper.QueryBuilder;
using Dapper;
using Microsoft.Extensions.Configuration;
using SIMA.Application.Query.Contract.Features.RiskManagement.RiskLevelCobits;
using SIMA.Framework.Common.Exceptions;
using SIMA.Framework.Common.Helper;
using SIMA.Framework.Common.Response;
using System.Data.SqlClient;

namespace SIMA.Persistance.Read.Repositories.Features.RiskManagement.RiskLevelCobits;

public class RiskLevelCobitQueryRepository : IRiskLevelCobitQueryRepository
{
    private readonly string _connectionString;
    private readonly string _mainQuery;
    public RiskLevelCobitQueryRepository(IConfiguration configuration)
    {
        _connectionString = configuration.GetConnectionString();
        _mainQuery = @"
SELECT
	RLC.[Id]
    ,RLC.[Code]
	,RLC.CurrentOccurrenceProbabilityValueId
	,COPV.Name CurrentOccurrenceProbabilityValueName
	,SV.Name SeverityValueName
	,RLC.NumericValue
	,RLC.SeverityId
	,S.Code SeverityName
	,A.[Name] ActiveStatus
    ,RLC.CreatedAt
FROM [RiskManagement].[RiskLevelCobit] RLC
INNER JOIN [Basic].[ActiveStatus] A ON RLC.ActiveStatusId = A.ID
INNER JOIN RiskManagement.Severity S on S.Id = RLC.SeverityId AND S.ActiveStatusId<>3
INNER JOIN RiskManagement.SeverityValue SV on SV.Id = S.SeverityValueId AND SV.ActiveStatusId<>3
INNER JOIN RiskManagement.CurrentOccurrenceProbabilityValue COPV on COPV.Id = RLC.CurrentOccurrenceProbabilityValueId AND COPV.ActiveStatusId<>3
WHERE RLC.ActiveStatusId <> 3
";
    }

    public async Task<Result<IEnumerable<GetRiskLevelCobitQueryResult>>> GetAll(GetAllRiskLevelCobitsQuery request)
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
        var response = await multi.ReadAsync<GetRiskLevelCobitQueryResult>();
        return Result.Ok(response, request, count);
    }

    public async Task<GetRiskLevelCobitQueryResult> GetById(GetRiskLevelCobitQuery request)
    {
        var query = $@"
          {_mainQuery} AND RLC.Id = @Id
";
        using var connection = new SqlConnection(_connectionString);
        await connection.OpenAsync();
        var result = await connection.QueryFirstAsync<GetRiskLevelCobitQueryResult>(query, new { request.Id });
        result.NullCheck();
        return result ?? throw SimaResultException.NotFound;
    }
}