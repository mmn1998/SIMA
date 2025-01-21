using ArmanIT.Investigation.Dapper.QueryBuilder;
using Dapper;
using Microsoft.Extensions.Configuration;
using SIMA.Application.Query.Contract.Features.BCP.Origins;
using SIMA.Framework.Common.Exceptions;
using SIMA.Framework.Common.Helper;
using SIMA.Framework.Common.Response;
using System.Data.SqlClient;

namespace SIMA.Persistance.Read.Repositories.Features.BCP.Origins;

public class OriginQueryRepository : IOriginQueryRepository
{
    private readonly string _connectionString;
    private readonly string _mainQuery;
    public OriginQueryRepository(IConfiguration configuration)
    {
        _connectionString = configuration.GetConnectionString();
        _mainQuery = @"SELECT O.[Id]
              ,O.[Name]
              ,O.[Code]
              ,O.[CreatedAt]
	          ,A.[Name] ActiveStatus
          FROM [BCP].[Origin] O
          INNER JOIN [Basic].[ActiveStatus] A ON O.ActiveStatusId = A.ID
          WHERE O.ActiveStatusId <> 3";
    }

    public async Task<Result<IEnumerable<GetOriginQueryResult>>> GetAll(GetAllOriginsQuery request)
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
        var response = await multi.ReadAsync<GetOriginQueryResult>();
        return Result.Ok(response, request, count);
    }

    public async Task<GetOriginQueryResult> GetById(GetOriginQuery request)
    {
        var query = $@"
          {_mainQuery} AND O.[Id] = @Id";
        using var connection = new SqlConnection(_connectionString);
        await connection.OpenAsync();
        var result = await connection.QueryFirstAsync<GetOriginQueryResult>(query, new { request.Id });
        result.NullCheck();
        return result ?? throw SimaResultException.NotFound;

    }
}