using ArmanIT.Investigation.Dapper.QueryBuilder;
using Dapper;
using Microsoft.Extensions.Configuration;
using SIMA.Application.Query.Contract.Features.BCP.BiaValues;
using SIMA.Framework.Common.Exceptions;
using SIMA.Framework.Common.Response;
using System.Data.SqlClient;

namespace SIMA.Persistance.Read.Repositories.Features.BCP.BiaValues;

public class BiaValueQueryRepository : IBiaValueQueryRepository
{
    private readonly string _connectionString;
    private readonly string _mainQuery;
    public BiaValueQueryRepository(IConfiguration configuration)
    {
        _connectionString = configuration.GetConnectionString();
        _mainQuery = @"
 Select 
	 BV.[Id]
	,BV.[Name]
	,BV.[ConsequenceIntensionId]
	,BV.[ConsequenceId]
	,BI.Name ConsequenceIntensionName
    ,C.Name ConsequenceName
    ,BV.CreatedAt
    ,BV.[ValueNumber]
	,A.[Name] ActiveStatus
From [BCP].BiaValue BV
join Basic.ActiveStatus A on BV.ActiveStatusId = A.Id
INNER JOIN BCP.ConsequenceIntension CI ON BV.OriginId = CI.Id AND CI.ActiveStatusId<>3
INNER JOIN BCP.Consequence C ON BV.ConsequenceId = C.Id AND C.ActiveStatusId<>3
WHERE BV.[ActiveStatusID] <> 3
";
    }

    public async Task<Result<IEnumerable<GetBiaValueQueryResult>>> GetAll(GetAllBiaValuesQuery request)
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
        var response = await multi.ReadAsync<GetBiaValueQueryResult>();
        return Result.Ok(response, request, count);
    }

    public async Task<GetBiaValueQueryResult> GetById(GetBiaValueQuery request)
    {
        var query = $@"
          {_mainQuery}
          AND BV.[Id] = @Id";
        using var connection = new SqlConnection(_connectionString);
        await connection.OpenAsync();
        var result = await connection.QueryFirstAsync<GetBiaValueQueryResult>(query, new { request.Id });
        return result ?? throw SimaResultException.NotFound;
    }
}