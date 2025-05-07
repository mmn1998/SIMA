using ArmanIT.Investigation.Dapper.QueryBuilder;
using Dapper;
using Microsoft.Extensions.Configuration;
using SIMA.Application.Query.Contract.Features.ServiceCatalog.ServiceCategories;
using SIMA.Application.Query.Contract.Features.ServiceCatalog.ServicePriorities;
using SIMA.Framework.Common.Exceptions;
using SIMA.Framework.Common.Helper;
using SIMA.Framework.Common.Response;
using System.Data.SqlClient;

namespace SIMA.Persistance.Read.Repositories.Features.ServiceCatalog.ServicePriorities
{
    public class ServicePriorityQueryRepository : IServicePriorityQueryRepository
    {
        private readonly string _connectionString;
        private readonly string _mainQuery;
        public ServicePriorityQueryRepository(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString();
            _mainQuery = @"
                    SELECT SP.[Id]
                                  ,SP.[Name]
                                  ,SP.[Code]
                                  ,SP.Ordering
	                              ,A.[Name] ActiveStatus
                                  ,SP.CreatedAt
                              FROM [ServiceCatalog].[ServicePriority] SP
                              INNER JOIN [Basic].[ActiveStatus] A ON SP.ActiveStatusId = A.ID
                    WHERE SP.ActiveStatusId <> 3";
        }
        public async Task<Result<IEnumerable<GetAllServicePrioritiesQueryResult>>> GetAll(GetAllServicePrioritiesQuery request)
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
                    var response = await multi.ReadAsync<GetAllServicePrioritiesQueryResult>();
                    return Result.Ok(response, request, count);
                }
            }
        }

        public async Task<GetAllServicePrioritiesQueryResult> GetById(GetServicePriorityQuery request)
        {
            var query = @"
                             SELECT
                                   SP.[Id]
                                  ,SP.[Name]
                                  ,SP.[Code]
                                  ,SP.Ordering
	                              ,A.[Name] ActiveStatus
                                  ,SP.CreatedAt
                              FROM [ServiceCatalog].[ServicePriority] SP
                              INNER JOIN [Basic].[ActiveStatus] A ON SP.ActiveStatusId = A.ID
                              WHERE SP.[Id] = @Id AND SP.ActiveStatusId <> 3";
            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();
                var result = await connection.QueryFirstAsync<GetAllServicePrioritiesQueryResult>(query, new { request.Id });
                result.NullCheck();
                return result ?? throw SimaResultException.NotFound;
            }
        }
    }
}
