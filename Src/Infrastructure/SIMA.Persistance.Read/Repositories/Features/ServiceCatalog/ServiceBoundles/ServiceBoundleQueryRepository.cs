using ArmanIT.Investigation.Dapper.QueryBuilder;
using Dapper;
using Microsoft.Extensions.Configuration;
using SIMA.Application.Query.Contract.Features.ServiceCatalog.ServiceBoundles;
using SIMA.Framework.Common.Exceptions;
using SIMA.Framework.Common.Helper;
using SIMA.Framework.Common.Response;
using System.Data.SqlClient;

namespace SIMA.Persistance.Read.Repositories.Features.ServiceCatalog.ServiceBoundles;

public class ServiceBoundleQueryRepository : IServiceBoundleQueryRepository
{
    private readonly string _connectionString;
    private readonly string _mainQuery;
    public ServiceBoundleQueryRepository(IConfiguration configuration)
    {
        _connectionString = configuration.GetConnectionString();
        _mainQuery = @"SELECT ST.[Id]
                          ,ST.[Name]
                          ,ST.[Code]
                          ,ST.[ServiceCategoryId]
                          ,ST.[CreatedAt]
	                      ,A.[Name] ActiveStatus
                      FROM [ServiceCatalog].[ServiceBoundle] ST
                      INNER JOIN [Basic].[ActiveStatus] A ON ST.ActiveStatusId = A.ID
                      WHERE ST.ActiveStatusId <> 3";
    }

    public async Task<Result<IEnumerable<GetServiceBoundleQueryResult>>> GetAll(GetAllServiceBoundlesQuery request)
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
            var dynaimcParameters = DapperHelperExtention.GenerateQuery(queryCount + query, request);
            using (var multi = await connection.QueryMultipleAsync(dynaimcParameters.Item1.RawSql, dynaimcParameters.Item2))
            {
                var count = await multi.ReadFirstAsync<int>();
                var response = await multi.ReadAsync<GetServiceBoundleQueryResult>();
                return Result.Ok(response, request, count);
            }
        }
    }

    public async Task<GetServiceBoundleQueryResult> GetById(GetServiceBoundleQuery request)
    {
        var query = @"
          SELECT ST.[Id]
              ,ST.[Name]
              ,ST.[Code]
              ,ST.[ServiceCategoryId]
              ,SC.[ServiceTypeId]
	          ,A.[Name] ActiveStatus
          FROM [ServiceCatalog].[ServiceBoundle] ST
          INNER JOIN [Basic].[ActiveStatus] A ON ST.ActiveStatusId = A.ID
          INNER JOIN [ServiceCatalog].[ServiceCategory] SC on SC.Id = ST.ServiceCategoryId
          WHERE ST.[Id] = @Id AND ST.ActiveStatusId <> 3";
        using (var connection = new SqlConnection(_connectionString))
        {
            await connection.OpenAsync();
            var result = await connection.QueryFirstAsync<GetServiceBoundleQueryResult>(query, new { request.Id });
            result.NullCheck();
            return result ?? throw SimaResultException.NotFound;
        }
    }
}
