using ArmanIT.Investigation.Dapper.QueryBuilder;
using Dapper;
using Microsoft.Extensions.Configuration;
using SIMA.Application.Query.Contract.Features.BCP.PlanTypes;
using SIMA.Framework.Common.Exceptions;
using SIMA.Framework.Common.Helper;
using SIMA.Framework.Common.Response;
using System.Data.SqlClient;

namespace SIMA.Persistance.Read.Repositories.Features.BCP.PlanTypes;

public class PlanTypeQueryRepository : IPlanTypeQueryRepository
{
    private readonly string _connectionString;
    private readonly string _mainQuery;
    public PlanTypeQueryRepository(IConfiguration configuration)
    {
        _connectionString = configuration.GetConnectionString();
        _mainQuery = @"SELECT PT.[Id]
              ,PT.[Name]
              ,PT.[Code]
              ,PT.[CreatedAt]
	          ,A.[Name] ActiveStatus
          FROM [BCP].[PlanType] PT
          INNER JOIN [Basic].[ActiveStatus] A ON PT.ActiveStatusId = A.ID
          WHERE PT.ActiveStatusId <> 3";
    }

    public async Task<Result<IEnumerable<GetPlanTypeQueryResult>>> GetAll(GetAllPlanTypesQuery request)
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
        var response = await multi.ReadAsync<GetPlanTypeQueryResult>();
        return Result.Ok(response, request, count);
    }

    public async Task<GetPlanTypeQueryResult> GetById(GetPlanTypeQuery request)
    {
        var query = $@"
          {_mainQuery} AND PT.[Id] = @Id";
        using var connection = new SqlConnection(_connectionString);
        await connection.OpenAsync();
        var result = await connection.QueryFirstAsync<GetPlanTypeQueryResult>(query, new { request.Id });
        result.NullCheck();
        return result ?? throw SimaResultException.NotFound;

    }
}