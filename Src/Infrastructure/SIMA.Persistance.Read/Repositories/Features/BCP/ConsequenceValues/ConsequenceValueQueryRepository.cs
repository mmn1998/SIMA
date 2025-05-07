using ArmanIT.Investigation.Dapper.QueryBuilder;
using Dapper;
using Microsoft.Extensions.Configuration;
using SIMA.Application.Query.Contract.Features.BCP.ConsequenceValues;
using SIMA.Framework.Common.Exceptions;
using SIMA.Framework.Common.Response;
using System.Data.SqlClient;

namespace SIMA.Persistance.Read.Repositories.Features.BCP.ConsequenceValues;

public class ConsequenceValueQueryRepository : IConsequenceValueQueryRepository
{
    private readonly string _connectionString;
    private readonly string _mainQuery;
    public ConsequenceValueQueryRepository(IConfiguration configuration)
    {
        _connectionString = configuration.GetConnectionString();
        _mainQuery = @"
 Select 
	 CV.[Id]
	,CV.[Name]
	,CV.[OriginId]
	,CV.[ConsequenceId]
	,O.Name OriginName
    ,C.Name ConsequenceName
    ,CV.CreatedAt
    ,CV.[ValueNumber]
	,A.[Name] ActiveStatus
From [BCP].ConsequenceValue CV
join Basic.ActiveStatus A on CV.ActiveStatusId = A.Id
INNER JOIN BCP.Origin O ON CV.OriginId = O.Id AND O.ActiveStatusId<>3
INNER JOIN BCP.Consequence C ON CV.ConsequenceId = C.Id AND C.ActiveStatusId<>3
WHERE CV.[ActiveStatusID] <> 3
";
    }

    public async Task<Result<IEnumerable<GetConsequenceValueQueryResult>>> GetAll(GetAllConsequenceValuesQuery request)
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
        var response = await multi.ReadAsync<GetConsequenceValueQueryResult>();
        return Result.Ok(response, request, count);
    }

    public async Task<GetConsequenceValueQueryResult> GetById(GetConsequenceValueQuery request)
    {
        var query = $@"
          {_mainQuery}
          AND CV.[Id] = @Id";
        using var connection = new SqlConnection(_connectionString);
        await connection.OpenAsync();
        var result = await connection.QueryFirstAsync<GetConsequenceValueQueryResult>(query, new { request.Id });
        return result ?? throw SimaResultException.NotFound;
    }
}