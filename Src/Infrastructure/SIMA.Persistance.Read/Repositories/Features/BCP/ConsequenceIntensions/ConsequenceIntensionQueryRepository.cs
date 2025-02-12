using ArmanIT.Investigation.Dapper.QueryBuilder;
using Dapper;
using Microsoft.Extensions.Configuration;
using SIMA.Application.Query.Contract.Features.BCP.ConsequenceIntensions;
using SIMA.Framework.Common.Exceptions;
using SIMA.Framework.Common.Response;
using System.Data.SqlClient;

namespace SIMA.Persistance.Read.Repositories.Features.BCP.ConsequenceIntensions;

public class ConsequenceIntensionQueryRepository : IConsequenceIntensionQueryRepository
{
    private readonly string _connectionString;
    private readonly string _mainQuery;
    public ConsequenceIntensionQueryRepository(IConfiguration configuration)
    {
        _connectionString = configuration.GetConnectionString();
        _mainQuery = @"
 Select 
	 CI.[Id]
	,CI.[Name]
	,CI.[Code]
    ,CI.CreatedAt
    ,CI.[ValueNumber]
	,A.[Name] ActiveStatus
From [BCP].ConsequenceIntension CI
join Basic.ActiveStatus A on CI.ActiveStatusId = A.Id
WHERE CI.[ActiveStatusID] <> 3
";
    }

    public async Task<Result<IEnumerable<GetConsequenceIntensionQueryResult>>> GetAll(GetAllConsequenceIntensionsQuery request)
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
        var response = await multi.ReadAsync<GetConsequenceIntensionQueryResult>();
        return Result.Ok(response, request, count);
    }

    public async Task<GetConsequenceIntensionQueryResult> GetById(GetConsequenceIntensionQuery request)
    {
        var query = $@"
          {_mainQuery}
          AND CI.[Id] = @Id";
        using var connection = new SqlConnection(_connectionString);
        await connection.OpenAsync();
        var result = await connection.QueryFirstAsync<GetConsequenceIntensionQueryResult>(query, new { request.Id });
        return result ?? throw SimaResultException.NotFound;
    }
}