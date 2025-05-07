using ArmanIT.Investigation.Dapper.QueryBuilder;
using Dapper;
using Microsoft.Extensions.Configuration;
using SIMA.Application.Query.Contract.Features.Auths.PositionTypes;
using SIMA.Framework.Common.Exceptions;
using SIMA.Framework.Common.Helper;
using SIMA.Framework.Common.Response;
using System.Data.SqlClient;

namespace SIMA.Persistance.Read.Repositories.Features.Auths.PositionTypes;

public class PositionTypeQueryRepository : IPositionTypeQueryRepository
{
    private readonly string _connectionString;
    private readonly string _mainQuery;
    public PositionTypeQueryRepository(IConfiguration configuration)
    {
        _connectionString = configuration.GetConnectionString();
        _mainQuery = @"SELECT ST.[Id]
              ,ST.[Name]
              ,ST.[Code]
              ,ST.[CreatedAt]
	          ,A.[Name] ActiveStatus
          FROM [Organization].[PositionType] ST
          INNER JOIN [Basic].[ActiveStatus] A ON ST.ActiveStatusId = A.ID
          WHERE ST.ActiveStatusId <> 3";
    }

    public async Task<Result<IEnumerable<GetPositionTypeQueryResult>>> GetAll(GetAllPositionTypesQuery request)
    {
        using (var connection = new SqlConnection(_connectionString))
        {
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
            using (var multi = await connection.QueryMultipleAsync(dynaimcParameters.Item1.RawSql, dynaimcParameters.Item2))
            {
                var count = await multi.ReadFirstAsync<int>();
                var response = await multi.ReadAsync<GetPositionTypeQueryResult>();
                return Result.Ok(response, request, count);
            }
        }
    }

    public async Task<GetPositionTypeQueryResult> GetById(GetPositionTypeQuery request)
    {
        var query = @"
          SELECT ST.[Id]
              ,ST.[Name]
              ,ST.[Code]
	          ,A.[Name] ActiveStatus
          FROM [Organization].[PositionType] ST
          INNER JOIN [Basic].[ActiveStatus] A ON ST.ActiveStatusId = A.ID
          WHERE ST.[Id] = @Id AND ST.ActiveStatusId <> 3";
        using (var connection = new SqlConnection(_connectionString))
        {
            await connection.OpenAsync();
            var result = await connection.QueryFirstAsync<GetPositionTypeQueryResult>(query, new { request.Id });
            result.NullCheck();
            return result ?? throw SimaResultException.NotFound;
        }
    }
}