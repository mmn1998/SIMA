using ArmanIT.Investigation.Dapper.QueryBuilder;
using Dapper;
using Microsoft.Extensions.Configuration;
using SIMA.Application.Query.Contract.Features.BCP.ConsequenceIntensionDescriptions;
using SIMA.Framework.Common.Exceptions;
using SIMA.Framework.Common.Response;
using System.Data.SqlClient;

namespace SIMA.Persistance.Read.Repositories.Features.BCP.ConsequenceIntensionDescriptions;

public class ConsequenceIntensionDescriptionQueryRepository : IConsequenceIntensionDescriptionQueryRepository
{
    private readonly string _connectionString;
    private readonly string _mainQuery;
    public ConsequenceIntensionDescriptionQueryRepository(IConfiguration configuration)
    {
        _connectionString = configuration.GetConnectionString();
        _mainQuery = @"
 Select 
	 BV.[Id]
	,BV.[ConsequenceIntensionId]
	,BV.[ConsequenceId]
	,CI.Name ConsequenceIntensionName
    ,C.Name ConsequenceName
    ,BV.CreatedAt
	,A.[Name] ActiveStatus
From [BCP].ConsequenceIntensionDescription BV
join Basic.ActiveStatus A on BV.ActiveStatusId = A.Id
INNER JOIN BCP.ConsequenceIntension CI ON BV.ConsequenceIntensionId = CI.Id AND CI.ActiveStatusId<>3
INNER JOIN BCP.Consequence C ON BV.ConsequenceId = C.Id AND C.ActiveStatusId<>3
WHERE BV.[ActiveStatusID] <> 3
";
    }

    public async Task<Result<IEnumerable<GetConsequenceIntensionDescriptionQueryResult>>> GetAll(GetAllConsequenceIntensionDescriptionsQuery request)
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
        var response = await multi.ReadAsync<GetConsequenceIntensionDescriptionQueryResult>();
        return Result.Ok(response, request, count);
    }

    public async Task<GetConsequenceIntensionDescriptionQueryResult> GetById(GetConsequenceIntensionDescriptionQuery request)
    {
        var query = $@"
          {_mainQuery}
          AND BV.[Id] = @Id";
        using var connection = new SqlConnection(_connectionString);
        await connection.OpenAsync();
        var result = await connection.QueryFirstAsync<GetConsequenceIntensionDescriptionQueryResult>(query, new { request.Id });
        return result ?? throw SimaResultException.NotFound;
    }
}