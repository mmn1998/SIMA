using ArmanIT.Investigation.Dapper.QueryBuilder;
using Dapper;
using Microsoft.Extensions.Configuration;
using SIMA.Application.Query.Contract.Features.RiskManagement.Severities;
using SIMA.Framework.Common.Exceptions;
using SIMA.Framework.Common.Helper;
using SIMA.Framework.Common.Response;
using System.Data.SqlClient;

namespace SIMA.Persistance.Read.Repositories.Features.RiskManagement.Severities;

public class SeverityQueryRepository : ISeverityQueryRepository
{
    private readonly string _connectionString;
    private readonly string _mainQuery;
    public SeverityQueryRepository(IConfiguration configuration)
    {
        _connectionString = configuration.GetConnectionString();
        _mainQuery = @"
SELECT
	S.[Id]
    ,S.[Code]
	,S.ConsequenceLevelId
	,CC.Name ConsequenceLevelName
	,S.AffectedHistoryId
	,AH.Name AffectedHistoryName
	,S.SeverityValueId
	,SV.Name SeverityValueName
	,A.[Name] ActiveStatus
    ,S.CreatedAt
FROM [RiskManagement].[Severity] S
INNER JOIN [Basic].[ActiveStatus] A ON S.ActiveStatusId = A.ID
INNER JOIN RiskManagement.ConsequenceLevel CC on CC.Id = S.ConsequenceLevelId AND CC.ActiveStatusId<>3
INNER JOIN RiskManagement.AffectedHistory AH on AH.Id = S.AffectedHistoryId AND AH.ActiveStatusId<>3
INNER JOIN RiskManagement.SeverityValue SV on SV.Id = S.SeverityValueId AND SV.ActiveStatusId<>3
WHERE S.ActiveStatusId <> 3
";
    }

    public async Task<Result<IEnumerable<GetSeverityQueryResult>>> GetAll(GetAllSeveritiesQuery request)
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
        var response = await multi.ReadAsync<GetSeverityQueryResult>();
        return Result.Ok(response, request, count);
    }

    public async Task<GetSeverityQueryResult> GetById(GetSeverityQuery request)
    {
        var query = $@"
          {_mainQuery} AND S.Id = @Id
";
        using var connection = new SqlConnection(_connectionString);
        await connection.OpenAsync();
        var result = await connection.QueryFirstAsync<GetSeverityQueryResult>(query, new { request.Id });
        result.NullCheck();
        return result ?? throw SimaResultException.NotFound;
    }
}