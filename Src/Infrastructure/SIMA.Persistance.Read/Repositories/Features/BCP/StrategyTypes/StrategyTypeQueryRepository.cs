using ArmanIT.Investigation.Dapper.QueryBuilder;
using Dapper;
using Microsoft.Extensions.Configuration;
using SIMA.Application.Query.Contract.Features.BCP.StrategyTypes;
using SIMA.Framework.Common.Exceptions;
using SIMA.Framework.Common.Helper;
using SIMA.Framework.Common.Response;
using System.Data.SqlClient;

namespace SIMA.Persistance.Read.Repositories.Features.BCP.StrategyTypes;

public class StrategyTypeQueryRepository : IStrategyTypeQueryRepository
{
    private readonly string _connectionString;
    private readonly string _mainQuery;
    public StrategyTypeQueryRepository(IConfiguration configuration)
    {
        _connectionString = configuration.GetConnectionString();
        _mainQuery = @"SELECT O.[Id]
              ,O.[Name]
              ,O.[Code]
              ,O.[CreatedAt]
	          ,A.[Name] ActiveStatus
          FROM [BCP].[StrategyType] O
          INNER JOIN [Basic].[ActiveStatus] A ON O.ActiveStatusId = A.ID
          WHERE O.ActiveStatusId <> 3";
    }

    public async Task<Result<IEnumerable<GetStrategyTypeQueryResult>>> GetAll(GetAllStrategyTypesQuery request)
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
        var response = await multi.ReadAsync<GetStrategyTypeQueryResult>();
        return Result.Ok(response, request, count);
    }

    public async Task<GetStrategyTypeQueryResult> GetById(GetStrategyTypeQuery request)
    {
        var query = $@"
          {_mainQuery} AND O.[Id] = @Id";
        using var connection = new SqlConnection(_connectionString);
        await connection.OpenAsync();
        var result = await connection.QueryFirstAsync<GetStrategyTypeQueryResult>(query, new { request.Id });
        result.NullCheck();
        return result ?? throw SimaResultException.NotFound;

    }
}