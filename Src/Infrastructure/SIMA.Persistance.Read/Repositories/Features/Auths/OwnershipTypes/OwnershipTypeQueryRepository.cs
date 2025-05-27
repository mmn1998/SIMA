using ArmanIT.Investigation.Dapper.QueryBuilder;
using Dapper;
using Microsoft.Extensions.Configuration;
using SIMA.Application.Query.Contract.Features.Auths.OwnershipTypes;
using SIMA.Framework.Common.Exceptions;
using SIMA.Framework.Common.Response;
using System.Data.SqlClient;

namespace SIMA.Persistance.Read.Repositories.Features.Auths.OwnershipTypes;

public class OwnershipTypeQueryRepository : IOwnershipTypeQueryRepository
{
    private readonly string _connectionString;
    private readonly string _mainQuery;
    public OwnershipTypeQueryRepository(IConfiguration configuration)
    {
        _connectionString = configuration.GetConnectionString();
        _mainQuery = @"
Select 
	F.[Id]
	,F.[Name]
	,F.[Code]
    ,F.CreatedAt
	,A.[Name] ActiveStatus
From Authentication.OwnershipType F
join Basic.ActiveStatus A on F.ActiveStatusId = A.Id
WHERE F.[ActiveStatusID] <> 3
";
    }
    public async Task<Result<IEnumerable<GetOwnershipTypeQueryResult>>> GetAll(GetAllOwnershipTypesQuery request)
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
        var response = await multi.ReadAsync<GetOwnershipTypeQueryResult>();
        return Result.Ok(response, request, count);
    }

    public async Task<GetOwnershipTypeQueryResult> GetById(GetOwnershipTypeQuery request)
    {
        var query = $@"
          {_mainQuery} AND F.[Id] = @Id";
        using var connection = new SqlConnection(_connectionString);
        await connection.OpenAsync();
        var result = await connection.QueryFirstAsync<GetOwnershipTypeQueryResult>(query, new { request.Id });
        return result ?? throw SimaResultException.NotFound;

    }
}