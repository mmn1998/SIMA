using ArmanIT.Investigation.Dapper.QueryBuilder;
using Dapper;
using Microsoft.Extensions.Configuration;
using SIMA.Application.Query.Contract.Features.ServiceCatalog.ServiceCustomerTypes;
using SIMA.Framework.Common.Exceptions;
using SIMA.Framework.Common.Helper;
using SIMA.Framework.Common.Response;
using System.Data.SqlClient;

namespace SIMA.Persistance.Read.Repositories.Features.ServiceCatalog.ServiceCustomerTypes;

public class ServiceCustomerTypeQueryRepository : IServiceCustomerTypeQueryRepository
{
    private readonly string _connectionString;
    private readonly string _mainQuery;
    public ServiceCustomerTypeQueryRepository(IConfiguration configuration)
    {
        _connectionString = configuration.GetConnectionString();
        _mainQuery = @"SELECT ST.[Id]
              ,ST.[Name]
              ,ST.[Code]
              ,ST.[CreatedAt]
	          ,A.[Name] ActiveStatus
          FROM [ServiceCatalog].[ServiceCustomerType] ST
          INNER JOIN [Basic].[ActiveStatus] A ON ST.ActiveStatusId = A.ID
          WHERE ST.ActiveStatusId <> 3";
    }

    public async Task<Result<IEnumerable<GetServiceCustomerTypeQueryResult>>> GetAll(GetAllServiceCustomerTypesQuery request)
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
                var response = await multi.ReadAsync<GetServiceCustomerTypeQueryResult>();
                return Result.Ok(response, request, count);
            }
        }
    }

    public async Task<GetServiceCustomerTypeQueryResult> GetById(GetServiceCustomerTypeQuery request)
    {
        var query = @"
          SELECT ST.[Id]
              ,ST.[Name]
              ,ST.[Code]
	          ,A.[Name] ActiveStatus
          FROM [ServiceCatalog].[ServiceCustomerType] ST
          INNER JOIN [Basic].[ActiveStatus] A ON ST.ActiveStatusId = A.ID
          WHERE ST.[Id] = @Id AND ST.ActiveStatusId <> 3";
        using (var connection = new SqlConnection(_connectionString))
        {
            await connection.OpenAsync();
            var result = await connection.QueryFirstAsync<GetServiceCustomerTypeQueryResult>(query, new { request.Id });
            result.NullCheck();
            return result ?? throw SimaResultException.NotFound;
        }
    }
}