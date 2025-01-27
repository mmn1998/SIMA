using ArmanIT.Investigation.Dapper.QueryBuilder;
using Dapper;
using Microsoft.Extensions.Configuration;
using SIMA.Application.Query.Contract.Features.RiskManagement.MatrixAs;
using SIMA.Framework.Common.Exceptions;
using SIMA.Framework.Common.Helper;
using SIMA.Framework.Common.Response;
using System.Data.SqlClient;

namespace SIMA.Persistance.Read.Repositories.Features.RiskManagement.MatrixAs;

public class MatrixAQueryRepository : IMatrixAQueryRepository
{
    private readonly string _connectionString;
    private readonly string _mainQuery;
    public MatrixAQueryRepository(IConfiguration configuration)
    {
        _connectionString = configuration.GetConnectionString();
        _mainQuery = @"
SELECT
	S.[Id]
    ,S.[Code]
	,S.TriggerStatusId
	,Ts.Name TriggerStatusName
	,S.UseVulnerabilityId
	,UV.Name UseVulnerabilityName
	,S.MatrixAValueId
	,SV.Color MatrixAValueName
	,A.[Name] ActiveStatus
    ,S.CreatedAt
FROM [RiskManagement].[MatrixA] S
INNER JOIN [Basic].[ActiveStatus] A ON S.ActiveStatusId = A.ID
INNER JOIN RiskManagement.TriggerStatus TS on TS.Id = S.TriggerStatusId AND TS.ActiveStatusId<>3
INNER JOIN RiskManagement.UseVulnerability UV on UV.Id = S.UseVulnerabilityId AND UV.ActiveStatusId<>3
INNER JOIN RiskManagement.MatrixAValue SV on SV.Id = S.MatrixAValueId AND SV.ActiveStatusId<>3
WHERE S.ActiveStatusId <> 3
";
    }

    public async Task<Result<IEnumerable<GetMatrixAQueryResult>>> GetAll(GetAllMatrixAsQuery request)
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
        var response = await multi.ReadAsync<GetMatrixAQueryResult>();
        return Result.Ok(response, request, count);
    }

    public async Task<GetMatrixAQueryResult> GetById(GetMatrixAQuery request)
    {
        var query = $@"
          {_mainQuery} AND S.Id = @Id
";
        using var connection = new SqlConnection(_connectionString);
        await connection.OpenAsync();
        var result = await connection.QueryFirstAsync<GetMatrixAQueryResult>(query, new { request.Id });
        result.NullCheck();
        return result ?? throw SimaResultException.NotFound;
    }
}