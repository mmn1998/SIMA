using ArmanIT.Investigation.Dapper.QueryBuilder;
using Dapper;
using Microsoft.Extensions.Configuration;
using SIMA.Application.Query.Contract.Features.Auths.TimeMeasurements;
using SIMA.Framework.Common.Exceptions;
using SIMA.Framework.Common.Helper;
using SIMA.Framework.Common.Response;
using System.Data.SqlClient;

namespace SIMA.Persistance.Read.Repositories.Features.Auths.TimeMeasurements;

public class TimeMeasurementQueryRepository : ITimeMeasurementQueryRepository
{
    private readonly string _connectionString;
    private readonly string _mainQuery;
    public TimeMeasurementQueryRepository(IConfiguration configuration)
    {
        _connectionString = configuration.GetConnectionString();
        _mainQuery = @"SELECT O.[Id]
              ,O.[Name]
              ,O.[Code]
              ,O.[UnitBasement]
              ,O.[CreatedAt]
	          ,A.[Name] ActiveStatus
          FROM [Basic].[TimeMeasurement] O
          INNER JOIN [Basic].[ActiveStatus] A ON O.ActiveStatusId = A.ID
          WHERE O.ActiveStatusId <> 3";
    }

    public async Task<Result<IEnumerable<GetTimeMeasurementQueryResult>>> GetAll(GetAllTimeMeasurementsQuery request)
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
                var response = await multi.ReadAsync<GetTimeMeasurementQueryResult>();
                return Result.Ok(response, request, count);
            }
        }
    }

    public async Task<GetTimeMeasurementQueryResult> GetById(GetTimeMeasurementQuery request)
    {
        var query = $@"
          {_mainQuery} AND O.[Id] = @Id";
        using (var connection = new SqlConnection(_connectionString))
        {
            await connection.OpenAsync();
            var result = await connection.QueryFirstAsync<GetTimeMeasurementQueryResult>(query, new { request.Id });
            result.NullCheck();
            return result ?? throw SimaResultException.NotFound;
        }
    }
}